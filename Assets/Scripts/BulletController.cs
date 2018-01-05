using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

	public GameObject explosion;
	//public int moveSpeed = 10;//unused now

	// Update is called once per frame
	void Update () {
	//Velocity Now controlled in PlayerWeapon.cs
	//transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
	}

	void OnCollisionEnter2D(Collision2D col)
	{
		GameObject veryTemp;

		if (col.gameObject.tag == "Player") {
			Physics2D.IgnoreCollision (col.gameObject.GetComponent<CircleCollider2D> (),
				this.GetComponent<Collider2D> ()); 
		} 
		veryTemp = Instantiate(explosion, this.transform.position, this.transform.rotation);
		Destroy (this.gameObject);
		Destroy (veryTemp,33f);
	}
}
