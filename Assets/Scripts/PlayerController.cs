using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour {

	public GameObject Arm;

	public GameObject bullet; 
	public Vector2 bulletVelocity; 
	public float deathTimer = 3.0f;
	//private Vector2 front = new Vector2 (0.9f, 0.1f);
	//private Vector2 up = new Vector2 (0.0f, 1.0f);

	private Rigidbody2D rb;
	private Vector2 characterVelocity;

	//Using this for animations
	private SpriteRenderer sr;
	private Animator anim;

	[SerializeField]
	private float normalSpeed;
	[SerializeField]
	private float runSpeed;
	[SerializeField]
	private float idleTolerance;

	[SerializeField]
	private float jumpHeight;
	
	[SerializeField]
	private Transform[] groundPoints; //jds
	
	[SerializeField]
	private float groundRadius; //jds
	
	[SerializeField]
	private LayerMask whatIsGround; //jds

//	public Transform fireLeft; 
//	public Transform fireRight; 
//	public Transform fireUp; 

	private int jumpCount = 0;

	private bool onGround = true; // used as trigger to prevent unlimited jumping
	private bool onLadder = false; //used as trigger for animation and different moving mechanics 
	private bool hasLegs = false; //used as trigger to prevent jumping with no legs
	private bool hasWeapon = false; //used as trigger to prevent shooting with no arms/munitions
	private bool hasSpeedLegs = false; //used as trigger to change character speed
	private bool hasDoubleJumpLegs = false; //used as trigger to change character double jump ability
	private bool canDoubleJump = false;
	//condition used to change animation states once
	private bool switchOff = true;

	[SerializeField]
	private int enhancements; //health 
	// Use this for initialization
	void Start () {		
		rb		= GetComponent<Rigidbody2D> ();
		sr		= GetComponent<SpriteRenderer> ();
		anim	= GetComponent<Animator> ();
		enhancements = 1;
		loadPlayerState ();

	}

	//When the player hits the level 2 trigger, we call this function and set the beenToLvl2 in the 
	//GameMasterControl script to true because the player is going to level 2
	public void savePlayerState()
	{
		GameMasterControl.pEnhancements = enhancements; 
		GameMasterControl.pHasLegs = hasLegs; 
		GameMasterControl.pHasSpeedLegs = hasSpeedLegs; 
		GameMasterControl.pHasDoubleJumpLegs = hasDoubleJumpLegs; 
		GameMasterControl.pHasWeapon = hasWeapon;
	}

	public void loadPlayerState()
	{
		//Debug.Log ("***loadPlayerState() called ***");
		enhancements = GameMasterControl.pEnhancements;
		hasLegs = GameMasterControl.pHasLegs; 
		hasSpeedLegs = GameMasterControl.pHasSpeedLegs; 
		hasDoubleJumpLegs = GameMasterControl.pHasDoubleJumpLegs; 
		hasWeapon = GameMasterControl.pHasWeapon; 
		if (hasWeapon) {
			Arm.SetActive (true);
			Arm.transform.position = new Vector3 (Arm.transform.position.x, Arm.transform.position.y + 0.4f, 0);
		}
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		savePlayerState ();

		if (IsGrounded()) {
			jumpCount = 0;
		} //reset jumpCount
		
		float horizontal = Input.GetAxisRaw("Horizontal"); //jds
		float vertical =  Input.GetAxisRaw("Vertical"); //jds

		//check if the player has legs and change intial anim entry point

		Movement (horizontal); 
		ladderMovement (onLadder, vertical);
		//Debug.Log ("enhancements = " + enhancements);
//		Debug.Log("&&&&&&&&&&&&&&&&&&&&&&&Stories Collected" + storyCollection);
	}

	void Update()	{
		if (enhancements < 0) {
			//set a timer so the screen doesnt appear immediately 
			deathTimer -= Time.deltaTime;
			if (deathTimer <= 0) {
				enhancements = 0;
				hasLegs = false;
				hasWeapon = false;
				hasSpeedLegs = false;
				hasDoubleJumpLegs = false;
				savePlayerState ();
				SceneManager.LoadScene ("Game Over");
			}
		}
	}

	//Handles all player's ground movement 
	private void Movement(float horizontal)
	{
		if (Input.GetButton ("Jump")) {
			if (onGround && (jumpCount==0) && (hasLegs || hasSpeedLegs || hasDoubleJumpLegs)) {	
				onGround = false; //we've jumped, cannot be on ground
				jumpCount = 1;
				characterVelocity = new Vector2 (rb.velocity.y, 10);
				rb.velocity = characterVelocity;
				//rb.velocity = new Vector2 (0, jumpHeight);
				//Debug.Log ("***JUMP***" + jumpCount);
			}
			else
				if ((rb.velocity.y<0) && !onGround && hasDoubleJumpLegs && (jumpCount == 1)) {
				rb.velocity = new Vector2 (0, jumpHeight);
				jumpCount++;
				//Debug.Log ("****DOUBLE JUMPED****" + jumpCount);
			}
		}

		if (hasLegs && switchOff) {
//			anim.Play ("standingIdle");
			anim.Play ("idle-legs");
			switchOff = false;
		}

		if (!hasLegs && !switchOff) { 
			anim.Play ("idle-nolegs");
			switchOff = true;
		}

		//Jogging mechanics for "A" key
		if (horizontal < 0) {
			//Animation flip
			sr.flipX = true; //=true;
			//check for gun to change animation

//			if (hasWeapon) {
//				//walking with gun animation
//				anim.SetInteger("State", 3);
//			} else {
//				anim.SetInteger ("State", 1);
//			}
			anim.SetInteger("State",1);

			rb.velocity = new Vector2 (-normalSpeed, rb.velocity.y);

		}

		//Jogging mechanics for "D" key
		if (horizontal > 0) {
			//animation flip
			sr.flipX = false;
//			if (hasWeapon) {
//				//walking with gun animation
//				anim.SetInteger("State", 3);
//			} else {
//				anim.SetInteger ("State", 1);
//			}
			anim.SetInteger("State",1);
			rb.velocity = new Vector2 (normalSpeed, rb.velocity.y); 



		}
			
		//not moving idle
		if (horizontal<(idleTolerance) && horizontal >(-1*idleTolerance) && onGround) {
			anim.SetInteger ("State", 0); //standing idle
			rb.velocity = new Vector2 (0, rb.velocity.y);
		}

		//Running mechanics for "D" key
		if ((horizontal > 0) && hasSpeedLegs == true) {
			sr.flipX = false;
			//needs to be fixed when running legs are added
//			if (hasWeapon) {
//				//walking with gun animation
//				anim.SetInteger ("State", 3);
//			} else {
//				anim.SetInteger ("State", 1);
//			}
			anim.SetInteger("State",1);
			rb.velocity = new Vector2 (runSpeed, rb.velocity.y); 
			
		}

		//Running mechanics for "A" key
		if ((horizontal < 0) && hasSpeedLegs == true) {
			sr.flipX = true;

			//fixed when running legs added
//			if (hasWeapon) {
//				//walking with gun animation
//				anim.SetInteger("State", 3);
//			} else {
//				anim.SetInteger ("State", 1);
//			}
			anim.SetInteger("State",1);
			rb.velocity = new Vector2 (-runSpeed, rb.velocity.y); 
			
		}
	}


	//Handles all player's movement on a ladder 
	private void ladderMovement(bool onLadder, float vertical)
	{
		if (onLadder) {
		
			//replace running/idle sprite with climbing sprite
			if (vertical > 0) {
				anim.SetInteger ("State", 2);
				rb.velocity = new Vector2 (0, normalSpeed); 
			}

			if (vertical < 0) {
				anim.SetInteger ("State", 2);
				rb.velocity = new Vector2 (0, -normalSpeed); 
			}
		}
	}

//	private void weaponController()
//	{
//		if ((Input.GetKeyDown(KeyCode.M) || Input.GetKeyDown(KeyCode.Mouse0)) && hasWeapon) {
//			//edit by jerry to change shooting animation
////			anim.SetBool("Shooting",true);
//			//
//
//			SoundManagerScript.PlaySound ("laser"); //jc
//
//			if (Input.GetKey (KeyCode.W)) {
//
//				//bullet stops at fireUp transfom object for some reason
//				GameObject bul = (GameObject)Instantiate (bullet, fireUp.position, Quaternion.identity); 
//				bul.transform.Rotate (0, 0, 90.0f); 
//				bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bulletVelocity.y, bulletVelocity.x); 
//			}
//
//			else if (sr.flipX) {
//				GameObject bul = (GameObject)Instantiate (bullet, fireLeft.position, Quaternion.identity); 
//				bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-bulletVelocity.x, bulletVelocity.y); 
//			} 
//			else {
//				GameObject bul = (GameObject)Instantiate (bullet, fireRight.position, Quaternion.identity); 
//				bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bulletVelocity.x, bulletVelocity.y); 
//			}
//
//
//		}
		//EDIT BY JERRY
//		if (Input.GetKeyUp (KeyCode.Mouse0) && hasWeapon) {
//			anim.SetBool ("Shooting", false);
//		}
		//

//	}

	//Handles all collisions that occur in-game (player between enviroment for now)
private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Environment") {
			onGround = true;
		}

		//EDIT by JERRY - include checks of double and speed legs within legs tag
		if (coll.gameObject.tag == "Legs") {



//			fireLeft.transform.position = new Vector3(fireLeft.transform.position.x, fireLeft.transform.position.y + 0.4f, 0); 
//			fireRight.transform.position = new Vector3 (fireRight.transform.position.x, fireRight.transform.position.y + 0.4f, 0); 

			//EDIT BY JERRY MOVE THE ENTIRE ARM
			if (!hasLegs) {
				Arm.transform.position = new Vector3 (Arm.transform.position.x, Arm.transform.position.y + 0.4f, 0);
			}

			if (coll.gameObject.name.Contains("Speed Legs")) {
				//EDIT BY JERRY
				//coll.gameObject.tag == "Legs" already destroys the legs dont need to destroy again
				//just change flags

				Destroy (coll.gameObject);

				hasSpeedLegs = true;
				//Mutually exclusive powerups

				//EDIT BY JERRY - hasLegs should always be set to true if the player
				//picked up any sort of legs. hasLegs should be set on collision with coll.gameobject.tag == Legs
				hasLegs = hasSpeedLegs;
				hasDoubleJumpLegs = false;
				++enhancements; 
				SoundManagerScript.PlaySound ("pickUpItem"); //jc
			} else if (coll.gameObject.name.Contains("Double Jump Legs")) {
				//EDIT BY JERRY
				//coll.gameObject.tag == "Legs" already destroys the legs dont need to destroy again
				//just change flags

				Destroy (coll.gameObject);

				SoundManagerScript.PlaySound ("pickUpItem");
				++enhancements;
				hasDoubleJumpLegs = true;
				canDoubleJump = true;
				//Mutually exclusive powerups

				//EDIT BY JERRY - hasLegs should always be set to true if the player
				//picked up any sort of legs. hasLegs should be set on collision with coll.gameobject.tag == Legs
				hasLegs = hasDoubleJumpLegs; //changed to be whatever legs it has
				hasSpeedLegs = false;
			} else {
				Destroy (coll.gameObject); 
				hasLegs = true; 
				hasSpeedLegs = false;
				hasDoubleJumpLegs = false;
				++enhancements; 
				SoundManagerScript.PlaySound ("pickUpItem"); //jc
			}

//			Destroy (coll.gameObject); 
//			hasLegs = true; 
////			hasSpeedLegs = false;
////			hasDoubleJumpLegs = false;
//			if (enhancements < 4){
//				enhancements ++; 
//			}
//			SoundManagerScript.PlaySound ("pickUpItem"); //jc

		}

