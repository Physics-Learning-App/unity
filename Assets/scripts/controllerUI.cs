using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controllerUI : MonoBehaviour {
	public RectTransform healthBarCurrent;
	public float healthMax;
	public float healthCurrent;
	public int score, potion, coin;
	public string playername;

	void Awake(){
		Time.timeScale = 0f;
	}

	void Start () {
		healthCurrent = healthMax;
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		healthBarCurrent.localScale = new Vector3(healthCurrent / healthMax, 1f, 1f);
	}

	public void setPlayerName(Text name) {
		playername = name.text;
		Time.timeScale = 1f;
	}
}
