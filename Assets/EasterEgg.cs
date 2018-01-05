using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class EasterEgg : MonoBehaviour {

	[SerializeField]
	string	loadLevel;


	//JC
	private BoxCollider2D coll;

	// Use this for initialization
	void Start () {
		coll = GetComponent<BoxCollider2D> ();
	}

	void OnTriggerEnter2D(Collider2D other){
		if (other.CompareTag("Player")){
			SceneManager.LoadScene(loadLevel);
		}
	}


}
