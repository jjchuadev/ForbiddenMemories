using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//JC

public class Memory4Script : MonoBehaviour {

	public Text dialogue;
	public Text logPanel;
	// Use this for initialization
	void Start () {
		
	}

	private void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			dialogue.text = "I awoke some time later to find I had no feeling in my body. As I attempted to lift myself off the table, I heard a scraping sound from directly under me. I moved my hand in front of my eyes only to see cold iron. I was furious; I could never return to the life I once had.";

//			logPanel.text += dialogue.text + "\n";
			GameGoodies.CollectedChips (3, dialogue.text);

		}
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
