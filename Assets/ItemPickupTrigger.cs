using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemPickupTrigger : MonoBehaviour {

	void OnTriggerEnter(Collider col)
    {
        if(col.tag == "Player" && col.GetComponent<PhotonView>().isMine)
        {
            GetComponentInParent<Item>().CollectItem();
        }
    }
}
