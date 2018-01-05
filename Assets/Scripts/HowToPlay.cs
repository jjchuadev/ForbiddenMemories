using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HowToPlay : MonoBehaviour {
	public GameObject helpPanel;


	public void showPanel() {
		helpPanel.gameObject.SetActive(true);

	}

	public void hidePanel()
	{
		helpPanel.gameObject.SetActive(false);

	}
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (Input.GetMouseButtonDown(0))
		{
			helpPanel.gameObject.SetActive(false);
		}
	}
}
