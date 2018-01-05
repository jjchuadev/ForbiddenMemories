using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class RespawnObjectsController : MonoBehaviour {

	//add listofspawnpoints from the level we are in
	public GameObject ListOfSpawnPoints;
	public Transform InstantiateFolder;
	public Transform[] objectPrefabList;
	private Transform objectPrefab;
	public float respawnRate = 5.0f;
	public string tag = "Weapon";

	//Used to check how many objects are allowed to be in the folder
	//0 is default and when there is 0 children then we respawn
	public int ChildCounter = 0;

	private List<GameObject> spawnPoints;

	private float localTimer;

	// Use this for initialization
	void Start () {
		spawnPoints  = new List<GameObject> ();
		for (int i = 0; i < ListOfSpawnPoints.transform.childCount; i++) {
			spawnPoints.Add (ListOfSpawnPoints.transform.GetChild (i).gameObject);
		}
		localTimer = respawnRate;

//		Debug.Log ("OUR CURRENT LEVEL IS: " + spawnPoints.ToArray ()[0].transform.position);
//		Vector3 randomSpawnPoint = spawnPoints [Random.Range (0, spawnPoints.Count)].transform.position;
//		Debug.Log ("THE RANDOM VECTOR IS: " + randomSpawnPoint);
//
//		Instantiate (weaponPrefab, randomSpawnPoint, weaponPrefab.rotation, InstantiateFolder);

	}
	
	// Update is called once per frame
	void Update () {
		localTimer -= Time.deltaTime;
		if (localTimer <= 0) {
			localTimer = respawnRate;
			RespawnObject();
		}
	}


	private void RespawnObject(){

		bool DoWeRespawn = findChildren (tag);

		if (DoWeRespawn) {
			Vector3 randomSpawnPoint = spawnPoints [Random.Range (0, spawnPoints.Count)].transform.position;
			objectPrefab = objectPrefabList[Random.Range (0, objectPrefabList.Length)];
			Debug.Log ("THE RANDOM VECTOR IS: " + randomSpawnPoint);
			Instantiate (objectPrefab, randomSpawnPoint, objectPrefab.rotation, InstantiateFolder);
		}
	}

	public bool findChildren(string tag){
		int cc = 0;
		Transform parent = InstantiateFolder;
		foreach (Transform child in parent) {
			if (child.tag == tag) {
				++cc;
			}
		}
		if (cc <= ChildCounter) {
			return true;
		}

		return false;
	}

}
