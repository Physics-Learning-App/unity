using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controllerUI : MonoBehaviour {
	public RectTransform healthBarCurrent;
	public float healthMax = 10f, healthCurrent = 10f;
	public int score, potion, coin, currentStage;
	public string playername;
	public Text textName;
	public GameObject player;
	public GameObject[] npc;

	void Awake(){
		Time.timeScale = 0f;
	}

	void FixedUpdate () {
		healthBarCurrent.localScale = new Vector3(healthCurrent / healthMax, 1f, 1f);
	}

	public void updateData(){
		Time.timeScale = 1f;
		playername = PlayerPrefs.GetString ("playerName");
		textName.text = playername;
		score = PlayerPrefs.GetInt("score");
		coin = PlayerPrefs.GetInt("coin");
		healthCurrent = PlayerPrefs.GetFloat("health");
		potion = PlayerPrefs.GetInt("potion");
		currentStage = PlayerPrefs.GetInt("currentStage");
		setCurrentStage ();
		float positionX = PlayerPrefs.GetFloat("positionX");
		float positionZ = PlayerPrefs.GetFloat("positionZ");
		setPositionCharacter (positionX, positionZ);
	}

	void setCurrentStage(){
		for (int i = 0; i < currentStage; i++) {
			npc [i].GetComponent<monsterAction> ().setIsCleared (true);
		}
	}

	public void setPositionCharacter(float x, float z){
		Vector3 tmp = new Vector3 (x, player.transform.position.y, z);
		player.transform.position = tmp;
	}

	public void updateCurrentStage(int value){
		currentStage = value;
		setCurrentStage ();
	}
}
