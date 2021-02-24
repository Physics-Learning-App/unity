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
	public GameObject[] enemies;

	GameObject controllerGame, player, boxFightMode, arenaFightMode, textSolution;
	GameObject boxWinning, boxGameOver, boxStageCleared, coverChoiceButton;
	monsterRequest monsterReq;
	controllerUI remoteUI;
	controllerMusic remoteMusic;
	Animator animatorPlayer;
	IEnumerator coroutine;
	string difficulty;
	int indexSprite, indexStage;
	int iSoal = 0;
	bool isBoss;

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
		boxStageCleared = canvas.boxStageCleared;
		coverChoiceButton = canvas.coverChoiceButton;
	}

	void bossMode(){
		Problem dataQuiz = monsterReq.getQuestion (difficulty, iSoal);
		textQuestion.text = dataQuiz.question;
		textSolution.GetComponent<Text> ().text = dataQuiz.explanation;
		answer = dataQuiz.answer;
		choice = dataQuiz.choices.Split(',');
		for (int i = 0; i < choice.Length; i++) {
			textChoice [i].text = choice [i];
		}
	}

	public void setQuestion(){
		Problem dataQuiz = monsterReq.getQuestion (difficulty, indexStage - 1);
		textQuestion.text = dataQuiz.question;
		textSolution.GetComponent<Text> ().text = dataQuiz.explanation;
		answer = dataQuiz.answer;
		choice = dataQuiz.choices.Split(',');
		for (int i = 0; i < choice.Length; i++) {
			textChoice [i].text = choice [i];
		}
	}

	public void tryAnswer(int index){
		coverChoiceButton.SetActive (true);
		if (textChoice [index].text == answer) {
			Debug.Log ("Correct");
			remoteUI.setCoin (2);
			remoteUI.setScore (10);
			remoteUI.updateCurrentStage (indexStage);
			animatorPlayer.SetBool ("isCorrect", true);
			coroutine = waitAnimationCompleted ();
			StartCoroutine (coroutine);
		} else {
			Debug.Log ("Wrong");
			remoteUI.setHealthCurrent(-1f);
			enemies [indexStage - 1].GetComponent<Animator> ().SetBool ("isWrong", true);
			coroutine = waitAnimationWrong ();
			StartCoroutine (coroutine);
		}
	}

	IEnumerator waitAnimationWrong(){
		yield return new WaitForSeconds (1.4f);
		coverChoiceButton.SetActive (false);
		if (remoteUI.healthCurrent <= 0) { // gameover
			remoteUI.healthCurrent = 10;
			remoteUI.score = 0;
			remoteUI.coin = 0;
			remoteUI.potion = 0;
			remoteUI.currentStage = 0;
			remoteUI.setPositionCharacter (12.35f, 1.84f);
			boxGameOver.SetActive (true);
			remoteMusic.setBgm (3, false);
			controllerGame.GetComponent<playerRequest> ().tryPutPlayer (false); //savedata
		}
		enemies [indexStage - 1].GetComponent<Animator> ().SetBool("isWrong", false);
	}

	IEnumerator waitAnimationCompleted(){
		yield return new WaitForSeconds (1.4f);
		coverChoiceButton.SetActive (false);
		if (isBoss) {
			iSoal++;
			if (iSoal > 4) {
				boxStageCleared.SetActive (true);
				remoteMusic.setBgm (2, false);
				remoteUI.setPositionCharacter (12.35f, 1.84f);
				controllerGame.GetComponent<playerRequest> ().tryPutPlayer (false);
			} else {
				imReady ();
			}
		} else {
			boxWinning.SetActive (true);
			remoteMusic.setBgm (2, false);
		}
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
		if (isBoss) {
			bossMode ();
			if (iSoal == 0) {
				remoteMusic.setBgm (4);
			}
		} else {
			setQuestion ();
			remoteMusic.setBgm (1);
		}
		setSpriteFightMode ();
		setEnemy ();
		boxFightMode.SetActive (true);
		boxFightMode.GetComponent<Animator> ().SetBool("isReady", true);
		animatorPlayer.SetBool ("isCorrect", false);
	}

	public void setDifficulty(string value) {
		difficulty = value;
	}

	public void setIndexSprite(int value){
		indexSprite = value;
	}

	public void setIndexStage(int value){
		if (value > 12) {
			value -= 12;
		} else if (value > 6) {
			value -= 6;
		}
		indexStage = value;
	}

	public void setIsBoss(bool value){
		isBoss = value;
	}

	void setSpriteFightMode(){ //setbackground fight mode n solution
		arenaFightMode.GetComponent<Image> ().sprite = backgroundArenaFightMode [indexSprite];
		boxWinning.GetComponent<Image> ().sprite = backgroundArenaFightMode [indexSprite];
	}

	void setEnemy(){ //set which enemy will active in fight mode
		bool isToActivated;
		for (int i = 0; i < enemies.Length; i++) {
			if (i == indexStage - 1) {
				isToActivated = true;
			} else {
				isToActivated = false;
			}
			enemies [i].SetActive (isToActivated);
		}
	}
}
