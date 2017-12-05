using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformController : MonoBehaviour {

    [HideInInspector]
    public bool facingRight = true;
    [HideInInspector]
    public bool jump = false;

    public float moveForce = 365f;
    public float maxSpeed = 5f;
    public float jumpForce = 1000f;
    public float climbingSpeed = 10f;
    public Transform groundCheck;
    public LevelManager levelManager;
    public GameObject[] visableWeapons;


    private bool grounded = false;
    private bool isClimbing = false;    
    private Animator anim;
    private Rigidbody2D rb2d;

    public AudioClip jumpSound;
    public AudioSource source1;
    public AudioSource source2;

    private bool haveKey = false;

    public GameObject gameCompletedImage;



    // Use this for initialization
    void Awake()
    {
        //DontDestroyOnLoad(gameObject);
        //Debug.Log("Dont destroy on load: " + name);

        anim = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();


    }

    // Update is called once per frame
    void Update()
    {
        grounded = Physics2D.Linecast(transform.position, groundCheck.position, 1 << LayerMask.NameToLayer("Ground"));       

        if (Input.GetButtonDown("Jump") && grounded)
        {
            jump = true;
            source2.PlayOneShot(jumpSound, 0.3f);
        }

        //player footstep sounds
        /*if ((Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical")) && !source1.isPlaying && grounded)
        {
            source1.UnPause();   
            
        }
        
        else if (!Input.GetButton("Horizontal") && !Input.GetButton("Vertical") && source1.isPlaying || !grounded)
        {
            source1.Pause();
        }*/
    }

    void FixedUpdate()
    {
        float h = Input.GetAxis("Horizontal");
               
        anim.SetFloat("speed", Mathf.Abs(h));

        if (h * rb2d.velocity.x < maxSpeed)
            rb2d.AddForce(Vector2.right * h * moveForce);

        if (Mathf.Abs(rb2d.velocity.x) > maxSpeed)
            rb2d.velocity = new Vector2(Mathf.Sign(rb2d.velocity.x) * maxSpeed, rb2d.velocity.y);

        if (h > 0 && !facingRight)
            Flip();
        else if (h < 0 && facingRight)
            Flip();
            

        //If Climbing is true
        if (isClimbing)
        {
            if (Input.GetAxis("Vertical") > 0)
            {               
                transform.Translate(0, 1 * climbingSpeed * Time.deltaTime, 0);
                Debug.Log("Climbing Up");
            }
            //this is down
            else if (Input.GetAxis("Vertical") < 0)
            {                
                transform.Translate(0, -1 * climbingSpeed * Time.deltaTime, 0);
                Debug.Log("Climbing Down");
            }
        }

        

        if (jump)
        {
            //anim.SetTrigger("Jump");
            rb2d.AddForce(new Vector2(0f, jumpForce));
            jump = false;
        }
    }


    void Flip()
    {
        //Flipping the character by changing the scale to negative or postive
        facingRight = !facingRight;
        Vector3 theScale = gameObject.transform.localScale;
        theScale.x *= -1;
        gameObject.transform.localScale = theScale;
        
        //flipping the weapons the player is carrying.
        for (int i = 0; i < visableWeapons.Length; i++)
        {
            Vector3 weaponScale = visableWeapons[i].transform.localScale;
            weaponScale.x *= -1;
            visableWeapons[i].transform.localScale = weaponScale;
        }
    }
    

    private void OnTriggerEnter2D(Collider2D other)
    {        
        if (other.gameObject.tag == "Door" && haveKey)
        {
            Debug.Log("Hit the door");
            other.gameObject.GetComponent<MoveDoors>().UnlockGate();

            Invoke("SetImageActive", 4);            
        }

        if (other.gameObject.tag == "Ladder")
        {
            isClimbing = true;
            rb2d.gravityScale = 0;
        }

        if (other.gameObject.tag == "Key")
        {
            Destroy(other.gameObject);
            haveKey = true;
            Debug.Log("HaveKey = " + haveKey);
        }
    }

    void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Ladder")
        {
            isClimbing = false;
            rb2d.gravityScale = 4.5f;
        }
    }

    void OnCollisionEnter2D(Collision2D other)
    {
        if (other.transform.tag == "movingPlatform")
        {
            transform.parent = other.transform;
        }
    }

    void OnCollisionExit2D(Collision2D other)
    {
        if (other.transform.tag == "movingPlatform")
        {
            transform.parent = null; 
        }
    }

    void OnLevelComplete()
    {
        levelManager.LoadLevel("01a Start Scene");
    }

    void SetImageActive()
    {
        gameCompletedImage.SetActive(true);
        Invoke("OnLevelComplete", 10f);
    }





}
