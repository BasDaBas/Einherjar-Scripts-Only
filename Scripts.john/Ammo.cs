using UnityEngine;
using System.Collections;

public class Ammo : MonoBehaviour
{
    public float speed;
    //private float weaponFallingSpeed;
    public int damage = 10;

    //private Rigidbody2D rb2d;

    void Start()
    {
        //rb2d = GetComponent<Rigidbody2D>();
    }
    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
        /*
        //checking if the weapon is not moving anymore so the animation can be stopped.
        weaponFallingSpeed = rb2d.velocity.magnitude;
        
        if (weaponFallingSpeed < 0.1)
        {
            gameObject.GetComponent<Animator>().enabled = false;
            Destroy(gameObject, 2f);
        }*/
    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.tag == "Enemy" || other.tag == "Boss")
        {            
            other.GetComponent<EnemyHealth>().EnemyTakeDamage(damage);

            Destroy(gameObject);
        }

        else if (other.tag == "ground") 
        {
            Destroy(gameObject);
        }        
    }
}