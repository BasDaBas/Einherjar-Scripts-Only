using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveDoors : MonoBehaviour {

    public GameObject door;
    public float moveSpeed;
    public Transform point;
    private bool gateUnlocked = false;
    public GameObject doorlight;


    // Use this for initialization
    void Start()
    {
    }

    // Update is called once per frame
    void Update()
    {
        if (gateUnlocked)
        {
            Debug.Log("GateUnlocked");
            Destroy(doorlight);
            door.transform.position = Vector3.MoveTowards(door.transform.position, point.position, Time.deltaTime * moveSpeed);
        }        
    }

    public void UnlockGate()
    {
        Debug.Log("Function Called");
        gateUnlocked = true;
        Debug.Log("GateUnlocked = " + gateUnlocked);        
    }


    
}
