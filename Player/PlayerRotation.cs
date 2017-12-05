using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : MonoBehaviour {

    public float speed = 50;

    void FixedUpdate()
    {

        ControlMouse();

    }

    void ControlMouse()
    {
        // capture mouse position. We need to convert between pixels and World Unities
        var mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        // Basicly it's looking to the mouse position. And rotating.
        Quaternion rot = Quaternion.LookRotation(transform.position - mousePosition, Vector3.forward);

        // set our gameobject rotation to the calculated one rotation
        transform.rotation = rot;
        // doesnt changerotation angles for x, y.
        transform.eulerAngles = new Vector3(0, 0, transform.eulerAngles.z);
        // prevents from "slide"
        GetComponent<Rigidbody2D>().angularVelocity = 0;

        // this because moving foward means moving in the Z axis, but this is a topdown game!
        // obs.: we still need to press up/down arrows to move!
        float inputVer = Input.GetAxis("Vertical");
        float inputHor = Input.GetAxis("Horizontal");
        GetComponent<Rigidbody2D>().AddForce(gameObject.transform.up * speed * inputVer);
        GetComponent<Rigidbody2D>().AddForce(gameObject.transform.right * speed * inputHor);

    }
}
