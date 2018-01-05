using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveBullet : MonoBehaviour {

	public int moveSpeed = 230;

	// Update is called once per frame
	void Update () {
		transform.Translate (Vector3.right * Time.deltaTime * moveSpeed);
//		Destroy (this.gameObject, 1);

	}

	private void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.tag == "Enemy") {
//			Debug.Log ("WE HIT OURSELVES");
//			GameObject enemy = GameObject.FindGameObjectWithTag ("Enemy"); 
//			if (enemy != null) {
			Physics2D.IgnoreCollision (col.gameObject.GetComponent<Collider2D> (),
				this.GetComponent<Collider2D> ()); 
		
//			}
		} else {
			Destroy (gameObject);
		}
	}
}
