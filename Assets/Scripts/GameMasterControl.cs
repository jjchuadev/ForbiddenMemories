using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;


public class GameMasterControl : MonoBehaviour 
{
	//https://www.sitepoint.com/saving-data-between-scenes-in-unity/

	//put persistent variables here to keep between scenes
	static public int	pEnhancements;		//current enhancement/HP level
	static public bool	pHasLegs;			//used as trigger to prevent jumping with no legs
	static public bool	pHasWeapon;			//used as trigger to prevent shooting with no arms/munitions
	static public bool	pHasSpeedLegs;		//used as trigger to change character speed
	static public bool	pHasDoubleJumpLegs; //used as trigger to change character double jump ability
	static public int	pGamesWon;			//keeps track of how many games have been one since game executed
	static public int	pMobsKilled;		//keeps track of how many mobs/bots have been killed since beginning of game
	static public int	pStoryCollection;	//keeps track of how many story pieces have been collected

	//New By Jerry
	//array that holds all text
	public static List<string> CollectedChips = new List<string>();
	public static List<Text> LogPanelText = new List<Text>();
	void Start(){
		for (int i = 0; i < 9; i++) {
			CollectedChips.Add ("");
		}
	}

	public void ClearCollectedChips(){
		CollectedChips.Clear ();
		LogPanelText.Clear ();
		for (int i = 0; i < 9; i++) {
			CollectedChips.Add("");
		}
	}

	static public void incStory () {
		pStoryCollection += 1;
	}

	static public void incGamesWon () {
		pGamesWon += 1;
	}

	static public void incMobsKilled () {
		pMobsKilled += 1;
	}
	//commented out by jerry all methods are not needed
//	void Awake()
//	{	}
//
//	//At start, load data from GlobalControl.
//	void Start () 
//	{
//
//	}
//
//	void Update()	{
//		if (pEnhancements < 0) {
//			pEnhancements = 0;
//			pHasLegs = false;
//			pHasWeapon = false;
//			pHasSpeedLegs = false;
//			pHasDoubleJumpLegs = false;
//			savePlayerState ();
//
//
//			SceneManager.LoadScene ("Game Over");
//		}
//	}
}
