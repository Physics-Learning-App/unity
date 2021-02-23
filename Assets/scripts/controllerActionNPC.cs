using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controllerActionNPC : MonoBehaviour {
	public string answer;
	public string[] choice;
	public Text textQuestion;
	public Text[] textChoice;
	public Sprite[] backgroundArenaFightMode;

	GameObject controllerGame, player, boxFightMode, arenaFightMode, textSolution;
	GameObject boxWinning, boxGameOver;
	monsterRequest monsterReq;
	controllerUI remoteUI;
	controllerMusic remoteMusic;
	Animator animatorPlayer;
	IEnumerator coroutine;
	string difficulty;
	int indexSprite, indexStage;

	void Start(){
		controllerGame = GameObject.Find ("controllerGame");
		monsterReq = controllerGame.GetComponent<monsterRequest> ();
		remoteUI = controllerGame.GetComponent<controllerUI> ();
		remoteMusic = controllerGame.GetComponent<controllerMusic> ();
		canvasDefineChild canvas = GameObject.Find ("Canvas").GetComponent<canvasDefineChild> ();
		boxFightMode = canvas.boxFightMode;
		player = canvas.charUI;
		animatorPlayer = player.GetComponent<Animator> ();
		arenaFightMode = canvas.arenaFightMode;
		textSolution = canvas.textSolution;
		boxWinning = canvas.boxWinning;
		boxGameOver = canvas.boxGameOver;
	}

	public void setQuestion(){
		Problem dataQuiz = monsterReq.getQuestion (difficulty);
		textQuestion.text = dataQuiz.question;
		textSolution.GetComponent<Text> ().text = dataQuiz.explanation;
		answer = dataQuiz.answer;
		choice = dataQuiz.choices.Split(',');
		for (int i = 0; i < choice.Length; i++) {
			textChoice [i].text = choice [i];
		}
	}

	public void setSpriteFightMode(){
		arenaFightMode.GetComponent<Image> ().sprite = backgroundArenaFightMode [indexSprite];
	}

	public void tryAnswer(int index){
		if (textChoice [index].text == answer) {
			Debug.Log ("Correct");
			remoteUI.coin += 2;
			remoteUI.score += 10;
			remoteUI.updateCurrentStage (indexStage);
			animatorPlayer.SetBool ("isCorrect", true);
			coroutine = waitAnimationCompleted ();
			StartCoroutine (coroutine);

		} else {
			Debug.Log ("Wrong");
			remoteUI.healthCurrent -= 1f;
			if (remoteUI.healthCurrent <= 0) {
				remoteUI.healthCurrent = 10;
				remoteUI.score = 0;
				remoteUI.coin = 0;
				remoteUI.potion = 0;
				remoteUI.currentStage = 0;
				remoteUI.setPositionCharacter (12.35f, 1.84f);
				boxGameOver.SetActive (true);
				remoteMusic.setBgm (3, false);
				controllerGame.GetComponent<playerRequest> ().tryPutPlayer (false);
			}
		}
	}

	IEnumerator waitAnimationCompleted(){
		yield return new WaitForSeconds (1.4f);
		boxWinning.SetActive (true);
		remoteMusic.setBgm (2, false);
	}

	public void resetCanvas(){
		boxFightMode.GetComponent<Animator> ().SetBool("isReady", false);
		StopCoroutine (coroutine);
		player.GetComponent<RectTransform> ().anchoredPosition = new Vector2(-278f, 8f);
		remoteMusic.setBgm (0, false);
	}

	public void notReady(){
		this.gameObject.SetActive (false);
	}

	public void imReady(){
		setQuestion ();
		setSpriteFightMode ();
		boxFightMode.SetActive (true);
		boxFightMode.GetComponent<Animator> ().SetBool("isReady", true);
		animatorPlayer.SetBool ("isCorrect", false);
		remoteMusic.setBgm (1);
	}

	public void setDifficulty(string value) {
		difficulty = value;
	}

	public void setIndexSprite(int value){
		indexSprite = value;
	}

	public void setIndexStage(int value){
		indexStage = value;
	}
}
