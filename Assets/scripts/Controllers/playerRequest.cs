using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class playerRequest : MonoBehaviour {
	public GameObject boxInputName, mainUI, healthBar;
	webAPI webApi;
	IEnumerator coroutine;
	private string endpoint = "/player";

	void Awake(){
		if (PlayerPrefs.HasKey ("playerName")) {
			startGame ();
		}
	}

	void Start () {
		webApi = GetComponent<webAPI> ();
	}

	void startGame(){
		boxInputName.SetActive (false);
		mainUI.SetActive (true);
		healthBar.SetActive (true);
		GetComponent<controllerUI> ().updateData ();
	}

	public void tryCreatePlayer(Text inputDataPlayerName){
		var request = webApi.CreateApiPostRequest (endpoint, new playerResponse {
			name = inputDataPlayerName.text
		});
		coroutine = postData (request);
		StartCoroutine (coroutine);
	}

	public void tryPutPlayer(bool changeScreen = true){
		var request = webApi.CreateApiPutRequest (endpoint, new playerResponse {
			name = GetComponent<controllerUI>().playername,
			score = GetComponent<controllerUI> ().score,
			potion = GetComponent<controllerUI> ().potion,
			health = GetComponent<controllerUI> ().healthCurrent,
			coin = GetComponent<controllerUI> ().coin,
			currentStage = GetComponent<controllerUI> ().currentStage,
			positionX = GetComponent<controllerUI> ().player.transform.position.x,
			positionZ = GetComponent<controllerUI> ().player.transform.position.z
		});
		coroutine = putData (request, changeScreen);
		StartCoroutine (coroutine);
	}

	IEnumerator putData(UnityWebRequest res, bool value){
		yield return res.Send ();
		if (res.isError) {
			Debug.Log(res.error);
		}
		else {
			Debug.Log(res.downloadHandler.text);
			StopCoroutine (coroutine);
			PlayerPrefs.DeleteAll ();
			if (value) {
				GetComponent<controllerScenes> ().changeScreenTo ("mainmenu");
			}
		}

	}

	IEnumerator postData(UnityWebRequest test) {
		yield return test.Send ();
		if(test.isError) {
			Debug.Log(test.error);
		}
		else {
			Debug.Log(test.downloadHandler.text);
			processJsonData (test.downloadHandler.text);
		}
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
		StopCoroutine (coroutine);
		startGame ();
	}
}
