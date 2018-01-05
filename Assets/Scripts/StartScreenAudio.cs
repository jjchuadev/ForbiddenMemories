using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartScreenAudio : MonoBehaviour {

	public AudioSource audioSrc;
	public AudioClip song1, song2; 

	// Use this for initialization
	void Start () {
		if (Random.value < 0.5f) {
			audioSrc.clip = song1;
		} else {
			audioSrc.clip = song2;
		}
		audioSrc.Play ();
	}
	
	// Update is called once per frame
	void Update () {
		
	}


	//Audio Clip Sources:
	// Freesound.org
	// startSong1, startSong2: Romariogrande
	// endSong1: levelclearer
}
