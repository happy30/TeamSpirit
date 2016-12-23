using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KartMainMenu : MonoBehaviour {
    public GameObject mainMenuPanel;
    public GameObject newGamePanel;
	
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void NewGameOptions()
    {
        newGamePanel.SetActive(true);
        mainMenuPanel.SetActive(false);
    }

    public void StartNewGame()
    {
        Application.LoadLevel(0);
    }

}
