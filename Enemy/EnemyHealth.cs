using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHealth : MonoBehaviour {

    public const int enemyMaxHealth = 100;
    public int currentHealth = enemyMaxHealth;

    public RectTransform healthBar;

    public GameObject[] dropItems;
    public GameObject key;

    public GameObject enemy;

    private BossFightStarted bossFightStartedScript;

    public GameObject[] doors;


    void Start()
    {
        bossFightStartedScript = GetComponent<BossFightStarted>();
    }

    public void EnemyTakeDamage(int amount)
    {
        currentHealth -= amount;

        if (currentHealth <= 0)
        {
            Debug.Log("Enemy Died!");
            EnemyDied();
        }

        healthBar.sizeDelta = new Vector2(currentHealth, healthBar.sizeDelta.y);
    }

    void EnemyDied()
    {
        if (gameObject.tag == "Enemy")
        {
            int randomIndex = Random.Range(0, dropItems.Length);
            Instantiate(dropItems[randomIndex], transform.position, transform.rotation);
            Destroy(enemy);
        }

        if (gameObject.tag == "Boss")
        {
            DoorsOpen(false);
            Instantiate(key, transform.position, transform.rotation);
            Destroy(enemy);
        }
    }

    public void DoorsOpen(bool doorOpen)
    {
        doors[0].SetActive(doorOpen);
        doors[1].SetActive(doorOpen);
    }
}
