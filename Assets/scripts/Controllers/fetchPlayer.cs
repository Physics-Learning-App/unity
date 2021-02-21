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
		if (www.isError) {
			Debug.Log (www.error);
		} else {
			Debug.Log (www.downloadHandler.text);
			//processJsonData (www.downloadHandler.text);
		}
	}

	public void tryFecth(){
		var request = webApi.CreateApiGetRequest ("/player");
		coroutine = fetchData (request);
		StartCoroutine (coroutine);
	}

	private void processJsonData(string _url){
		playerResponse jsonData = JsonUtility.FromJson<playerResponse> (_url);
		PlayerPrefs.SetString ("playerName", jsonData.name);
		PlayerPrefs.Save ();
		StopCoroutine (coroutine);
		GetComponent<controllerScenes> ().changeScreenTo ("gameplay");
	}
}
