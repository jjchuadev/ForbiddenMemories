using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

//JC

public class Memory2Script : MonoBehaviour {

	public Text dialogue;
	public Text logPanel;

	// Use this for initialization
	void Start () {
		
	}

	private void OnCollisionEnter2D(Collision2D coll){
		if (coll.gameObject.tag == "Player") {
			dialogue.text = "My name is Roberto Aguilar, I was a university student at UC Irvine back on Earth. Everything else about my past life is nothing more than a haze.";

//			logPanel.text += dialogue.text + "\n";
			GameGoodies.CollectedChips (1, dialogue.text);
		}
	}

	// Update is called once per frame
	void Update () {

	}
}
