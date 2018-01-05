using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//JC

public class Memory5Script : MonoBehaviour {

	public Text dialogue;
	public Text logPanel;
	// Use this for initialization
	void Start () {
		
	}

	private void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			dialogue.text = "In a blind fury, I found the nearest weapon and wreaked havoc on any thing in sight: soldiers, personnel, families, computers. Nothing could escape my rage.";
//			logPanel.text += dialogue.text + "\n";
			GameGoodies.CollectedChips (4, dialogue.text);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
