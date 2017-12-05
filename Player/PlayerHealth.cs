using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.Networking;

public class PlayerHealth : MonoBehaviour
{    

    public RectTransform healthBar;
    private float maxHealthBarSize;
    private float currentHealthBarSize;

    public GameObject fadeOutImage;
    public LevelManager levelmanager;

    void Awake()
    {
        currentHealthBarSize = healthBar.sizeDelta.x;
        maxHealthBarSize = healthBar.sizeDelta.x;
    }

   

    public void TakeDamage(int amount)
    {


        currentHealthBarSize -= amount;

        if (healthBar.sizeDelta.x <= 0)
        {
            Debug.Log("Player Died!");
            fadeOutImage.SetActive(true);

            Invoke("PlayerDied", 7f);

        }

        healthBar.sizeDelta = new Vector2(currentHealthBarSize, healthBar.sizeDelta.y);
    }

    public void addHealth(int amount)
    {
        currentHealthBarSize += amount;
        if (currentHealthBarSize > maxHealthBarSize)// if we gain more health then the max health 
        {
            currentHealthBarSize = maxHealthBarSize;
        }

        healthBar.sizeDelta = new Vector2(currentHealthBarSize, healthBar.sizeDelta.y);
    }

    void PlayerDied()
    {       
        levelmanager.LoadLevel("01a Start Menu");
    }




}