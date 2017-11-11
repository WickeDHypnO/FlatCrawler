using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerName : Photon.PunBehaviour {

    public Text playerName;

    void OnEnable()
    {
        if (photonView.isMine)
        {
            photonView.RPC("RpcSetName", PhotonTargets.Others, GameManager.playerName);
            playerName.text = "";
        }
    }
	// Update is called once per frame
	void Update () {
        playerName.transform.rotation = Quaternion.identity; 
	}

    [PunRPC]
    void RpcSetName(string name)
    {
        playerName.text = name;
    }
}
