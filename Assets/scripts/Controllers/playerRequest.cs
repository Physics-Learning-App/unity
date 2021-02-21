using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class playerRequest : MonoBehaviour {
	webAPI webApi;
	IEnumerator coroutine;

	void Start () {
		webApi = GetComponent<webAPI> ();
	}

	public void tryCreatePlayer(Text inputDataPlayerName){
		var request = webApi.CreateApiPostRequest ("/player", new playerResponse {
			name = inputDataPlayerName.text
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
			Debug.Log(test.downloadHandler.text);
			processJsonData (test.downloadHandler.text);
		}
	}

	private void processJsonData(string _url){
		playerResponse jsonData = JsonUtility.FromJson<playerResponse> (_url);
		PlayerPrefs.SetString ("playerName", jsonData.name);
		PlayerPrefs.Save ();
		StopCoroutine (coroutine);
	}
}
