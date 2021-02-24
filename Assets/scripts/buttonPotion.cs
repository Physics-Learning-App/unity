using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class buttonPotion : MonoBehaviour {
	public GameObject potionBuy, potionUse;
	bool isPressed = false;

	public void clickButton(){
		if (!isPressed) {
			isPressed = true;
			potionBuy.SetActive (true);
			potionUse.SetActive (true);
		} else {
			isPressed = false;
			potionBuy.SetActive (false);
			potionUse.SetActive (false);
		}
		Debug.Log(isPressed);
	}
}
