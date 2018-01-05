using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RestartGameDoorScript : MonoBehaviour {

	public GameObject child;
	public GameObject bossDoor;
	private SpriteRenderer sr;

	void Start(){
		sr = child.GetComponent<SpriteRenderer> ();
		sr.color = Color.green;
		BoxCollider2D[] colliders = bossDoor.GetComponents<BoxCollider2D> ();
		colliders [1].enabled = false;
	}

}
