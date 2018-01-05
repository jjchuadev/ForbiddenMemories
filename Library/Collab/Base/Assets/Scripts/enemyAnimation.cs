using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemyAnimation : MonoBehaviour {

	public GameObject enemy; 

	private int health; 
	private string enemyName; 
	private Animator ani; 

	// Use this for initialization
	void Start () {

		health = enemy.GetComponent<EnemyAI> ().health; 
		enemyName = enemy.name; 
		ani = enemy.GetComponent<Animator> (); 
	
	}
	
	// Update is called once per frame
	void Update () {

		enemyAnimator (health, enemyName); 
		
	} 
		
	void enemyAnimator(int health, string name)
	{

		if (health == 0) {
			if (enemyName.Contains("hoverEnemy")) {
				ani.Play ("hovdeath"); 
			} else if (enemyName.Contains("palekEnemy")) {
				ani.Play ("palekdeath"); 
			} else {
				ani.Play ("roombadeath"); 
			}
		}
	}
}
