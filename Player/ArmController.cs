using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmController : MonoBehaviour {

    public float weaponOffSet;
	
	// Update is called once per frame
	void Update ()
    {
        //subtracting the position of the player from the mouse position
        Vector3 difference = Camera.main.ScreenToWorldPoint(Input.mousePosition) - transform.position;
        difference.Normalize();

        float rotZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg; //find the angle in degrees
        transform.rotation = Quaternion.Euler(0f, 0f, rotZ - weaponOffSet);
    }
}
