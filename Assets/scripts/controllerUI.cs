using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controllerUI : MonoBehaviour {
	public RectTransform healthBarCurrent;
	public float healthMax = 10f, healthCurrent = 10f;
	public int score, potion, coin, currentStage;
	public string playername;
	public Text textName, textScore, textCoin, textPotion;
	public GameObject player;
	public GameObject[] npc;

	void Awake(){
		Time.timeScale = 0f;
	}

	void Start(){
		updateHealth ();
		updatePotion ();
		updateCoin ();
		updateScore ();
	}

	void updateHealth(){
		if (healthCurrent < 0) {
			healthCurrent = 0;
		} else if (healthCurrent > healthMax) {
			healthCurrent = healthMax;
		}
		healthBarCurrent.localScale = new Vector3(healthCurrent / healthMax, 1f, 1f);
	}

	void updateCoin(){
		if (coin < 0) {
			coin = 0;
		}
		textCoin.text = "" + coin;
	}

	void updateScore(){
		textScore.text = "" + score;
	}

	void updatePotion(){
		if (potion < 0) {
			potion = 0;
		}
		textPotion.text = "" + potion;
	}

	public void updateData(){
		Time.timeScale = 1f;
		playername = PlayerPrefs.GetString ("playerName");
		textName.text = playername;
		score = PlayerPrefs.HasKey ("score") ? PlayerPrefs.GetInt ("score") : 0;
		coin = PlayerPrefs.HasKey ("coin") ? PlayerPrefs.GetInt ("coin") : 0;
		healthCurrent = PlayerPrefs.HasKey ("health") ? PlayerPrefs.GetFloat ("health") : 0;
		potion = PlayerPrefs.HasKey ("potion") ? PlayerPrefs.GetInt ("potion") : 0;
		currentStage = PlayerPrefs.HasKey ("currentStage") ? PlayerPrefs.GetInt ("currentStage") : 0;
		setCurrentStage ();
		float positionX = PlayerPrefs.HasKey ("positionX") ? PlayerPrefs.GetFloat ("positionX") : player.transform.position.x;
		float positionZ = PlayerPrefs.HasKey ("positionZ") ? PlayerPrefs.GetFloat ("positionZ") : player.transform.position.z;
		setPositionCharacter (positionX, positionZ);
		updateHealth ();
		updatePotion ();
		updateCoin ();
		updateScore ();
	}

	void setCurrentStage(){
		int baseStage = 0;
		if (currentStage > 11) {
			baseStage = 12;
		} else if (currentStage > 5) {
			baseStage = 6;
		}
		for (int i = baseStage; i < currentStage; i++) {
			npc [i].GetComponent<monsterAction> ().setIsCleared (true);
		}
	}

	void setPotion(int value){
		potion += value;
		updatePotion ();
	}

	public void setScore(int value){
		score += value;
		updateScore ();
	}

	public void setHealthCurrent(float value){
		healthCurrent += value;
		updateHealth ();
	}

	public void setCoin(int value) {
		coin += value;
		updateCoin ();
	}

	public void setPositionCharacter(float x, float z){
		Vector3 tmp = new Vector3 (x, player.transform.position.y, z);
		player.transform.position = tmp;
	}

	public void updateCurrentStage(int value){
		currentStage = value;
		setCurrentStage ();
	}

	public void usePotion(){
		if (potion > 0) {
			setPotion (-1);
			setHealthCurrent (1f);
			Debug.Log ("Its suits your taste: HP +1");
		} else {
			potion = 0;
			Debug.Log ("Dont have potion, buy one");
		}
	}

	public void buyPotion(){
		if (coin > 4) {
			setCoin (-5);
			setPotion (1);
			Debug.Log ("You decided to buy potion: 5 coins used");
		} else {
			Debug.Log ("I need more coins to do that");
		}
	}
}
