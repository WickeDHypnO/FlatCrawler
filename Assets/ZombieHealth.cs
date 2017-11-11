using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ZombieHealth : Photon.PunBehaviour {

    float health = 50f;
    public float maxHealth = 50f;
    public GameObject deathPrefab;
    public Slider healthSlider;

    [PunRPC]
    void RpcDealDamage(float delta)
    {
        health -= delta;
        healthSlider.value = health / maxHealth;
        if (health <= 0)
        {
            if (PhotonNetwork.isMasterClient)
            {
                FindObjectOfType<GameManager>().Kills++;          
            }
            Destroy(gameObject);
        }
    }

    void OnDestroy()
    {
        Instantiate(deathPrefab, transform.position, transform.rotation);
        GetComponent<DropItem>().DropItemForPlayer();
        GetComponent<DropItem>().DropWeapon();
    }

    public void DealDamage(float delta)
    {
        photonView.RPC("RpcDealDamage", PhotonTargets.All, delta);
    }
}
