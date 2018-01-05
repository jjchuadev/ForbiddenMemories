using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//JC

public class Memory9Script : MonoBehaviour {

	public Text dialogue;
	public Text logPanel;

	// Use this for initialization
	void Start () {
		
	}

	private void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			dialogue.text = "In my listless wandering, I stumbled upon some memory chips. I could forget this all happened. I eventually discovered the machine to aid the memory transfer was in the same room I first awoke in. Ironic isn't it? Maybe my next self will find peace with all this; I know I won't.";
//			logPanel.text += dialogue.text + "\n";
			GameGoodies.CollectedChips (8, dialogue.text);


		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