//		if (coll.gameObject.tag == "Legs") {
//
//			//growPlayer ();
//
//			Destroy (coll.gameObject); 
//			hasLegs = true; 
//			hasSpeedLegs = false;
//			hasDoubleJumpLegs = false;
//			if (enhancements < 4){
//				enhancements ++; 
//			}
//			SoundManagerScript.PlaySound ("pickUpItem"); //jc
//
//		}

		if (coll.gameObject.tag == "Enemy") {
			enhancements -= 1;

			//JC
			SoundManagerScript.PlaySound ("playerDamage");

			if (enhancements < 0) // so we die if we have -1 enhancement
			{
				anim.SetBool ("PlayerDead", true);

				anim.Play ("playerdeath"); //death animation is played but object is not destroyed
				//Destroy (this.gameObject);

				//JC
				//Sound clip when player dies
				SoundManagerScript.PlaySound ("playerDying");
			}

			if (enhancements < 3) {
				if (canDoubleJump || hasSpeedLegs || hasDoubleJumpLegs) {
					canDoubleJump = false; 
					hasSpeedLegs = false; 
					hasDoubleJumpLegs = false;
					//replace sprite with apropriate sprite 
				} 
			}
			if (enhancements < 2) {
				if (hasLegs && !hasSpeedLegs && !hasDoubleJumpLegs) {
					anim.Play ("idle-nolegs");
					Arm.transform.position = new Vector3 (Arm.transform.position.x, Arm.transform.position.y - 0.4f, 0);
					hasLegs = false; 

				} 
			}

			if (hasWeapon && enhancements == 0) {
				Arm.SetActive (false); //disable the arm if it doesnt have it
				hasWeapon = false;

			}

		}


