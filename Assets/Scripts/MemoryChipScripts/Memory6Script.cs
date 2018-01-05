using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//JC

public class Memory6Script : MonoBehaviour {

	public Text dialogue;
	public Text logPanel;

	// Use this for initialization
	void Start () {
		
	}

	private void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			dialogue.text = "In my rampage, I released other experiments locked away on the station which led to the genocide of everyone on board.";
//			logPanel.text += dialogue.text + "\n";
			GameGoodies.CollectedChips (5, dialogue.text);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
