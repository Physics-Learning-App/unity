using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class controllerLoadingScreen : MonoBehaviour {
	public controllerScenes remoteScene;
	public bool isLoadingFinished = false;
	public Text gettingUserData;

	void FixedUpdate(){
		if (isLoadingFinished)
			gettingUserData.text = "Click anywhere to continue..";
	}

	public void continueGame(){
		if (isLoadingFinished) {
			remoteScene.changeScreenTo ("gameplay");
		}
	}
}