//		if (coll.gameObject.name == "Speed Legs"){
//			//EDIT BY JERRY
//			//coll.gameObject.tag == "Legs" already destroys the legs dont need to destroy again
//			//just change flags
//
//			//Destroy (coll.gameObject);
//
//			hasSpeedLegs = true;
//			//Mutually exclusive powerups
//
//			//EDIT BY JERRY - hasLegs should always be set to true if the player
//			//picked up any sort of legs. hasLegs should be set on collision with coll.gameobject.tag == Legs
//			hasLegs = hasSpeedLegs;
//			hasDoubleJumpLegs = false;
//
//			if (enhancements < 4){
//			enhancements ++; 
//			}
//			SoundManagerScript.PlaySound ("pickUpItem"); //jc
//		}
//
//		if (coll.gameObject.name == "Double Jump Legs") {
//			//EDIT BY JERRY
//			//coll.gameObject.tag == "Legs" already destroys the legs dont need to destroy again
//			//just change flags
//
//			//Destroy (coll.gameObject);
//
//			SoundManagerScript.PlaySound ("pickUpItem");
//			if (enhancements < 4){
//				enhancements ++; 
//			} 
//			hasDoubleJumpLegs = true;
//			canDoubleJump = true;
//			//Mutually exclusive powerups
//
//			//EDIT BY JERRY - hasLegs should always be set to true if the player
//			//picked up any sort of legs. hasLegs should be set on collision with coll.gameobject.tag == Legs
//			hasLegs = hasDoubleJumpLegs; //changed to be whatever legs it has
//			hasSpeedLegs = false;
//		}

		if (coll.gameObject.tag == "Weapon") {
			hasWeapon = true; 
			++enhancements; 
			Destroy (coll.gameObject); 
			SoundManagerScript.PlaySound ("pickUpItem"); //jc
			Arm.SetActive(true);
		}

		//jc
		if (coll.gameObject.tag == "MemoryChip") { 
			Destroy (coll.gameObject); 
			SoundManagerScript.PlaySound ("pickUpMemory");
			 //need to add script to display message when memory chip is picked up
		}
		if (enhancements > 6) {
			enhancements = 6;
		}
			
	}


	//Handles all collision that continuously occur in-game (i.e. when the player is on a ladder)
	private void OnCollisionStay2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Environment" && coll.gameObject.name == "Ladder") {

			rb.gravityScale = 0; 
			onLadder = true; 
		}
	}

	//Handles any commands that occur after two objects stop colliding (i.e. player is off the ladder)
	private void OnCollisionExit2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Environment" && coll.gameObject.name == "Ladder")
		{
			rb.gravityScale = 2; 
			onLadder = false; 
		}

	}
		
	private bool IsGrounded() //jds
	{
		if (rb.velocity.y <= 0) //falling state <0
		{
			foreach (Transform point in groundPoints)
			{
				Collider2D[] colliders = Physics2D.OverlapCircleAll(point.position, groundRadius, whatIsGround);
				for (int i = 0; i < colliders.Length; i++)
				{
					if (colliders[i].gameObject != this.gameObject)
					{
						canDoubleJump = true;
						return true;
					}
				
				}
			}
		}
		return false;
	}




}
