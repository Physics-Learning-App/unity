using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class loginRequest : MonoBehaviour {
	public Text username, password, textLoginFailed;
	public GameObject loadingScreen;
	webAPI webApi;
	private IEnumerator coroutine;

	void Awake(){
		if (PlayerPrefs.HasKey ("access_token")) {
			GetComponent<controllerScenes> ().changeScreenTo ("gameplay");
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

	}

	IEnumerator postData(UnityWebRequest test) {
		yield return test.Send ();
		if(test.isError) {
			Debug.Log(test.error);
		}
		else {
			processJsonData (test.downloadHandler.text);
		}
	}

	private void processJsonData(string _url){
		loginResponse jsonData = JsonUtility.FromJson<loginResponse> (_url);
		Debug.Log(jsonData.access_token);
		PlayerPrefs.SetString ("access_token", jsonData.access_token);
		PlayerPrefs.Save ();
		StopCoroutine (coroutine);
		loadingScreen.SetActive (true);
		GetComponent<fetchPlayer> ().tryFecth ();
	}

}
