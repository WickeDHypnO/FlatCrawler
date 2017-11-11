using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DropItem : MonoBehaviour {

    public GameObject itemPrefab;
    public GameObject weaponPrefab;

    public float itemChance = 10f;
    public float weaponChance = 5f;

    public List<float> rarityTable;

    public void DropItemForPlayer()
    {
        //if (Random.Range(0f, 100f) < weaponChance)
        //{
        //    var weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
        //}
    }

    public void DropWeapon()
    {
        if(Random.Range(0f,100f) < weaponChance)
        {
            var weapon = Instantiate(weaponPrefab, transform.position, Quaternion.identity);
            float rarity = Random.Range(0f, 100f);
            if(rarity < rarityTable[0])
            {
                weapon.GetComponent<Weapon>().rarity = Rarity.Legendary;
            }
            else if(rarity < rarityTable[1])
            {
                weapon.GetComponent<Weapon>().rarity = Rarity.Mythic;
            }
            else if(rarity < rarityTable[2])
            {
                weapon.GetComponent<Weapon>().rarity = Rarity.Rare;
            }
            else
            {
                weapon.GetComponent<Weapon>().rarity = Rarity.Common;
            }
            weapon.GetComponent<Weapon>().SetRarityMaterial();
            weapon.GetComponent<Rigidbody>().AddForce(new Vector3(Random.Range(10f, -10f),0, Random.Range(10f, -10f)), ForceMode.Impulse);
        }
    }
}
