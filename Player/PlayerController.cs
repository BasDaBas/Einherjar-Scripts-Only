using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{   
    public float speed = 6;
    public float bulletSpeed = 25;
    private float nextFire;
    public float fireRate;


    public GameObject bulletPrefab;
    public Transform bulletSpawn;

    private PlayerHealth health;

    Rigidbody2D rbody;

    //Animator anim;
    void Start()
    {
        rbody = GetComponent<Rigidbody2D>();
        health = GetComponent<PlayerHealth>();
        //anim = GetComponent<Animator>();


    }
    void Update()
    {       

        Vector2 movement_vector = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime * speed);

       //Player will Shoot when you press Space
       if (Input.GetKeyDown(KeyCode.Space) && Time.time > nextFire)
        {            
            Fire();
        }
        //Player will turn the lantern on or off when you press E
        if (Input.GetKeyDown(KeyCode.E))
        {
            //Smash Crate? 
        }

        if (movement_vector != Vector2.zero)
        {
            //anim.Setbool("isWalking", true);
           // anim.SetFloat("Input_x", movement_vector.x);
           // anim.SetFloat("Input_y", movement_vector.y);
        }
        else
        {
           // anim.Setbool("isWalking", false);
            rbody.MovePosition(rbody.position + movement_vector * Time.deltaTime);
        }

    }   

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == "Enemy")
        {
            health.TakeDamage(10);
        }

    }

    void Fire()
    {
        Debug.Log("CmdFire is called");
        nextFire = Time.time + fireRate;
        var bullet = (GameObject)Instantiate(bulletPrefab, bulletSpawn.position, bulletSpawn.rotation);   


        Destroy(bullet, 1.5f);

    }
    
}