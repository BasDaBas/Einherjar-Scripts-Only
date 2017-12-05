using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ToggleCanvas : MonoBehaviour {

    public GameObject optionCanvas;
    public GameObject startCanvas;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ToggleStartCanvas()
    {
        optionCanvas.SetActive(false);
        startCanvas.SetActive(true);
    }

    public void ToggleOptionCanvas()
    {
        optionCanvas.SetActive(true);
        startCanvas.SetActive(false);
    }
}
