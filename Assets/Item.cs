using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Rarity
{
    Common,
    Rare,
    Mythic,
    Legendary
}

public class Item : MonoBehaviour {

    public string name;
    public Sprite image;
    public GameObject model;
    public Rarity rarity;

    public void CollectItem()
    {
        FindObjectOfType<PlayerInventoryController>().PickUpItem(this);
    }
}
