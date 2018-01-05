using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossDeathManager : MonoBehaviour {


	[SerializeField]
	public float deathTimer = 3f; //default to 3
	//end of edits
	public int health;
	//can only get hit this amount per stage
	public int hitLimit = 5;

	public GameObject invisibleDoorTrigger;
	private bool isBossWaiting;
	BossJunkerController BossController;

//	private bool isBossWaiting;
//	private Animator anim; 

	// Use this for initialization
	void Start () {
//		anim = GetComponent<Animator> ();
		BossController = GetComponent<BossJunkerController>();
		isBossWaiting = BossController.BossIsWaiting;
		health = BossController.BossHealth;
//		health = GetComponent<BossJunkerController>().BossHealth;
//		isBossWaiting = GetComponent<BossJunkerController> ().BossIsWaiting;
	}
	
	// Update is called once per frame
	void Update () {
		enemyAnimator(health);
		BossHitCounter ();
	}

	void enemyAnimator(int health)
	{
		//changed to less than or equal to
		//the transform is check to make sure the boss is visible

		if (health <= 0 && transform.position.x < 75) {
			//disable enemy AI Script

//			this.gameObject.GetComponent<BossJunkerController> ().enabled = false;


			/////////////////////////
			//Add anim.Play("BossDeath")



			//edit by jerry Add timer here
			deathTimer -= Time.deltaTime;
			if (deathTimer <= 0) {
				invisibleDoorTrigger.SetActive (true);
				Destroy (this.gameObject);
			}

		}
	}


	void OnTriggerEnter2D(Collider2D col)
	{
		if (hitLimit > 0) {
			if (col.gameObject.name.Contains ("Bullet")) {
				this.health -= 2; //set to 5 change later
				hitLimit -= 1;
				SoundManagerScript.PlaySound ("enemyHitByLaser"); //jc
				BossController.BossHealth = this.health;
				if (health <= 0 && transform.position.x < 75) {
					BossController.GetComponent<Rigidbody2D> ().velocity = new Vector2 (0, 0);
					GetComponent<BossJunkerController> ().enabled = false;
				}
			}
		}

	}

	//only a certain amount of hits per round
	void BossHitCounter(){

		isBossWaiting = BossController.BossIsWaiting;
		if (isBossWaiting) {
			hitLimit = 5;
		}
	}

}
