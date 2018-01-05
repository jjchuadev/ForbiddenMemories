using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyWeapon : MonoBehaviour {

	public Transform BulletPrefab;

	public float fireRate = 1;
	public float Damage = 1; //change to 1
	public LayerMask whatToHit; // Ignore raycast and player or in our case enemy (add enemy layer)
	public int ClosestDistanceTillFire = 15;
//	public float TimeToSpawnBullet = 0;

	public Transform target = null;

	float timeToFire = 0;
	Transform firePoint;
	// Use this for initialization
	void Awake () {
		firePoint = transform.Find ("FirePoint");
		if (firePoint == null) {
			Debug.LogError ("No Fire Point");

		}
	}
	
	// Update is called once per frame, change to update if fail
	void Update () {
		if (fireRate == 0) {
//			Shoot ();
		} 
		else {
			if (Time.time > timeToFire) {
				timeToFire = Time.time + 1 / fireRate;
				Shoot ();
			}
		}
			
	}
		

	void Shoot(){
		
		Vector2 targetPosition = new Vector2 (target.position.x, target.position.y);
		Vector2 firePointPosition = new Vector2 (firePoint.position.x, firePoint.position.y);

		RaycastHit2D hit = Physics2D.Raycast (firePointPosition, targetPosition - firePointPosition, ClosestDistanceTillFire, whatToHit);

//		FireBullet ();

		if (hit.collider != null) {
			if (hit.collider.tag == "Player") {
				Debug.Log ("WE HIT THE PLAYER");
				FireBullet ();
			}
		}

		Debug.DrawLine (firePointPosition, (targetPosition - firePointPosition) * 100);

	}

	void FireBullet(){
		//if you uncomment the first one then the bullets follow you. The second just shoots in a straight line
//		Instantiate (BulletPrefab, firePoint.position, firePoint.rotation,transform);
		Instantiate (BulletPrefab, firePoint.position, firePoint.rotation);

	}

}
