using UnityEngine;
using System.Collections;
//using System.Collections.Generic;


public class CollisionTrigger : MonoBehaviour {

	private BoxCollider2D playerCollider;
	private BoxCollider2D playerArmCollider;
	[SerializeField]
	private BoxCollider2D platformCollider;
	[SerializeField]
	private BoxCollider2D platformTrigger;

	//NEW
	private GameObject player;

	void Start () {
		//NEW
		player = GameObject.Find("Player");
		playerCollider = player.GetComponent<BoxCollider2D>();
		playerArmCollider = player.transform.Find("Arm").gameObject.GetComponent<BoxCollider2D>();
		//
//		playerCollider = GameObject.Find("Player").GetComponent<BoxCollider2D>();
		Physics2D.IgnoreCollision(platformCollider, platformTrigger, true);

	}

	void OnTriggerEnter2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Physics2D.IgnoreCollision(platformCollider, playerCollider, true);
			//NEW BY JERRY
			Physics2D.IgnoreCollision(platformCollider, playerArmCollider, true);
		}
	}

	void OnTriggerExit2D(Collider2D other) {
		if (other.gameObject.tag == "Player") {
			Physics2D.IgnoreCollision(platformCollider, playerCollider, false);
			//New by Jerry
			Physics2D.IgnoreCollision(platformCollider, playerArmCollider, false);
		}
	}
	

}
