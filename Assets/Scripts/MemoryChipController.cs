using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using System.Diagnostics;
using UnityEngine.UI; //JC

public class MemoryChipController : MonoBehaviour {

	//Static panel
	public static PanelScript p;

	private void OnCollisionEnter2D(Collision2D coll){


		if (coll.gameObject.tag == "Player") {

			//Ensure croutine runs only once
			bool enter = true;
			if (enter) {
				enter = false;
				p.showPanel();


				GameMasterControl.incStory(); //jds
				//run routine, wait before closing panel
				p.hidePanel();

			}
		}
	}





}
