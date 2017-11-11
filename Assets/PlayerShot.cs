using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerShot : Photon.PunBehaviour
{

    public GameObject shotPrefab;
    public float shotDelay = 0.2f;
    bool shot = false;
    float timer = 0f;

    void Update()
    {
        if (photonView.isMine)
        {
            timer += Time.deltaTime;
            if (Input.GetMouseButton(0) && timer > shotDelay)
            {
                shot = true;
                PhotonNetwork.Instantiate(shotPrefab.name, transform.position + transform.forward, Quaternion.LookRotation(transform.forward),0);
                shot = false;
                timer = 0f;
            }
        }
    }
}