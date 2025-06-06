﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class characterMove : MonoBehaviour {
	public Transform camera;
	public bool isInteraction = false;
	private float speed = 1f;

	void FixedUpdate () {
		if (Input.GetKey (KeyCode.LeftShift)) {
			speed = 2f;
		} else {
			speed = 1f;
		}
		if (!isInteraction) {
			if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
				GetComponent<Rigidbody> ().velocity = new Vector2 (3f * speed, GetComponent<Rigidbody> ().velocity.y);
				GetComponent<SpriteRenderer> ().flipX = false;
			}
			if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
				GetComponent<Rigidbody> ().velocity = new Vector2 (-3f * speed, GetComponent<Rigidbody> ().velocity.y);
				GetComponent<SpriteRenderer> ().flipX = true;
			}
			if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
				GetComponent<Rigidbody> ().velocity = new Vector3 (GetComponent<Rigidbody> ().velocity.x, GetComponent<Rigidbody> ().velocity.y, 3f * speed);
			}
			if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
				GetComponent<Rigidbody> ().velocity = new Vector3 (GetComponent<Rigidbody> ().velocity.x, GetComponent<Rigidbody> ().velocity.y, -3f * speed);
			}
			if (Input.anyKey == false) {
				GetComponent<Animator> ().SetBool ("moving", false);
				GetComponent<Rigidbody> ().velocity = Vector3.zero;
			} else 
				GetComponent<Animator> ().SetBool ("moving", true);
		} else GetComponent<Animator> ().SetBool ("moving", false);
		camera.transform.position  = new Vector3 (GetComponent<Transform> ().transform.position.x, camera.transform.position.y, GetComponent<Transform> ().transform.position.z);
	}

	public void setIsInteraction(bool value) {
		isInteraction = value;
	}
}
