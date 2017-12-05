using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TurnAround : MonoBehaviour {

    public GameObject enemyGraphic;
    private bool facingRight = true;



    // Use this for initialization
    void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //if the player is in range the enemy will look at the player depending on which way it is looking.
            if (facingRight && other.transform.position.x < transform.position.x)
            {
                flipFacingFanFir();
            }
            else if (!facingRight && other.transform.position.x > transform.position.x)
            {
                flipFacingFanFir();
            }
        }
    }
    void flipFacingFanFir()
    {
        if (enemyGraphic == null)
        {
            Destroy(gameObject);
        }

        float facingX = enemyGraphic.transform.localScale.x;
        facingX *= -1f;
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;
    }


}
