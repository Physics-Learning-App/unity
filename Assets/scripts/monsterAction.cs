using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monsterAction : MonoBehaviour {
	public GameObject canvasNPC;

	void OnCollisionEnter(Collision collision)
	{
		if (collision.gameObject.CompareTag("Player")) {
			canvasNPC.SetActive (true);
			collision.gameObject.GetComponent<characterMove> ().isInteraction = true;
		}
	}

	void OnCollisionExit(Collision collision){
		Debug.Log ("Keluar");
	}
}
