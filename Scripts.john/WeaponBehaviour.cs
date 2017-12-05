using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//arrays start with 0 , that's why I changed all the numbers from  1 2 3 to 0 1 2 

public class WeaponBehaviour : MonoBehaviour
{
    private PlatformController platformController;// refference to the PlatformerScript

    //weapon visable in players hand
    public GameObject[] visRangeWeapons;

    //the object that you shoot
    public GameObject[] weapons;    

    //the meleeweapon variant, is same as the object that you shoots but behaves different
    public GameObject[] meleeWeapons;    

    public GameObject[] slots;   

    public GameObject[] slotsActive;

    // the rotation point
    public Transform aimPoint;
    //weapon rate speed
    public float fireRate = 1.5f;
    //time to fire
    private float timeToFire = 1.5f;


    // amount of ammo 
    public int ammoKnife = 10000;
    public int ammoAxe = 0;
    public int ammoSpear = 0;

    // keeps track of what weapon is active
    private int activeWeapon = 0;

    // checks if player picked up a weapon
    private bool weaponChoise1 = true;
    private bool weaponChoise2 = false;
    private bool weaponChoise3 = false;

    private float ammoBoost;
    private float boostTime = 3f;


    // Use this for initialization
    void Start()
    {
        platformController = GetComponent<PlatformController>();//to get a acces to the platformController script
    }

    // Update is called once per frame
    void Update()
    {

        ///////////////////////////////////////////////////////////////KNIFE////////////////////////////////////////////////////////
        if (weaponChoise1 == true)
        {
            slots[0].SetActive(true);

            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                visRangeWeapons[0].SetActive(true);
                visRangeWeapons[1].SetActive(false);
                visRangeWeapons[2].SetActive(false);

                slotsActive[0].SetActive(true);
                slotsActive[1].SetActive(false);
                slotsActive[2].SetActive(false);
               
                activeWeapon = 1;
            }
        }
        if (activeWeapon == 1 && ammoKnife > 0)
        {
            timeToFire = 2f;

            if ( Input.GetMouseButtonDown(0) && Time.time > timeToFire)
            {
                timeToFire = Time.time + fireRate;
                ammoKnife--;
                Instantiate(weapons[0], aimPoint.position, aimPoint.rotation);                
            }
            else
            {
                Debug.Log("No Knife Ammo");
            }
        }
        ///////////////////////////////////////////////////////////////AXE////////////////////////////////////////////////////////
        if (weaponChoise2 == true)
        {
            slots[1].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                visRangeWeapons[0].SetActive(false);
                visRangeWeapons[1].SetActive(true);
                visRangeWeapons[2].SetActive(false);

                slotsActive[0].SetActive(false);
                slotsActive[1].SetActive(true);
                slotsActive[2].SetActive(false);

                activeWeapon = 2;
            }
        }

        if (activeWeapon == 2 && ammoAxe > 0)
        {
            timeToFire = 1.5f;            
            if (Input.GetMouseButtonDown(0) && Time.time > timeToFire)
            {
                timeToFire = Time.time + fireRate;
                ammoAxe--;
                Instantiate(weapons[1], aimPoint.position, aimPoint.rotation);
            }             
            else
            {
                Debug.Log("No Ammo For Axe");
            }
        }
        ///////////////////////////////////////////////////////////////SOMETHING ELSE////////////////////////////////////////////////////////
        if (weaponChoise3 == true)
        {
            slots[2].SetActive(true);
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                visRangeWeapons[0].SetActive(false);
                visRangeWeapons[1].SetActive(false);
                visRangeWeapons[2].SetActive(true);

                slotsActive[0].SetActive(false);
                slotsActive[1].SetActive(false);
                slotsActive[2].SetActive(true);

                activeWeapon = 3;
            }
        }

        if (ammoSpear > 0 && activeWeapon == 3 )
        {
            timeToFire = 2f;
                        
            if (Input.GetMouseButtonDown(0) && Time.time > timeToFire)
            {
                timeToFire = Time.time + fireRate;
                ammoSpear--;
                Instantiate(weapons[2], aimPoint.position, aimPoint.rotation);
            }
            else
            {
                Debug.Log("No Ammo For Axe");
            }
        }
    }     

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "PickUp1")//Knife Weapon Pickup
        {
            Destroy(other.gameObject, 0);
            weaponChoise1 = true;
            //ammoKnife =+ 3;
        }

        if (other.tag == "PickUp2")// Axe Weapon Pickup
        {
            Destroy(other.gameObject, 0);
            weaponChoise2 = true;
            //ammoAxe =+ 2;
        }

        if (other.tag == "PickUp3")//Spear Weapon Pickup
        {
            Destroy(other.gameObject, 0);
            weaponChoise3 = true;
            //ammoSpear =+ 2;
        }

        if (other.tag == "PickUp4")//Ammo boost pickup ?
        {
            Destroy(other.gameObject, 0);
            ammoBoost = Time.time + boostTime;
        }

        if (other.tag == "PickUp5")//Player speedboost Pickup
        {
            Destroy(other.gameObject, 0);
            platformController.maxSpeed = 10; //player speed is 10
        }

        if (other.tag == "Knife Ammo")//Ammo Knife Pickup
        {
            Destroy(other.gameObject, 0);
            ammoKnife++;
        }
        if (other.tag == "Axe Ammo")//Ammo Axe Pickup
        {
            Destroy(other.gameObject, 0);
            ammoAxe++;
        }
        if (other.tag == "Spear Ammo")//Ammo spear Pickup
        {
            Destroy(other.gameObject, 0);
            ammoSpear++;
        }
    }
}



















