//MainMenuManager by Arne
using UnityEngine;
using System.Collections;

public class MainMenuManager : MonoBehaviour 
{	
	//public GameObject newGameIntro;
	public GameObject mainMenuManager;
	public GameObject optionsManagerPanel;
	public GameObject exitGamePanel;
	public GameObject creditsScene;

	// Use this for initialization
	void Awake () 
	{
		optionsManagerPanel.SetActive(false);
		exitGamePanel.SetActive(false);
	}
	// Update is called once per frame
	void Update () 
	{
		if(Input.GetButtonDown("Cancel")) 
		{
			if(optionsManagerPanel.activeSelf)
			{
				optionsManagerPanel.SetActive(false);
				mainMenuManager.SetActive(true);
				
			}
			if(exitGamePanel.activeSelf)
			{
				exitGamePanel.SetActive(false);
			}
		}
	}
	//NewGameMenu activates the start of the game
	public void NewGameMenu () 
	{
		//load = GameObject.Find("GameManager").GetComponent<LoadController>();
		//load.LoadScene("LVL1");
		
	}
	//OptionsMenu gives you access to edit controls or graphic options 
	public void OptionsMenu () 
	{
		optionsManagerPanel.SetActive(true);
		mainMenuManager.SetActive(false);
	}
	//OptionsBack gets you back to the previous screen
	public void OptionsBack () 
	{
		optionsManagerPanel.SetActive(false);
		mainMenuManager.SetActive(true);
	
	}
	//ExitGameMenu exits you from the game
	public void ExitGameMenu () 
	{
		exitGamePanel.SetActive(true);
	}
	//ExitGameTrue gets the game to quit
	public void ExitGameTrue ()
	{
		Application.Quit();
	}
	//ExitGameFalse gets you back to main menu
	public void ExitGameFalse ()
	{
		exitGamePanel.SetActive(false);
	}
	//CreditsMenu gets you a scene of all the credits
	public void CreditsMenu ()
	{
		//shows you a scene with all the credits
	}
}
