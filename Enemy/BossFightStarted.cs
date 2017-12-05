using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFightStarted : MonoBehaviour {

    public BossFight bossFightScript;
    private bool bossFightStarted = false;

    public GameObject[] doors;

    // Use this for initialization
    void Start()
    {
    }

    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Bullet" && !bossFightStarted)
        {
            bossFightStarted = true;
            DoorsOpen(true);

            StartCoroutine(bossFightScript.shoot());
        }
    }

    public void DoorsOpen(bool doorOpen)
    {
        doors[0].SetActive(doorOpen);
        doors[1].SetActive(doorOpen);
    }

    
}
