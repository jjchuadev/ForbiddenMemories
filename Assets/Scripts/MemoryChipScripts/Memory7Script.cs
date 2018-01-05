using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//JC

public class Memory7Script : MonoBehaviour {

	public Text dialogue;
	public Text logPanel;

	// Use this for initialization
	void Start () {
		dialogue.text = "";
		logPanel.text = "";
	}

	private void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			dialogue.text = "Once there was nothing left to destroy, I felt empty. I needed to sob, I needed to weep, I needed to cry, I needed to escape...";
//			logPanel.text += dialogue.text + "\n";
			GameGoodies.CollectedChips (6, dialogue.text);
		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
