using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Item {

    public string itemName;
    public int itemID;
    public string itemDisc;
    public Sprite spriteIcon;
    public int itemPower;
    public int itemSpeed;
    public ItemType itemType;

    public enum ItemType {
        Weapon, Consumable, Armor
    }

    public Item(string name, int id, string disc, string icon, int power, int speed, ItemType type)
    {
        itemName = name;
        itemID = id;
        itemDisc = disc;
        itemPower = power;
        itemSpeed = speed;
        itemType = type;


    }
}
