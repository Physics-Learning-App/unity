﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class controllerMusic : MonoBehaviour {
	public AudioClip[] bgmList;
	AudioSource thisAudioSource;

	void Start() {
		thisAudioSource = GetComponent<AudioSource> ();
	}


	public void setBgm(int indexBgmList) {
		thisAudioSource.Stop ();
		thisAudioSource.clip = bgmList [indexBgmList];
		thisAudioSource.Play ();
	}
}