using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour {

    public List<Item> inventory = new List<Item>();
    private ItemDataBase dataBase;

	// Use this for initialization
	void Start ()
    {
        dataBase = GameObject.FindGameObjectWithTag("Item Database").GetComponent<ItemDataBase>();
        inventory.Add(dataBase.items[0]);
        inventory.Add(dataBase.items[1]);


    }

    // Update is called once per frame
    void Update () {
		
	}

    void OnGUI()
    {
        for (int i = 0; i < inventory.Count; i++)
        {
            GUI.Label(new Rect(10, i * 20, 200, 50), inventory[i].itemName);
        }
    }
}
