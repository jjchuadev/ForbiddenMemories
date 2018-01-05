using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HidePanel : MonoBehaviour {
	public GameObject thisPanel;



	public void hidePanel()
	{
		thisPanel.gameObject.SetActive(false);

	}


	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
