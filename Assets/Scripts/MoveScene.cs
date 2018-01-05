using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;

public class MoveScene : MonoBehaviour
{
	[SerializeField]
	string	loadLevel;


	//JC
	private BoxCollider2D coll;
	public Transform InstantiateFolder;
	public int ChildCounter = 0;

	// Use this for initialization
	void Start () {
		coll = GetComponent<BoxCollider2D> ();
	}

	void OnTriggerEnter2D(Collider2D other)
	{
		if (other.CompareTag("Player"))
		{
			bool chipsGone = findChildren ("MemoryChip");
			if (chipsGone) {
				SceneManager.LoadScene(loadLevel);
			}

		}
	}

	private bool findChildren(string tag){
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