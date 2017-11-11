using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerList : Photon.PunBehaviour {

    public GameObject playerUIPrefab;
    public List<GameObject> playerPrefabs = new List<GameObject>();
    public float spacer = 20f;

    void ClearPlayers()
    {
        foreach(GameObject go in playerPrefabs)
        {
            Destroy(go);
        }
        playerPrefabs.Clear();
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        base.OnPhotonPlayerConnected(newPlayer);
        StartCoroutine(WaitAndTakePlayers());
    }

    public override void OnPhotonPlayerDisconnected(PhotonPlayer otherPlayer)
    {
        base.OnPhotonPlayerDisconnected(otherPlayer);
        StartCoroutine(WaitAndTakePlayers());
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        StartCoroutine(WaitAndTakePlayers());
    }

    IEnumerator WaitAndTakePlayers()
    {
        yield return new WaitForSeconds(0.75f);
        int iter = 0;
        ClearPlayers();
        foreach (PhotonPlayer pp in PhotonNetwork.otherPlayers)
        {
            GameObject inst = Instantiate(playerUIPrefab, transform);
            inst.transform.localPosition = new Vector3(inst.transform.localPosition.x, inst.transform.localPosition.y - spacer * iter, inst.transform.localPosition.z);
            inst.name = pp.ID.ToString();
            inst.GetComponentInChildren<Text>().text = pp.NickName;
            playerPrefabs.Add(inst);
            iter++;
        }
    }
}
