using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Networking;

public class fetchPlayer : MonoBehaviour {
	webAPI webApi;
	IEnumerator coroutine;

	void Start () {
		webApi = GetComponent<webAPI> ();
	}

	IEnumerator fetchData(UnityWebRequest www){
		yield return www.Send ();
		if (www.isNetworkError) {
			Debug.Log (www.error);
		} else {
			Debug.Log (www.downloadHandler.text);
			if (!www.downloadHandler.text.Contains("errors")) {
				processJsonData (www.downloadHandler.text);
			}
		}
	}

	public void checkingWhereToChangeScence(){
		int currentStage = PlayerPrefs.GetInt("currentStage");
		controllerScenes remoteScene = GetComponent<controllerScenes> ();
		if (currentStage > 17) {
			GetComponent<loginRequest> ().loadingScreen.SetActive (false);
			GetComponent<loginRequest> ().allCleared.SetActive (true);
			GetComponent<loginRequest> ().waitScreen.SetActive (false);
			PlayerPrefs.DeleteAll ();
		} else if (currentStage > 11) {
			remoteScene.changeScreenTo ("gameplay2");
		} else if (currentStage > 5) {
			remoteScene.changeScreenTo ("gameplay1");
		} else {
			remoteScene.changeScreenTo ("gameplay");
		}
	}

	public void tryFetch(){
		var request = webApi.CreateApiGetRequest ("/player");
		coroutine = fetchData (request);
		StartCoroutine (coroutine);
	}

	private void processJsonData(string _url){
		playerResponse jsonData = JsonUtility.FromJson<playerResponse> (_url);
		PlayerPrefs.SetString ("playerName", jsonData.name);
		PlayerPrefs.SetInt ("potion", jsonData.potion);
		PlayerPrefs.SetFloat ("health", jsonData.health);
		PlayerPrefs.SetInt ("coin", jsonData.coin);
		PlayerPrefs.SetInt ("score", jsonData.score);
		PlayerPrefs.SetInt ("currentStage", jsonData.currentStage);
		PlayerPrefs.SetFloat ("positionX", jsonData.positionX);
		PlayerPrefs.SetFloat ("positionZ", jsonData.positionZ);
		PlayerPrefs.Save ();
	}
}
