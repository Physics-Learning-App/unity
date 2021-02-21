using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class monsterRequest : MonoBehaviour {
	private string baseUrl = "http://localhost:3000/monsters";
	public string[] questionEasy, questionMedium, questionHard;
	public string[] answerEasy, answerMedium, answerHard;

	void Start(){
		StartCoroutine (getDataEasy (baseUrl + "/easy"));
		StartCoroutine (getDataMedium (baseUrl + "/medium"));
		StartCoroutine (getDataHard (baseUrl + "/hard"));
	}

	IEnumerator getDataEasy (string _url) {
		UnityWebRequest www = UnityWebRequest.Get(_url);
		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			processJsonDataEasy (www.downloadHandler.text);
		}
	}

	IEnumerator getDataMedium (string _url) {
		UnityWebRequest www = UnityWebRequest.Get(_url);
		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			processJsonDataMedium (www.downloadHandler.text);
		}
	}

	IEnumerator getDataHard (string _url) {
		UnityWebRequest www = UnityWebRequest.Get(_url);
		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			processJsonDataHard (www.downloadHandler.text);
		}
	}

	private void processJsonDataEasy(string _url){
		monsterResponse jsonData = JsonUtility.FromJson<monsterResponse> (_url);

		foreach (Problem el in jsonData.Problems) {
			string[] tmp = new string[questionEasy.Length];
			Array.Copy (questionEasy, tmp, questionEasy.Length);
			questionEasy = new string[questionEasy.Length + 1];
			Array.Copy (tmp, questionEasy, tmp.Length);
			questionEasy [questionEasy.Length - 1] = el.question;
			tmp = new string[answerEasy.Length];
			Array.Copy (answerEasy, tmp, answerEasy.Length);
			answerEasy = new string[answerEasy.Length + 1];
			Array.Copy (tmp, answerEasy, tmp.Length);
			answerEasy [answerEasy.Length - 1] = el.answer;
		}
	}

	private void processJsonDataMedium(string _url){
		monsterResponse jsonData = JsonUtility.FromJson<monsterResponse> (_url);

		foreach (Problem el in jsonData.Problems) {
			string[] tmp = new string[questionMedium.Length];
			Array.Copy (questionMedium, tmp, questionMedium.Length);
			questionMedium = new string[questionMedium.Length + 1];
			Array.Copy (tmp, questionMedium, tmp.Length);
			questionMedium [questionMedium.Length - 1] = el.question;
			tmp = new string[answerMedium.Length];
			Array.Copy (answerMedium, tmp, answerMedium.Length);
			answerMedium = new string[answerMedium.Length + 1];
			Array.Copy (tmp, answerMedium, tmp.Length);
			answerMedium [answerMedium.Length - 1] = el.answer;
		}
	}

	private void processJsonDataHard(string _url){
		monsterResponse jsonData = JsonUtility.FromJson<monsterResponse> (_url);

		foreach (Problem el in jsonData.Problems) {
			string[] tmp = new string[questionHard.Length];
			Array.Copy (questionHard, tmp, questionHard.Length);
			questionHard = new string[questionHard.Length + 1];
			Array.Copy (tmp, questionHard, tmp.Length);
			questionHard [questionHard.Length - 1] = el.question;
			tmp = new string[answerHard.Length];
			Array.Copy (answerHard, tmp, answerHard.Length);
			answerHard = new string[answerHard.Length + 1];
			Array.Copy (tmp, answerHard, tmp.Length);
			answerHard [answerHard.Length - 1] = el.answer;
		}
	}
}
