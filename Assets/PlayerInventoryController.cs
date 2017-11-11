using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerInventoryController : MonoBehaviour
{

    public List<Weapon> weapons;
    public List<Item> items;
    public int slots = 10;
    public Weapon test;


    public void PickUpItem(Item item)
    {
        if (items.Count + weapons.Count < slots)
        {
            if (item is Weapon)
            {
                item.transform.SetParent(transform);
                item.GetComponentsInChildren<Collider>()[1].enabled = false;
                item.model.SetActive(false);
                var wpn = item as Weapon;
                weapons.Add(wpn);
            }
            else
            {
                items.Add(item);
            }
        }
    }

    private void Start()
    {

        if (test)
            PickUpItem(test);
    }
}
