using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossFight : MonoBehaviour {

    public GameObject fireBreath;
    public GameObject breatheSpawnPoint;

    //Facing
    public GameObject enemyGraphic;
    private bool facingRight = true;

    private GameObject player;

    private bool fightStarted = false;
    private bool playerEnteredRoom = false;

    private AudioSource source;
    public AudioClip[] FanFirClips;

    private float nextEnemyGrowl = 0f;
    private float growlTime = 5f;




    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");

        source = GetComponent<AudioSource>();

        StartCoroutine(cinematic());

    }

    // Update is called once per frame
    void Update ()
    {
        if (Time.time > nextEnemyGrowl)
        {
            if (Random.Range(0, 10) >= 5)
            {
                source.PlayOneShot(FanFirClips[4], 0.8f);
            }

            nextEnemyGrowl = Time.time + growlTime;
        }
    }

    IEnumerator cinematic()
    {
        source.PlayOneShot(FanFirClips[0], 0.8f);
        yield return new WaitForSeconds(FanFirClips[0].length + 1f);
        source.PlayOneShot(FanFirClips[1], 0.8f);
        yield return new WaitForSeconds(FanFirClips[1].length + 2f);
        source.PlayOneShot(FanFirClips[2], 0.8f);
        yield return new WaitForSeconds(FanFirClips[2].length + 2f);
        source.PlayOneShot(FanFirClips[3], 0.8f);
        yield return new WaitForSeconds(FanFirClips[2].length);
        
        //turn on player scripts to move again.
        TurnOffPlayer(true);

        yield return new WaitForSeconds(2f);
        StartCoroutine(shoot());

    }

    public IEnumerator shoot()
    {
        Debug.Log("Started Coroutine shoot");

        if (!fightStarted)
        {
            fightStarted = true;
        }
        Vector2 playerPosition = player.transform.position;

        
        //calculatin the rotation towards the player.
        playerPosition.x = playerPosition.x - breatheSpawnPoint.transform.position.x;
        playerPosition.y = playerPosition.y - breatheSpawnPoint.transform.position.y;

        float angle = Mathf.Atan2(playerPosition.y, playerPosition.x) * Mathf.Rad2Deg;

        //set playerposition back so the drake will look the good way.
        playerPosition = player.transform.position;

        //enemy will look at the player 
        if (facingRight && playerPosition.x < transform.position.x)// so if the X position of the player is lower then the boss it means the player is left from the boss.
        {
            Debug.Log("Player is left from the boss");
            flipFacing();
        }
        else if (!facingRight && playerPosition.x > transform.position.x)
        {
            Debug.Log("Player is Right from the boss");
            flipFacing();
        }
        yield return new WaitForSeconds(1.5f);       
        Instantiate(fireBreath, breatheSpawnPoint.transform.position, Quaternion.Euler(new Vector3(0, 0, angle)));

        if (fightStarted)
        {
            StartCoroutine(shoot());
        }
        

    }

    void flipFacing()
    {
        float facingX = enemyGraphic.transform.localScale.x;
        facingX *= -1f;
        enemyGraphic.transform.localScale = new Vector3(facingX, enemyGraphic.transform.localScale.y, enemyGraphic.transform.localScale.z);
        facingRight = !facingRight;
    }

    void OnTriggerEnter2D(Collider2D other)
    {
        

        if (other.tag  == "Player")
        {
            if (!playerEnteredRoom)
            {
                playerEnteredRoom = true;
                //source.PlayOneShot(dragonGrowl, 0.8f);
            }            
           
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
       
        if (other.tag == "Player")
        {

        }
    }

    void TurnOffPlayer(bool playerActive)
    {
        player.GetComponent<PlatformController>().enabled = playerActive;
        player.GetComponent<Animator>().enabled = playerActive;
        player.GetComponent<WeaponBehaviour>().enabled = playerActive;
    }
}
