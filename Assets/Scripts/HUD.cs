using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HUD : MonoBehaviour {

	public Sprite[] HeartSprites;

	public Image HeartUI;

	private int enhancements; //health 

	void start () {
		
	}

	void Update () {
		enhancements = GameMasterControl.pEnhancements;
		switch (enhancements)
		{
		case 0:
		case 1:
		case 2:
		case 3:
		case 4:
		case 5:
			HeartUI.sprite = HeartSprites [enhancements];
			break;
		case 6:
		case 7:
			HeartUI.sprite = HeartSprites [5];
			break;
		default:
			HeartUI.sprite = HeartSprites [0];
			break;
		}
	}
}
