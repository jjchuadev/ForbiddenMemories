using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour {


	private Vector3 offset; 
	public GameObject player; 
	// Use this for initialization
	void Start () {

		offset = this.transform.position - player.transform.position; 
	}
	
	// Update is called once per frame
	void Update () {

		//Sets the camera in the center of the player; camera always follows player
		this.transform.position = player.transform.position + offset;

		// IDEA: game starts and the players is positioned on the left of the screen;
		// player moves to center of screen and camera starts to follow it for every new room/level 
		// if we have multiple rooms/levels 

	}
}
