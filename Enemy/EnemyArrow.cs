using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyArrow : MonoBehaviour {

    public int speed;
    public int damage = 10;

    void Update()
    {
        transform.Translate(Vector3.right * Time.deltaTime * speed);
    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        if (other.tag == "Player")
        {
            other.GetComponent<PlayerHealth>().TakeDamage(damage);
            Destroy(this.gameObject);
        }
        if (other.tag == "ground")
        {
            Destroy(this.gameObject);
        }
        
    }
}
