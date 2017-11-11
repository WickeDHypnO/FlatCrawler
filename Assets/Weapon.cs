using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Weapon : Item {

    public float damage;
    public Material commonMaterial;
    public Material rareMaterial;
    public Material mythicMaterial;
    public Material legendaryMaterial;

    public void SetRarityMaterial()
    {
        switch (rarity)
        {
            case Rarity.Common:
                break;
            case Rarity.Rare:
                GetComponentInChildren<MeshRenderer>().material = rareMaterial;
                break;
            case Rarity.Mythic:
                GetComponentInChildren<MeshRenderer>().material = mythicMaterial;
                break;
            case Rarity.Legendary:
                GetComponentInChildren<MeshRenderer>().material = legendaryMaterial;
                break;
        }
    }
}
