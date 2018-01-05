using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PanelScript : MonoBehaviour {
	[SerializeField]
	public static int delay;

	public GameObject panel;


	//init to 1 so when counter increases, panel becomes active.
	int counter = 1;

	public void showPanel()
	{
		if (panel != null)
			panel.gameObject.SetActive(true);
	}

	public void hidePanel()
	{
		if (panel != null)
			StartCoroutine(delayTimer());
	}

	//Toggleable panel
	public void showHidePanel()
	{
		counter++;
		if (counter % 2 == 1)
		{
			if (panel != null)
				panel.gameObject.SetActive(false);
		}
		else
		{
			if (panel != null)
				panel.gameObject.SetActive(true);
		}
	}

	// Use this for initialization
	void Start ()
	{
	}

	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown("k"))
			showHidePanel();
	}

	//panel delay before closing
	IEnumerator delayTimer()
	{
		yield return new WaitForSeconds(5);
		panel.gameObject.SetActive(false);
	}

}
