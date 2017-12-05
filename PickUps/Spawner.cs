using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    
    public GameObject[] enemies;                // The enemy Melee prefab to be spawned.
    //public GameObject enemyRanged;                // The enemy Ranged prefab to be spawned.

    public GameObject[] treasure;                // The enemy Ranged prefab to be spawned.



    // Use this for initialization
    void Start()
    {
        if (this.gameObject.tag == "EnemySpawner")
        {
            EnemySpawn();
        }
        else 
        {
            TreasureSpawn();
        }
    }	
	

    void EnemySpawn()
    {
        //Generates a random number
        int enemySpawnIndex = Random.Range(0, 12);

        //If the number is  4 or lower
        if (enemySpawnIndex <= 4)
        {
            //Spawn Enemy Melee
            Instantiate(enemies[0], transform.position, transform.rotation);
        }
        //if the number is 8 or higher
        if (enemySpawnIndex >=8)
        {
            //Spawn Enemy Ranged
            Instantiate(enemies[0], transform.position, transform.rotation);//there is only 1 ranged and melee enemy so for now just spawn the same enemy.
        }
        else 
        {
            //Spawn Nothing
        }

    }

    void TreasureSpawn()
    {
        //Generates a random number
        int treasureSpawnIndex = Random.Range(0, 3);

        //If the number is  0 or lower
        if (treasureSpawnIndex == 0)
        {
            int treasures = Random.Range(0, treasure.Length);
            //Spawn a Treasure 
            Instantiate(treasure[treasures], transform.position, transform.rotation);
        }        
        else
        {
            //Spawn Nothing
        }

    }
}
