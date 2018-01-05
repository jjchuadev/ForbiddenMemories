using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitiateBossFight : MonoBehaviour {

	public GameObject BossJunker;
	public BoxCollider2D NoEscapeCollider;
	public GameObject playerCamera;
	public GameObject bossCamera;
	[HideInInspector]
	public bool didTheCameraChange = false;
	private bool cameraToggle = true;
	// Use this for initialization
	void Start () {
		NoEscapeCollider.enabled = false;
		bossCamera.SetActive (false);
	}


//	void OnTriggerEnter2D(Collider2D coll){
//		//the player is entering the boss fight
//
//		if (coll.gameObject.tag == "Player") {
//			Debug.Log ("ALL HELL GON BREAK LOOSE");
//			NoEscapeCollider.enabled = true;
//			BossJunker.SetActive (true);
//
//		}
//	}

	void OnTriggerExit2D(Collider2D coll){
		if (coll.gameObject.tag == "Player" && coll.gameObject.transform.position.x > 35.2f) {
			if (BossJunker != null) {
				Debug.Log ("START ENEMY");
				NoEscapeCollider.enabled = true;

				if (!didTheCameraChange) {
					bossCamera.SetActive (!bossCamera.activeSelf);
					playerCamera.SetActive (!playerCamera.activeSelf);
					didTheCameraChange = true;
				}
				BossJunker.SetActive (true);
			} else {
				if (!cameraToggle) {
					Debug.Log ("BOSS CAMERA");
					bossCamera.SetActive (!bossCamera.activeSelf);
					playerCamera.SetActive (!playerCamera.activeSelf);
					cameraToggle = !cameraToggle;
				}

			}
		}else if (coll.gameObject.tag == "Player" && coll.gameObject.transform.position.x < 34) {
			if (cameraToggle) {
				Debug.Log ("PLAYER CAMERA");
				bossCamera.SetActive (!bossCamera.activeSelf);
				playerCamera.SetActive (!playerCamera.activeSelf);
				cameraToggle = !cameraToggle;
			}
		}
	}

}
