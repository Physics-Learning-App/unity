using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class charMove : MonoBehaviour {
	[SerializeField] private SpriteRenderer characterSpriteRenderer;
	[SerializeField] private Rigidbody characterRigidBody;
	[SerializeField] private Animator characterAnimator;
	
	// Update is called once per frame
	void FixedUpdate () {
		if (Input.GetKey (KeyCode.RightArrow) || Input.GetKey (KeyCode.D)) {
			characterRigidBody.velocity = new Vector2 (3f, characterRigidBody.velocity.y);
			characterSpriteRenderer.flipX = false;
		}
		if (Input.GetKey (KeyCode.LeftArrow) || Input.GetKey (KeyCode.A)) {
			characterRigidBody.velocity = new Vector2 (-3f, characterRigidBody.velocity.y);
			characterSpriteRenderer.flipX = true;
		}
		if (Input.GetKey (KeyCode.UpArrow) || Input.GetKey (KeyCode.W)) {
			characterRigidBody.velocity = new Vector3 (characterRigidBody.velocity.x, characterRigidBody.velocity.y, 3f);
		}
		if (Input.GetKey (KeyCode.DownArrow) || Input.GetKey (KeyCode.S)) {
			characterRigidBody.velocity = new Vector3 (characterRigidBody.velocity.x, characterRigidBody.velocity.y, -3f);
		}
		if (characterRigidBody.velocity != new Vector3(0f, 0f, 0f)){
			characterAnimator.SetBool ("moving", true);
		} else characterAnimator.SetBool ("moving", false);
	}
}
