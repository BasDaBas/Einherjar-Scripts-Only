using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyMeleeController : MonoBehaviour {

    Animator enemyAnimator;

    public float enemySpeed;


    //Facing
    public GameObject enemyGraphic;
    private bool canFlip = true;
    private bool facingRight = false;
    private float flipTime = 5f;
    private float nextFlipChance = 0f;

    //Attacking
    public float chargeTime;
    private float startChargeTime;
    private bool charging;
    Rigidbody2D enemyRB;
        

    // Use this for initialization
    void Start ()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        enemyRB = GetComponent<Rigidbody2D>();

    }
	
	// Update is called once per frame
	void Update ()
    {
        if (Time.time > nextFlipChance)
        {
            if (Random.Range(0,10) >=5)
            {
                flipFacing();                
            }

            nextFlipChance = Time.time + flipTime;
        }
	}

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Player")
        {
            //if the player is in range the enemy will look at the player depending on which way it is looking.
            if (facingRight && other.transform.position.x < transform.position.x)
            {
                flipFacing();
            }
            else if (!facingRight && other.transform.position.x > transform.position.x)
            {
                flipFacing();
            }
            canFlip = false;//to makes sure the enemy doesn't flip anymore when the player is in range.
            charging = true;
            startChargeTime = Time.time + chargeTime;
            enemyAnimator.SetBool("isCharging", charging);
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag =="Player")
        {
            if (startChargeTime < Time.time)
            {
                if (!facingRight)
                {
                    enemyRB.AddForce(new Vector2(-1, 0) * enemySpeed);
                }
                else
                {
                    enemyRB.AddForce(new Vector2(1, 0) * enemySpeed);
                }
                enemyAnimator.SetBool("isCharging", charging);
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canFlip = true;
            charging = false;
            enemyRB.velocity = new Vector2(0f, 0f);
            enemyAnimator.SetBool("isCharging", charging);

        }
    }

    void flipFacing()
    {
        if (!canFlip)
        {
            return;
        }

        float facingX = enemyGraphic.transform.localScale.x;
        facingX *= -1f;
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y,enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;
    }
}
