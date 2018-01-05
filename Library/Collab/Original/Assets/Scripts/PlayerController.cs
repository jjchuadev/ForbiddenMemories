using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour {

	public GameObject bullet; 
	public Vector2 bulletVelocity; 
	private Vector2 front = new Vector2 (0.9f, 0.1f);
	private Vector2 up = new Vector2 (0.0f, 1.0f);

	private Rigidbody2D rb;
	private float growth = 3.0f; 

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
	private bool isGrounded; //jds
	private bool jump; //jds

	private bool onGround = true; // used as trigger to prevent unlimited jumping
	private bool onLadder = false; //used as trigger for animation and different moving mechanics 
	private bool hasLegs = false; //used as trigger to prevent jumping with no legs
	private bool hasWeapon = false; //used as trigger to prevent shooting with no arms/munitions
	private bool hasSpeedLegs = false; //used as trigger to change character speed
	private bool hasDoubleJumpLegs = false; //used as trigger to change character double jump ability
	private bool canDoubleJump = false;
	//condition used to change animation states once
	private bool switchOff = true;
	// Use this for initialization
	void Start () {

		rb = GetComponent<Rigidbody2D> ();
		sr = GetComponent <SpriteRenderer> ();
		anim = GetComponent<Animator> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {

		isGrounded = IsGrounded();
		
		float horizontal = Input.GetAxis("Horizontal"); //jds
		float vertical =  Input.GetAxis("Vertical"); //jds

		//check if the player has legs and change intial anim entry point

		Movement (horizontal); 
		ladderMovement (onLadder, vertical); 
		weaponController ();
		
		jump = false; //jds
	}

	//Handles all player's ground movement 
	private void Movement(float horizontal)
	{

		
		
		//if (Input.GetKey (KeyCode.Space) && onGround && hasLegs) {
		if (Input.GetButton("Jump") && onGround && hasLegs) {	
			jump = true; //jds
			onGround = false;
			rb.velocity = new Vector2 (0, jumpHeight);  
		}

		//movement mechanics for double jump legs
		if (Input.GetButton ("Jump") && hasDoubleJumpLegs) {
			if (IsGrounded ()) {
				rb.velocity = new Vector2 (0, jumpHeight);
			} else {
				if (canDoubleJump) {
					canDoubleJump = false;
					rb.velocity = new Vector2 (0, jumpHeight);
				}
			}
		}


		if (hasLegs && switchOff) {
			anim.Play ("standingIdle");
			switchOff = false;
		}

		if (!hasLegs && !switchOff) { 
			anim.Play ("idle-wheel");
			switchOff = true;
		}

		//if (Input.GetKey (KeyCode.A)) {
		if (horizontal < 0) { //jds
			//Animation flip
			sr.flipX = true;

			//check for legs to change animation
			anim.SetInteger ("State", 1);


			rb.velocity = new Vector2 (-normalSpeed, rb.velocity.y); 
		}

		//if (Input.GetKey (KeyCode.D)) {
		if (horizontal > 0) { //jds
			//animation flip
			sr.flipX = false;

			anim.SetInteger ("State", 1);

			rb.velocity = new Vector2 (normalSpeed, rb.velocity.y); 
		}
			
			//not moving idle
		//if (!Input.GetKey (KeyCode.D) && !Input.GetKey (KeyCode.A) && onGround) {
		if (horizontal<(idleTolerance) && horizontal >(-1*idleTolerance) && onGround) { //jds

			anim.SetInteger ("State", 0); //standing idle

			rb.velocity = new Vector2 (0, 0);
		}

		//Running mechanics for "D" key
		//if (Input.GetKey (KeyCode.D) && hasSpeedLegs == true) {
		if ((horizontal > 0) && hasSpeedLegs == true) { //jds
			sr.flipX = false;

			//needs to be fixed when running legs are added
			anim.SetInteger ("State", 1);


			rb.velocity = new Vector2 (runSpeed, rb.velocity.y); 
		}

		//Running mechanics for "A" key
		//if (Input.GetKey (KeyCode.A) && hasSpeedLegs == true) {
		if ((horizontal < 0) && hasSpeedLegs == true) { //jds
			sr.flipX = true;

			//fixed when running legs added
			anim.SetInteger ("State", 1);


			rb.velocity = new Vector2 (-runSpeed, rb.velocity.y); 
		}

	}


	//Handles all player's movement on a ladder 
	private void ladderMovement(bool onLadder, float vertical)
	{
		if (onLadder) {
		
			//replace running/idle sprite with climbing sprite
			//if (Input.GetKey (KeyCode.W)) {
			if (vertical > 0) { //jds
				anim.SetInteger ("State", 2);
				rb.velocity = new Vector2 (0, normalSpeed); 
			}

			//if (Input.GetKey (KeyCode.S)) {
			if (vertical < 0) { //jds
				anim.SetInteger ("State", 2);
				rb.velocity = new Vector2 (0, -normalSpeed); 
			}
		}
	}

	private void weaponController()
	{

		//if (Input.GetKeyDown(KeyCode.Mouse0) && hasWeapon) {
		if (Input.GetButton("Fire1") && hasWeapon) { //jds

			SoundManagerScript.PlaySound ("laser"); //jc

			if (Input.GetKeyDown (KeyCode.UpArrow)) {

				Debug.Log ("up arrow was pressed"); 
				GameObject bul = (GameObject)Instantiate (bullet, (Vector2)transform.position + up, Quaternion.Euler (0, 90, 0));
				bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bulletVelocity.y, bulletVelocity.x * transform.localScale.x); 

			} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
				
				Debug.Log("down arrow was pressed"); 
				front = new Vector2 (0.9f, 0.0f); 
				GameObject bul = (GameObject)Instantiate (bullet, (Vector2)transform.position + front, Quaternion.identity); 
				bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bulletVelocity.x * transform.localScale.x, bulletVelocity.y);  

			} else if (sr.flipX) {

				GameObject bul = (GameObject)Instantiate (bullet, (Vector2)transform.position - front, Quaternion.identity);
				bul.GetComponent<SpriteRenderer> ().flipX = true; 

				bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-bulletVelocity.x * transform.localScale.x, bulletVelocity.y);  

			} else {

				GameObject bul = (GameObject)Instantiate (bullet, (Vector2)transform.position + front, Quaternion.identity); 
				bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bulletVelocity.x * transform.localScale.x, bulletVelocity.y); 
			}


			}

		}

	//Handles all collisions that occur in-game (player between enviroment for now)
	private void OnCollisionEnter2D(Collision2D coll)
	{
		if (coll.gameObject.tag == "Environment") {
			onGround = true;
		}
		if (coll.gameObject.tag == "Legs") {

			//growPlayer ();

//			Debug.Log ("touching the legs"); 
			Destroy (coll.gameObject); 
			hasLegs = true; 
			SoundManagerScript.PlaySound ("pickUpItem"); //jc
//			Debug.Log ("Legs are gone"); 

		}


		if (coll.gameObject.tag == "Player" && coll.gameObject.name == "Speed Legs"){
			Destroy (coll.gameObject);
			hasSpeedLegs = true;
			SoundManagerScript.PlaySound ("pickUpItem"); //jc
		}

		if (coll.gameObject.name == "Double Jump Legs") {
			Destroy (coll.gameObject);
			SoundManagerScript.PlaySound ("pickUpItem");
			hasDoubleJumpLegs = true;
			canDoubleJump = true;
		}

		if (coll.gameObject.tag == "Weapon") {
			Debug.Log ("WEAPON");
			hasWeapon = true; 
			Destroy (coll.gameObject); 
			SoundManagerScript.PlaySound ("pickUpItem"); //jc
		}

		//jc
		if (coll.gameObject.tag == "MemoryChip") { 
			Destroy (coll.gameObject); 
			SoundManagerScript.PlaySound ("pickUpMemory");
			// need to add script to display message when memory chip is picked up
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


	// This is a fill funtion that should be replaced with a sprite change when the 
	// sprites are ready to implement; just shows that the leg collectable works 

	private void growPlayer()
	{
		Vector3 playerScale = this.transform.localScale; 
		playerScale.y += growth; 
		this.transform.localScale = playerScale; 
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
						//Debug.Log ("IsGrounded TRUE");
						canDoubleJump = true;
						return true;
					}
				
				}
			}
		}
		//Debug.Log ("IsGrounded FALSE");
		return false;
	}
	


}
