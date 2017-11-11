using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieAttack : Photon.PunBehaviour
{
    Transform player;
    float timer = 0f;
    public float attackDelay = 2f;
    public float attackDistance = 1f;
    PlayerTargeter targeter;

    void Start()
    {
        targeter = GetComponent<PlayerTargeter>();
    }

    void FixedUpdate()
    {
        if(!PhotonNetwork.isMasterClient)
        {
            return;
        }
        timer += Time.deltaTime;
        if (targeter.target && Vector3.Distance(transform.position, targeter.target.transform.position) < attackDistance)
        {
            if (timer > attackDelay)
            {
                photonView.RPC("RpcAttackPlayer", PhotonTargets.All, targeter.target.GetPhotonView().viewID);
                timer = 0f;
            }
        }
    }

    [PunRPC]
    void RpcAttackPlayer(int viewID)
    {
        PhotonView.Find(viewID).GetComponent<PlayerHealth>().TakeDamage(10f);
    }

    void AttackPlayer()
    {
        targeter.target.GetComponent<PlayerHealth>().TakeDamage(10f);
    }
}

