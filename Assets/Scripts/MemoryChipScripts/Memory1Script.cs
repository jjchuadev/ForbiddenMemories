using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//JC

public class Memory1Script : MonoBehaviour {

	public Text dialogue;
	public Text logPanel;

	// Use this for initialization
	void Start () {
		
	}

	private void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			dialogue.text = "I have no recollection of how I arrived on this station, but I recall my past life.";


//			logPanel.text += dialogue.text + "\n";
			GameGoodies.CollectedChips (0, dialogue.text);
			GameMasterControl.incStory ();
		}

	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
