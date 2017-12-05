using UnityEngine;
using System.Collections;

public class ShowGamePanels : MonoBehaviour {

	public GameObject pausePanel;							//Store a reference to the Game Object MenuPanel 


	//Call this function to activate and display the main menu panel during the main menu
	public void ShowMenu()
	{
		pausePanel.SetActive (true);
	}

	//Call this function to deactivate and hide the main menu panel during the main menu
	public void HideMenu()
	{
		pausePanel.SetActive (false);
	}
	
	
}
