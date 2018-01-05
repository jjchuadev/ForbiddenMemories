using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//JC

public class Memory3Script : MonoBehaviour {

	public Text dialogue;
	public Text logPanel;

	// Use this for initialization
	void Start () {
		
	}

	private void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			dialogue.text = "What I can recall haunts me to this day; alien cyborgs surrounding my body tied down to a table. My intestines pulsating were the last thing I gazed upon before losing consciousness.";
//			logPanel.text += dialogue.text + "\n";
			GameGoodies.CollectedChips (2, dialogue.text);

		}
	}

	// Update is called once per frame
	void Update () {

	}
}
