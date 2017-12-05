using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyRangedController : MonoBehaviour {

    Animator enemyAnimator;

    public GameObject enemyBullet;
    public Transform spawnPoint;
    private float fireRate = 1.6f;
    private float nextFire = 0f;

    private Animator anim;

    //Facing
    public GameObject enemyGraphic;
    private bool canFlip = true;
    private bool facingRight = false;
    private float flipTime = 5f;
    private float nextFlipChance = 0f;

    //Attacking
    private float startShootTime;
    private bool canShoot;
    private AudioSource source;
    public AudioClip arrowClip;
    private GameObject player;



    // Use this for initialization
    void Start()
    {
        enemyAnimator = GetComponentInChildren<Animator>();
        source = GetComponent<AudioSource>();
        player = GameObject.FindGameObjectWithTag("Player");

    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > nextFlipChance)
        {
            if (Random.Range(0, 10) >= 5)
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
            canShoot = true;
            
        }

    }

    void OnTriggerStay2D(Collider2D other)
    {
        if (other.tag == "Player")
        {            
            if (!facingRight)
            {
                spawnPoint.rotation = Quaternion.Euler(new Vector3(0f, -180f, 0f));
                StartCoroutine(EnemyShoot());
                //EnemyShooting();
            }
            else
            {
                spawnPoint.rotation = Quaternion.Euler(Vector3.zero);
                StartCoroutine(EnemyShoot());

                //EnemyShooting();
            }
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.tag == "Player")
        {
            canFlip = true;
            canShoot = false;
            enemyAnimator.SetBool("isShooting", false);
            StopAllCoroutines();

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
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;
    }

   

    IEnumerator EnemyShoot()
    {
        enemyAnimator.SetBool("isShooting", true);

        yield return new WaitForSeconds(0.8f);
        if (Time.time > nextFire)//time passed is greater then nextfire
        {
            Vector2 playerPosition = player.transform.position;


            //calculatin the rotation towards the player.
            playerPosition.x = playerPosition.x - spawnPoint.transform.position.x;
            playerPosition.y = playerPosition.y - spawnPoint.transform.position.y;

            float angle = Mathf.Atan2(playerPosition.y, playerPosition.x) * Mathf.Rad2Deg;

            

            nextFire = Time.time + fireRate;

            Instantiate(enemyBullet, spawnPoint.position, Quaternion.Euler(new Vector3(0, 0, angle)));
            source.PlayOneShot(arrowClip);

        }
        StartCoroutine(EnemyShoot());
    }
}
