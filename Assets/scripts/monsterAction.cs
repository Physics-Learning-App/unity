using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterAction : MonoBehaviour {
	public string difficulty;
	public int indexBackgroundFightMode, indexStage;
	private bool isCleared = false;
	GameObject canvasNPC;
	controllerActionNPC remoteActionNPC;

	void Start() {
		canvasNPC = GameObject.Find("Canvas").GetComponent<canvasDefineChild> ().actionNPC;
		remoteActionNPC = canvasNPC.GetComponent<controllerActionNPC> ();
	}

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player") && !isCleared) {
			canvasNPC.SetActive (true);
			collision.gameObject.GetComponent<characterMove> ().setIsInteraction (true);
			remoteActionNPC.setDifficulty (difficulty);
			remoteActionNPC.setIndexSprite(indexBackgroundFightMode);
			remoteActionNPC.setIndexStage (indexStage);
		}
	}

	public void setIsCleared(bool value){
		isCleared = value;
		GetComponent<Animator> ().SetBool ("isCleared", value);
	}
}
