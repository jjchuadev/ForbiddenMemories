using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyShoot : MonoBehaviour {


	private SpriteRenderer sr;
	public Transform target = null;

	public GameObject bullet; 
	public Vector2 bulletVelocity;

	public Transform fireLeft; 
	public Transform fireRight;


	public Sprite bulletColor; 


	//variable of player position

	//used for the interval of shooting
	[SerializeField]
	float timer; 

	[SerializeField]
	float delay ; //3f

	// Use this for initialization
	void Start () {

		//get component from parent
		sr = GetComponent<SpriteRenderer>();
//		StartCoroutine (LookAtPlayer ());
	}

	
	// Update is called once per frame
	void Update() {

//		if (target == null)
//			return;

		timer -= Time.deltaTime;
		if (timer <= 0) {
			Debug.Log ("timer went off"); 
			weaponController ();
			timer = delay;
			return; 
		}
	}

	private void weaponController()
	{
		Debug.Log ("in the weapon cotroller"); 
		SoundManagerScript.PlaySound ("laser"); //jc

		if (sr.flipX) {
			//fireRight.position
			GameObject bul = (GameObject)Instantiate (bullet, fireRight.position, ArmRotation.armRotation); 
			bul.GetComponent<SpriteRenderer> ().sprite = bulletColor; 
			Debug.Log ("about to shoot flipx"); 
			bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (bulletVelocity.x, bulletVelocity.y); 
			Debug.Log ("projectile shot flipx"); 

		} 
		else {
			//fireLeft.position
			GameObject bul = (GameObject)Instantiate (bullet, fireLeft.position, ArmRotation.armRotation); 
			bul.GetComponent<SpriteRenderer> ().sprite = bulletColor; 			
			Debug.Log ("about to shoot"); 
			bul.GetComponent<Rigidbody2D> ().velocity = new Vector2 (-bulletVelocity.x, bulletVelocity.y); 
			Debug.Log ("projectile shot"); 










		}
			

	}

}
