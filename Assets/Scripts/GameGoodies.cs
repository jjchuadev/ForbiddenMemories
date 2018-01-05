using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class GameGoodies : MonoBehaviour {


	//function for any script that wants to spawn objects 
	//SpawnPointsParent is a folder/gameobject with other children that are Vector3 in the game
	//Whatareyouspawning is a list of transform/prefabs of all objects you would like to be randomly selected
	//WhereAreyouspawning is a Parent gameobject/transform where the child object will be spawned to
	public static void SpawnObjects(GameObject SpawnPointsParent,Transform[] whatYouAreSpawning, Transform whereAreYouSpawning){
		if (SpawnPointsParent != null) {
			List<GameObject> spawnPoints = new List<GameObject> ();
			for (int i = 0; i < SpawnPointsParent.transform.childCount; i++) {
				spawnPoints.Add (SpawnPointsParent.transform.GetChild (i).gameObject);
			}
			Vector3 randomSpawnPoint = spawnPoints [Random.Range (0, spawnPoints.Count)].transform.position;
			Transform objectPrefab = whatYouAreSpawning[Random.Range (0, whatYouAreSpawning.Length)];
			Instantiate (objectPrefab, randomSpawnPoint, objectPrefab.rotation, whereAreYouSpawning);
		}
	}

	public static void EnemyDropRate(Transform SpawningObject, Transform[] objectPrefabs, Transform InstantiateFolder, int ChildCounter){

		Transform objectPrefab = objectPrefabs [Random.Range (0, objectPrefabs.Length)];
		bool CanWeSpawn = FindChildrenByTag (objectPrefab.tag, InstantiateFolder, ChildCounter);
		if(CanWeSpawn){
			Instantiate (objectPrefab, SpawningObject.position, objectPrefab.rotation, InstantiateFolder);
		}
	}

	public static bool FindChildrenByTag(string tag, Transform Parent,int ChildCounter){
			int cc = 0;
			foreach (Transform child in Parent) {
				if (child.tag == tag) {
					++cc;
				}
			}
			if (cc <= ChildCounter) {
				return true;
			}

			return false;

	}

	public static void CollectedChips(int chipNumber,string chipText){
		GameMasterControl.CollectedChips.Insert (chipNumber,chipText);
		Debug.Log (GameMasterControl.CollectedChips[chipNumber]);
	}

	public static void ClearLog(Text[] alltext){
		foreach (Text text in alltext) {
			text.text = "";
		}
	}
}
