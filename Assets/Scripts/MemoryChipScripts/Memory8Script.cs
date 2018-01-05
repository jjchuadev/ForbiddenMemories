using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//JC

public class Memory8Script : MonoBehaviour {

	public Text dialogue;
	public Text logPanel;

	// Use this for initialization
	void Start () {
		
	}

	private void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			dialogue.text = "I wanted to forget everything that occurred, all the slaughtering, the screams, the bloodshed; it was too much for me to bear.";
//			logPanel.text += dialogue.text + "\n";
			GameGoodies.CollectedChips (7, dialogue.text);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
