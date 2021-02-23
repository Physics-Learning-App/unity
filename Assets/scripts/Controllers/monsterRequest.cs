using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.Networking;

public class monsterRequest : MonoBehaviour {
	private string baseUrl = "http://localhost:4001/monsters";
	public string[] questionEasy, questionMedium, questionHard;
	public string[] answerEasy, answerMedium, answerHard;
	public string[] choiceEasy, choiceMedium, choiceHard;

	void Start(){
		StartCoroutine (getDataEasy (baseUrl + "/easy"));
		StartCoroutine (getDataMedium (baseUrl + "/medium"));
		StartCoroutine (getDataHard (baseUrl + "/hard"));
	}

	public Problem getQuestion(string difficulty){
		switch (difficulty) {
		case "easy":
			return new Problem {
				question = questionEasy [0],
				answer = answerEasy [0],
				choices = choiceEasy [0]
			};
		case "medium":
			return new Problem {
				question = questionMedium [0],
				answer = answerMedium [0],
				choices = choiceMedium [0]
			};
		case "hard":
			return new Problem {
				question = questionHard [0],
				answer = answerHard [0],
				choices = choiceHard [0]
			};
		default:
			return new Problem();
		}
	}

	IEnumerator getDataEasy (string _url) {
		UnityWebRequest www = UnityWebRequest.Get(_url);
		yield return www.Send();

		if(www.isError) {
			Debug.Log(www.error);
		}
		else {
			processJsonDataEasy (www.downloadHandler.text);
			Debug.Log (www.downloadHandler.text);
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
			Debug.Log (www.downloadHandler.text);
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
			Debug.Log (www.downloadHandler.text);
		}
	}

	private void processJsonDataEasy(string _url){
		monsterResponse jsonData = JsonUtility.FromJson<monsterResponse> (_url);

		foreach (Problem el in jsonData.Problems) {
			string[] tmp = new string[questionEasy.Length];
			Array.Copy (questionEasy, tmp, questionEasy.Length);
			questionEasy = new string[questionEasy.Length + 1];
			Array.Copy (tmp, questionEasy, tmp.Length);
			questionEasy [tmp.Length] = el.question;
			tmp = new string[answerEasy.Length];
			Array.Copy (answerEasy, tmp, answerEasy.Length);
			answerEasy = new string[answerEasy.Length + 1];
			Array.Copy (tmp, answerEasy, tmp.Length);
			answerEasy [tmp.Length] = el.answer;
			tmp = new string[choiceEasy.Length];
			Array.Copy (choiceEasy, tmp, choiceEasy.Length);
			choiceEasy = new string[choiceEasy.Length + 1];
			Array.Copy (tmp, choiceEasy, tmp.Length);
			choiceEasy [tmp.Length] = el.choices;
		}
	}

	private void processJsonDataMedium(string _url){
		monsterResponse jsonData = JsonUtility.FromJson<monsterResponse> (_url);

		foreach (Problem el in jsonData.Problems) {
			string[] tmp = new string[questionMedium.Length];
			Array.Copy (questionMedium, tmp, questionMedium.Length);
			questionMedium = new string[questionMedium.Length + 1];
			Array.Copy (tmp, questionMedium, tmp.Length);
			questionMedium [tmp.Length] = el.question;
			tmp = new string[answerMedium.Length];
			Array.Copy (answerMedium, tmp, answerMedium.Length);
			answerMedium = new string[answerMedium.Length + 1];
			Array.Copy (tmp, answerMedium, tmp.Length);
			answerMedium [tmp.Length] = el.answer;
			tmp = new string[choiceMedium.Length];
			Array.Copy (choiceMedium, tmp, choiceMedium.Length);
			choiceMedium = new string[choiceMedium.Length + 1];
			Array.Copy (tmp, choiceMedium, tmp.Length);
			choiceMedium [tmp.Length] = el.choices;
		}
	}

	private void processJsonDataHard(string _url){
		monsterResponse jsonData = JsonUtility.FromJson<monsterResponse> (_url);

		foreach (Problem el in jsonData.Problems) {
			string[] tmp = new string[questionHard.Length];
			Array.Copy (questionHard, tmp, questionHard.Length);
			questionHard = new string[questionHard.Length + 1];
			Array.Copy (tmp, questionHard, tmp.Length);
			questionHard [tmp.Length] = el.question;
			tmp = new string[answerHard.Length];
			Array.Copy (answerHard, tmp, answerHard.Length);
			answerHard = new string[answerHard.Length + 1];
			Array.Copy (tmp, answerHard, tmp.Length);
			answerHard [tmp.Length] = el.answer;
			tmp = new string[choiceHard.Length];
			Array.Copy (choiceHard, tmp, choiceHard.Length);
			choiceHard = new string[choiceHard.Length + 1];
			Array.Copy (tmp, choiceHard, tmp.Length);
			choiceHard [tmp.Length] = el.choices;
		}
	}
}
