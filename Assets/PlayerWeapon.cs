using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerWeapon : MonoBehaviour {

	public GameObject BulletPrefab; // what our bullet looks like and how deadly it is

	public GameObject Bullet_Emitter; // the position the bullets originate from

	public float BulletSpeed = 10; // force added to the bullet
	public float fireRate = 0; // single fire if 0, rapid fire if not
    //	public float Damage = 1; //change to 1
	//  public LayerMask whatToHit; // Ignore raycast and player or in our case enemy (add enemy layer)
	//  public int BulletDistance = 50;
	//	public float TimeToSpawnBullet = 0;

	//place in time when our next shot will be Better for intervals
	float timeToFire = 0;
	Transform firePoint;
	// Use this for initialization
	void Awake () {
		firePoint = transform.Find ("playerFirePoint");
		if (firePoint == null) {
			Debug.LogError ("No Fire Point");

		}
	}

	// Update is called once per frame, change to update if fail
	void Update () {
		//If 0 checks everytime we click. If not 0 it shoots much faster
		if (fireRate == 0) {
			if (Input.GetKeyDown (KeyCode.Mouse0)) {
				Shoot ();
			}
		} 
		else {
			if (Input.GetKey (KeyCode.Mouse0)) {
				if (Time.time > timeToFire) {
					//1/firerate is the delay
					timeToFire = Time.time + 1 / fireRate;
					Shoot ();
				}
			}
		}
	}

	void Shoot(){
		FireBullet ();
	}

	void FireBullet(){
		SoundManagerScript.PlaySound ("laser"); //laser sound

		GameObject Temporary_Bullet_Handler; //make a new bullet
		Temporary_Bullet_Handler = Instantiate (BulletPrefab, Bullet_Emitter.transform.position,Bullet_Emitter.transform.rotation) as GameObject;

		Rigidbody2D Temporary_RigidBody; //lets add some force
		Temporary_RigidBody = Temporary_Bullet_Handler.GetComponent<Rigidbody2D>();
		Temporary_RigidBody.AddForce(transform.right * BulletSpeed);

		Destroy (Temporary_Bullet_Handler, 3.0f); //bye bye in 3 seconds
	}
}