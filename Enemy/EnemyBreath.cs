using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBreath : MonoBehaviour {

    public float lifetime = 1;
    private Color fade;
    private float timer;
    private bool fightStarted;

    public int damage = 30;

    // Use this for initialization
    void Start ()
    {
		
	}
	
	// Update is called once per frame
	void Update ()
    {
        timer += Time.deltaTime;
        if (timer >= lifetime)
        {
            //Transparency.
            fade = GetComponent<Renderer>().material.color;
            fade.a = fade.a / 1.1f;
            GetComponent<Renderer>().material.color = fade;
            //kill when faded
            if (fade.a <= .1)
            {
                Destroy(gameObject);
            }
        }    
    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
        }

    }
}