using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIControl : MonoBehaviour {
	public GameObject logPanel;
	public Text[] AllTextPanels;
	private static bool created = false;

   [SerializeField]
	PanelScript pCtrl;

	//init to 1 so when counter increases, panel becomes active.
	int counter = 1;

	//Toggleable panel
	public void showHidePanel(GameObject panel)
	{
		counter++;
		if (counter % 2 == 1)
		{
			if (panel != null)
				panel.gameObject.SetActive(false);
		}
		else
		{
			if (panel != null) {
				panel.gameObject.SetActive (true);
			}
		}
	}

	// Use this for initialization
	void Start () {
		MemoryChipController.p = pCtrl;
		GameMasterControl.LogPanelText.Clear ();
		foreach(Text text in AllTextPanels){
			GameMasterControl.LogPanelText.Add (text);
		}
	}

//	private void Awake()
//	{
//
//		DontDestroyOnLoad(transform.gameObject);
//
//	}

	// Update is called once per frame
	void Update() {
		if (Input.GetKeyDown(KeyCode.Escape))
		{
			showHidePanel(logPanel);
			PopulateLog ();
		}


	}

	private void PopulateLog(){
		for (int index = 0; index < 9; index++) {
			GameMasterControl.LogPanelText [index].text = GameMasterControl.CollectedChips [index];
		}

//		int index = 0;
//		foreach (string text in GameMasterControl.CollectedChips) {
//			GameMasterControl.LogPanelText[index++].text = text;
////			++index;
//		}
	}


}
