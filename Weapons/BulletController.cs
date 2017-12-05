using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour {

    public float bulletSpeed = 10;
    Rigidbody2D myRB;

    // Use this for initialization
    void Awake ()
    {
        myRB = GetComponent<Rigidbody2D>();
        myRB.AddForce(new Vector2(1, 0) * bulletSpeed, ForceMode2D.Impulse);

        //transform.Translate(Vector3.right * Time.deltaTime);       
               
    }
	
}
