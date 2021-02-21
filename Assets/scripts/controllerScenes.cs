using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class controllerScenes : MonoBehaviour {
	public void changeScreenTo(string namescene) {
		Time.timeScale = 1f;
		SceneManager.LoadScene(namescene);
	}
}
