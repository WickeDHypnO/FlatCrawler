using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PlayerTargeter : Photon.PunBehaviour, IPunObservable
{
    NavMeshAgent agent;
    public GameObject target;
    Vector3 destination;
    void Start()
    {
        agent = GetComponent<NavMeshAgent>();
    }

    void FixedUpdate()
    {
        if (PhotonNetwork.isMasterClient)
        {
            float dist = float.MaxValue;

            foreach (GameObject go in GameManager.players)
            {
                float tempDistance = Vector3.Distance(transform.position, go.transform.position);
                if (tempDistance < dist)
                {
                    destination = go.transform.position;
                    target = go;
                    dist = tempDistance;
                }
            }
            agent.SetDestination(destination);
        }
        
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(destination);
        }
        else
        {
            destination = (Vector3)stream.ReceiveNext();
        }
    }
}
