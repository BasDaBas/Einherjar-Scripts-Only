using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;

public class GameManager : MonoBehaviour {

    private bool isPaused;                              //Boolean to check if the game is paused or not
    private ShowGamePanels showPanels;                  //Reference to the ShowPanels script used to hide and show UI panels
    public GameObject player;



    void Start()
    {
        //Cursor.visible = false;
        Time.timeScale = 1;                             //setting timescale on 1 so if you would go back from the game to the main menu and back into the game, the game will be "unpaused"
    }

    void Awake()
    {
        //Get a component reference to ShowPanels attached to this object, store in showPanels variable
        showPanels = GetComponent<ShowGamePanels>();
    }

    // Update is called once per frame
    void Update()
    {

        if (Input.GetKeyDown(KeyCode.P) && isPaused == false)
        {
            Debug.Log("Open Menu");
            DoPause();
        }
        else if (Input.GetKeyDown(KeyCode.P) && isPaused == true)
        {
            Debug.Log("Close Menu");
            UnPause();
        }

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Debug.Log("Escape is pressed");
            SceneManager.LoadScene(1);

        }

    }

    public void DoPause()
    {
        //make cursor visible
        //Cursor.visible = true;
        //Set isPaused to true
        isPaused = true;
        //Set time.timescale to 0, this will cause animations and physics to stop updating
        Time.timeScale = 0;
        //call the ShowPausePanel function of the ShowPanels script
        showPanels.ShowMenu();
    }

    public void UnPause()
    {
        //make cursor not visible
        //Cursor.visible = false;
        //Set isPaused to false
        isPaused = false;
        //Set time.timescale to 1, this will cause animations and physics to continue updating at regular speed
        Time.timeScale = 1;
        //call the HidePausePanel function of the ShowPanels script
        showPanels.HideMenu();
    }
}
