using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerData : MonoBehaviour {
	public void logout () {
		PlayerPrefs.DeleteAll ();
	}
}
