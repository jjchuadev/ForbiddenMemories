using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossJunkerController : MonoBehaviour {


	public int BossHealth = 50;
	private int HealthDifference = 0; //used to check if we entered next stage. Used in movement

	public float BossTimer = 10f; //how long the boss should be in the scene before changing
	public float BossDelay = 10f;
	public float BossSpeed = 10f;

	[HideInInspector]
	public bool BossIsWaiting = false;

	private SpriteRenderer sr;
	private Rigidbody2D rb;

	//Who do the enemies attack??
	public Transform target;


	//Enemy Spawning Variables
	public GameObject roombaPrefab;
	public GameObject hoverPrefab;
	public GameObject hoverShooterPrefab;
	public GameObject dolekPrefab;

	//How many enemies are allowed to be on the screen - Used to check children
	public int EnemyLimit = 5;
	public float spawnRate = 1;
	public float KillChildrenTimer = 10.0f;
	public Transform InstantiateFolder;

	private float timeToSpawn = 0;

	//Where do we move to
	private Vector2 currentDirection;

	private bool bossVisible = true; //Needed to not trigger the OnCollisionExit2d twice. Weird bug will check it later

	// Use this for initialization
	void Start () {
		sr = GetComponent<SpriteRenderer> ();
		rb = GetComponent<Rigidbody2D> ();
		currentDirection = Vector2.left;
		rb.velocity = new Vector2 (-1f * BossSpeed, transform.position.y);
		BossPosition ();
	}

	// Update is called once per frame
	void FixedUpdate () {
		MoveBigMama ();

	}

	void Update(){
		EnemySpawner ();
	}

	void OnTriggerEnter2D(Collider2D coll){
		if (coll.gameObject.tag == "Wall" && transform.position.x < 79) {
//			didWeMove = false;
			bossVisible = true;
		}
	}

	void OnTriggerExit2D(Collider2D coll){
//		Debug.Log ("COLLIDED WITH SOMETHING : " + coll.gameObject.tag);
		//We are now outside of the visible screen. Check if we should move the boss down
		//bossVisible used to be !didWeMove
		if (coll.gameObject.tag == "Wall" && transform.position.x >= 79 && bossVisible) {
//			BossHealth -= 10;
//			Debug.Log ("Collided With WALL");


			bossVisible = false;
			if ((HealthDifference - BossHealth) >= 10) {
				BossPosition ();
//				didWeMove = true;
			}
		}

//		if (coll.gameObject.name.Contains("Bullet")) {
//			this.BossHealth -= 5; //set to 5 change later
//			if (BossHealth <= 0 && transform.position.x < 75) {
//				rb.velocity = new Vector2 (0, 0);
//				GetComponent<BossJunkerController> ().enabled = false;
//			}
//		}
	}
		


	/// <summary>
	/// MY FUNCTIONS CONSIST OF THE FOLLOWING
	/// </summary>



	void MoveBigMama(){
		//Method that moves back and forth the values to check are hard coded and could be improved
		//but they work for now. If you make edits just comment out do not delete.

		//This must be checked in order to stop the boss from moving when it reaches 0 health
		if (BossHealth > 0) {

			if (currentDirection.x < 0 && transform.position.x < 37) {
				rb.velocity = new Vector2 (BossSpeed, transform.position.y);
				currentDirection = Vector2.right;
			} else if (currentDirection.x > 0 && transform.position.x > 80) {
				//stop BigMoma and wait
				BossTimer -= Time.fixedDeltaTime;
				if (BossTimer <= 0) {
					//start moving again and enter scene
					rb.velocity = new Vector2 (-1f * BossSpeed, transform.position.y);
					currentDirection = Vector2.left;
					BossTimer = BossDelay;
				} else {
					rb.velocity = new Vector2 (0, 0);
					//flag to not spawn enemies while waiting
					BossIsWaiting = true;
				}
			} else if (transform.position.x < 70 && BossIsWaiting) {
				//wait till the boss passes this mark
				BossIsWaiting = false;
			}
		} else {

			//if health reaches 0 need to move the boss back into the screen then kill
			if (currentDirection.x > 0 && transform.position.x > 80) {
				//stop BigMoma and wait
				BossTimer -= Time.fixedDeltaTime;
				if (BossTimer <= 0) {
					//start moving again and enter scene
					rb.velocity = new Vector2 (-1f * BossSpeed, transform.position.y);
					currentDirection = Vector2.left;
					BossTimer = BossDelay;
				} else {
					rb.velocity = new Vector2 (0, 0);
					//flag to not spawn enemies while waiting
					BossIsWaiting = true;
				}
			}
		}

	}

	void BossPosition(){
		if (BossHealth > 40) {
			//Boss is in its first phase
			transform.position = new Vector3 (this.transform.position.x, 9.5f, this.transform.position.z);
			HealthDifference = BossHealth;

		}
		else if (BossHealth <= 40 && BossHealth > 30) {
			// Boss moves to Y = 6.5
//			transform.Translate (Vector3.up * -3.0f);
			transform.position = new Vector3 (this.transform.position.x, 6.5f, this.transform.position.z);

			HealthDifference = BossHealth;
		} else if (BossHealth <= 30 && BossHealth > 20) {
			//Boss moves to Y = 3.5
//			transform.Translate(Vector3.up * -3.0f);
			transform.position = new Vector3 (this.transform.position.x, 3.5f, this.transform.position.z);

			HealthDifference = BossHealth;
		} else if (BossHealth <= 20 && BossHealth > 10) {
			//boss moves to y = .5
//			transform.Translate(Vector3.up * -3.0f);

			transform.position = new Vector3 (this.transform.position.x, 0.5f, this.transform.position.z);

			HealthDifference = BossHealth;
		} else if (BossHealth <= 10 && BossHealth > 0) {
			//boss moves to y = -2.5
//			transform.Translate(Vector3.up * -3.0f);
			transform.position = new Vector3 (this.transform.position.x, -2.5f, this.transform.position.z);
			HealthDifference = BossHealth;
		} else {
			//Boss Is Dead Destroy him
		}

	}

	void EnemySpawner(){
		if (BossHealth > 40) {
			//Boss Is almost full health Y = 9.5
//			StartCoroutine(EnemySpawner50());
			EnemySpawner50();
		} else if (BossHealth <= 40 && BossHealth > 30) {
			// Boss moves to Y = 6.5
//			StartCoroutine (EnemySpawner40 ());
			EnemySpawner40();
		} else if (BossHealth <= 30 && BossHealth > 20) {
			//Boss moves to Y = 3.5
			EnemySpawner30 ();
		} else if (BossHealth <= 20 && BossHealth > 10) {
			//boss moves to y = .5
			EnemySpawner20();
		} else if (BossHealth <= 10 && BossHealth > 0) {
			//boss moves to y = -2.5
			EnemySpawner10();
		} else {
			//Boss Is Dead Destroy him
		}
	}

	//Spawn Timers

	private void EnemySpawner50(){
		//only spawns roombas

		//counts how many children there should only be a max of 10 enemies at a time
		if (transform.childCount < EnemyLimit && !BossIsWaiting) {
			if (Time.time > timeToSpawn) {
				timeToSpawn = Time.time + 1 / spawnRate;
				spawnRoomba ();

			}
		}
	}

	private void EnemySpawner40(){
		//Spawns roombas and hovers at different intervals

		if (transform.childCount < EnemyLimit && !BossIsWaiting) {
			if (Time.time > timeToSpawn) {
				timeToSpawn = Time.time + 1 / spawnRate;
				spawnHover ();

			}
		}
	}


	private void EnemySpawner30(){
		if (transform.childCount < EnemyLimit && !BossIsWaiting) {
			if (Time.time > timeToSpawn) {
				timeToSpawn = Time.time + 1 / spawnRate;
				spawnHoverShooter ();
			}
		}
	}

	private void EnemySpawner20(){
		EnemyLimit = 6;
		if (transform.childCount < EnemyLimit && !BossIsWaiting) {
			if (Time.time > timeToSpawn) {
				timeToSpawn = Time.time + 1 / spawnRate;
				spawnHoverShooter ();
				spawnRoomba ();
			}
		}
	}

//	private IEnumerator EnemySpawner20(){
//		if (transform.childCount < EnemyLimit && !BossIsWaiting) {
//			if (Time.time > timeToSpawn) {
//				
//				spawnHoverShooter ();
//				yield return new WaitForSeconds (1.0f);
//				spawnRoomba ();
//				timeToSpawn = Time.time + 1 / spawnRate;
//			}
//		}
//		yield return false;
//	}

	private void EnemySpawner10(){
		EnemyLimit = 6;
		if (transform.childCount < EnemyLimit && !BossIsWaiting) {
			if (Time.time > timeToSpawn) {
				timeToSpawn = Time.time + 1 / spawnRate;
				spawnHoverShooter ();
				spawnRoomba ();
				spawnHover ();
			}
		}
	}

//	private IEnumerator EnemySpawner10(){
//		if (transform.childCount < EnemyLimit && !BossIsWaiting) {
//			if (Time.time > timeToSpawn) {
//
//				spawnHoverShooter ();
//				yield return new WaitForSeconds (1.0f);
//				spawnRoomba ();
//				yield return new WaitForSeconds (1.0f);
//				spawnHover ();
//				timeToSpawn = Time.time + 1 / spawnRate;
//			}
//		}
//		yield return false;
//	}


	//Enemies to Spawn

	private void spawnRoomba(){
		
		GameObject roomba = (GameObject) Instantiate (roombaPrefab, transform.position, this.transform.rotation,transform);
		roomba.GetComponent<EnemyAI> ().target = target;
		roomba.GetComponent<EnemyDropRate> ().InstantiateFolder = InstantiateFolder;
		//KILL CHILD AFTER CERTAIN TIME
		Destroy (roomba, KillChildrenTimer);
	}

	private void spawnHover(){

		GameObject hover = (GameObject) Instantiate (hoverPrefab, transform.position, this.transform.rotation,transform);
		hover.GetComponent<EnemyAI> ().target = target;
		hover.GetComponent<EnemyDropRate> ().InstantiateFolder = InstantiateFolder;
		//KILL CHILD AFTER CERTAIN TIME
		Destroy (hover, KillChildrenTimer);
	}

	private void spawnHoverShooter(){
		
		GameObject hoverShooter = (GameObject) Instantiate (hoverShooterPrefab, transform.position, this.transform.rotation,transform);
		hoverShooter.GetComponent<EnemyAI> ().target = target;	
		hoverShooter.GetComponentInChildren<ArmRotation> ().target = target;
		hoverShooter.GetComponentInChildren<EnemyWeapon> ().target = target;
		hoverShooter.GetComponent<EnemyDropRate> ().InstantiateFolder = InstantiateFolder;
		//KILL CHILD AFTER CERTAIN TIME

		Destroy (hoverShooter, KillChildrenTimer);
	}

	private void spawnDolek(){
		
		GameObject dolek = (GameObject) Instantiate (dolekPrefab, transform.position, this.transform.rotation,transform);
		dolek.GetComponent<EnemyAI> ().target = target;	
		dolek.GetComponentInChildren<ArmRotation> ().target = target;
		dolek.GetComponentInChildren<EnemyWeapon> ().target = target;
		dolek.GetComponent<EnemyDropRate> ().InstantiateFolder = InstantiateFolder;

		//KILL CHILD AFTER CERTAIN TIME
		Destroy(dolek,KillChildrenTimer);
	}




}
