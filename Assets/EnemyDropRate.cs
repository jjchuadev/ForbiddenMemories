using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDropRate : MonoBehaviour {
	/*
		This script will take a list of gameobjects that it can spawn it
		will spawn the object before the enemy is destroyed in the exact location.
		The object will spawn under the PlayerInteraction Hierarchy.
		Must work in sync with the RespawnObjectsController
	*/

//	public Transform spawnerPrefab;
	public Transform InstantiateFolder;
	public Transform[] objectPrefabList;
	private Transform objectPrefab;

	//Used to check how many objects are allowed to be in the folder
	//0 is default and when there is 0 children then we respawn
	private float localTimer;
	private int health;
	private enemyAnimation aniScript;
	private bool spawned;
	private Transform InteractableObjectFolder;
	void Start(){
		aniScript = GetComponent<enemyAnimation> ();
		health = aniScript.health;
		spawned = false;
		InteractableObjectFolder = GameObject.Find ("PlayerInteraction").transform;
	}


	void Update(){
		health = aniScript.health;
		if (health <= 0 && !spawned) {
			if (DropChance ()) {
				GameGoodies.EnemyDropRate (this.transform, objectPrefabList, InstantiateFolder,1);
			}
			spawned = true;
		}
	}

	private bool DropChance(){
		//Shitty Algorithm but whatever if you want to change it let me know !!

		int myNumber = Random.Range (0, 100);
		int myChance = 3;
		int randomNess = Random.Range (0, myChance - 1);
		int result = myNumber % myChance;
		if (result == randomNess) {
			Debug.Log ("CAN DROP NOW LOOK IN PLAYERINTERACTIONS");
			return true;
		}
		return false;
	}


}
