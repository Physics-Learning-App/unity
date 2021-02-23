using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Networking;

public class playersRequest : MonoBehaviour {
	public Text[] leaderBoardNameText;
	public Text[] leaderBoardScoreText;
	webAPI webApi;
	IEnumerator coroutine;

	void Start(){
		webApi = GetComponent<webAPI> ();
	}

	public void fetchPlayers(){
		var request = webApi.CreateApiGetRequest ("/players");
		coroutine = tryFetch (request);
		StartCoroutine (coroutine);
	}

	IEnumerator tryFetch(UnityWebRequest www){
		yield return www.Send ();
		if (www.isError) {
			Debug.Log (www.error);
		} else {
			Debug.Log (www.downloadHandler.text);
			if (!www.downloadHandler.text.Contains ("errors")) {
				processJsonData (www.downloadHandler.text);
			}
		}
	}

	private void processJsonData(string _url){
		playersResponse jsonData = JsonUtility.FromJson<playersResponse> (_url);
		for (int i = 0; i < 10; i++) {
			leaderBoardNameText[i].text = (i+1) + "." + jsonData.players[i].name;
			leaderBoardScoreText [i].text = "" + jsonData.players [i].score;
		}
	}
}
