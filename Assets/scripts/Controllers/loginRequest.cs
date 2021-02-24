using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class loginRequest : MonoBehaviour {
	public Text username, textLoginFailed;
	public InputField password;
	public GameObject loadingScreen, allCleared, waitScreen;
	webAPI webApi;
	private IEnumerator coroutine;

	void Awake(){
		if (PlayerPrefs.HasKey ("access_token")) {
			int currentStage = PlayerPrefs.GetInt ("currentStage");
			controllerScenes remoteScene = GetComponent<controllerScenes> ();
			if (currentStage > 17) {
				allCleared.SetActive (true);
				waitScreen.SetActive(false);
				PlayerPrefs.DeleteAll ();
			} else if (currentStage > 11) {
				remoteScene.changeScreenTo ("gameplay2");
			} else if (currentStage > 5) {
				remoteScene.changeScreenTo ("gameplay1");
			} else {
				remoteScene.changeScreenTo ("gameplay");
			}
		}
	}

	void Start(){
		webApi = GetComponent<webAPI> ();
	}

	public void tryLogin(){
		var request = webApi.CreateApiPostRequest ("/login", new loginRequestBody {
			username = username.text,
			password = password.text
		});
		coroutine = postData (request);
		StartCoroutine (coroutine);
		username.text = "";
		password.text = "";
	}

	IEnumerator postData(UnityWebRequest test) {
		yield return test.Send ();
		if(test.isNetworkError) {
			Debug.Log(test.error);
		}
		else {
			string res = test.downloadHandler.text;
			Debug.Log (res);
			string errorsMessage = "";
			if (res.Contains("error")) {
				errorsResponse json = JsonUtility.FromJson<errorsResponse> (res);
				foreach (string el in json.errors) {
					errorsMessage = errorsMessage + el + ", ";
				}
				textLoginFailed.text = errorsMessage;
			} else {
				Debug.Log ("Login success");
				processJsonData (res);
			}
		}
	}

	private void processJsonData(string _url){
		loginResponse jsonData = JsonUtility.FromJson<loginResponse> (_url);
		Debug.Log(jsonData.access_token);
		PlayerPrefs.SetString ("access_token", jsonData.access_token);
		PlayerPrefs.Save ();
		StopCoroutine (coroutine);
		loadingScreen.SetActive (true);
		GetComponent<fetchPlayer> ().tryFetch ();
	}

}
