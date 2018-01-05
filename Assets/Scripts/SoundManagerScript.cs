using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Script to manage the sound effects in the game
// Will need to add more AudioClip vars as we add more sounds
//
// Finished sounds:
// - Player picks up item
// - Player picks up memory chip
// - Laser shots
// - Enemy hit by laser
//
// Still needed:
// - Enemy dies
// - Player gets hurt
//
//** please add more items to the list that you think are necessary

public class SoundManagerScript : MonoBehaviour {

	public static AudioClip pickUpItem, pickUpMemory, laser, enemyHitByLaser, playerDying, enemyDying, playerDamage;
	static AudioSource audioSrc;

	// Use this for initialization
	void Start () {

		pickUpItem = Resources.Load<AudioClip> ("sounds/pickUpItem");
		pickUpMemory = Resources.Load<AudioClip> ("sounds/pickUpMemory");
		laser = Resources.Load<AudioClip> ("sounds/laser");
		enemyHitByLaser = Resources.Load<AudioClip> ("sounds/enemyHitByLaser");
		playerDying = Resources.Load<AudioClip> ("sounds/dying");
		enemyDying = Resources.Load<AudioClip> ("sounds/dying");
		playerDamage = Resources.Load<AudioClip> ("sounds/playerDamage");

		audioSrc = GetComponent<AudioSource> ();
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

	public static void PlaySound (string clip) {

		switch (clip) {

		case ("pickUpItem"):
			audioSrc.PlayOneShot (pickUpItem);
			break;

		case ("pickUpMemory"):
			audioSrc.PlayOneShot (pickUpMemory);
			break;

		case ("laser"):
			audioSrc.PlayOneShot (laser);
			break;

		case ("enemyHitByLaser"):
			audioSrc.PlayOneShot (enemyHitByLaser);
			break;

		case ("playerDying"):
			audioSrc.PlayOneShot (playerDying);
			break;

		case ("enemyDying"):
			audioSrc.PlayOneShot (enemyDying);
			break;

		case ("playerDamage"):
			audioSrc.PlayOneShot (playerDamage);
			break;
		}

	}
}



// SOURCES

// freesound.org
// - Kastenfrosch: pickUpMemory
// - waveplay: pickUpItem
// - djfroyd: laser
// - Motion_S: enemyHitByLaser
// - startSong1, startSong2: Romariogrande
// - endSong1: levelclearer
// - playerDying/enemyDying: JoelAudio
// - playerDamage: SexyNakedBunny