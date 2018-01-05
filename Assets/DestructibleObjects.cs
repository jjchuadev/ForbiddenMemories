using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestructibleObjects : MonoBehaviour {

	public GameObject objectPrefab; 

	[SerializeField]
	public int health = 2; //default to 2 

	[SerializeField]
	public float deathTimer = 1.5f; //default to 3


	// Update is called once per frame
	void Update () {

		destroyObject(health); 

	} 

	void destroyObject(int health)
	{
		if (health <= 0) {
			deathTimer -= Time.deltaTime;
			if (deathTimer <= 0) {
				Destroy (objectPrefab);
			}
		}
	}





	void OnCollisionEnter2D(Collision2D col)
	{
		if (col.gameObject.name.Contains("Bullet")) {
			this.health -= 1; 
			SoundManagerScript.PlaySound ("enemyHitByLaser"); //jc
		}
	}


}
