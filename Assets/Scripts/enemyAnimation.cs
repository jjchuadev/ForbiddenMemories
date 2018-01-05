using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimation : MonoBehaviour {

	public GameObject enemy; 

	//edit by jerry made public and serializable
	[SerializeField]
	public int health = 2; //default to 2 

	[SerializeField]
	public float deathTimer = 1.5f; //default to 3
	//end of edits

	private string enemyName; 
	private Animator ani; 

	// Use this for initialization
	void Start () {
		//commented out by jerry
//		health = enemy.GetComponent<EnemyAI> ().health; 
		enemyName = enemy.name; 
		ani = enemy.GetComponent<Animator> (); 
	
	}
	
	// Update is called once per frame
	void Update () {

		enemyAnimator (health, enemyName); 
		
	} 
		
	void enemyAnimator(int health, string name)
	{
		//changed to less than or equal to
		if (health <= 0) {
			//disable enemy AI Script

			this.gameObject.GetComponent<EnemyAI> ().enabled = false;

			//JC
			SoundManagerScript.PlaySound ("enemyDying");

			/////////////////////////

			if (enemyName.Contains ("hoverEnemy")) {

				//disables the arm since it keeps shooting and is there when the animation plays
				if (enemyName.Contains ("hoverEnemyShooter")) {
					enemy.transform.GetChild (0).gameObject.SetActive (false);
				}
				ani.Play ("hovdeath"); 
			} else if (enemyName.Contains ("palekEnemy")) {
				if (enemyName.Contains ("palekEnemyShooter")) {
					enemy.transform.GetChild (0).gameObject.SetActive (false);
				}
				ani.Play ("palekdeath"); 
			} else {
				ani.Play ("roombadeath"); 
			} 

			//edit by jerry Add timer here
			deathTimer -= Time.deltaTime;
			if (deathTimer <= 0) {
				Destroy (this.gameObject);
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
