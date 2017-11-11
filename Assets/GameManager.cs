using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class GameManager : Photon.PunBehaviour, IPunObservable
{
    public GameObject player;
    static GameObject staticPlayer;
    public GameObject endGameScreen;
    public int Kills
    {
        get
        {
            return kills;
        }
        set
        {
            endGameScreen.GetComponent<EndGameScreen>().killsText.text = "Kills: " + value;
            kills = value;
        }
    }
    public int kills;
    public Slider playerHealthSlider;
    public CanvasGroup playerRedScreen;
    public static List<GameObject> players = new List<GameObject>();
    public GameObject startGameScreen;
    public Transform playerList;
    public bool dead = false;
    public static string playerName;

    public void StartGame()
    {
        if(PhotonNetwork.isMasterClient)
        {
            FindObjectOfType<Spawner>().level = int.Parse(startGameScreen.GetComponent<StartGameScreen>().startingLevel.text);
            FindObjectOfType<Spawner>().levelIncrease = int.Parse(startGameScreen.GetComponent<StartGameScreen>().levelIncrease.text);
            PhotonNetwork.room.IsOpen = false;
            FindObjectOfType<RoomGenerator>().seed = UnityEngine.Random.Range(int.MinValue, int.MaxValue);
        }
        photonView.RPC("RpcStartGame", PhotonTargets.All, FindObjectOfType<RoomGenerator>().seed);
    }

    [PunRPC]
    void RpcStartGame(int seed)
    {

        FindObjectOfType<RoomGenerator>().GenerateNewMap(seed);
        startGameScreen.GetComponent<StartGameScreen>().StartGame();
        GameObject instantPlayer = PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity, 0);
        staticPlayer = instantPlayer;
        //FindObjectOfType<Spawner>().StartSpawning();
        FindObjectOfType<CameraFollow>().target = instantPlayer.transform;
        PhotonNetwork.player.SetScore(1);
        FindObjectOfType<Spawner>().StartSpawning();
    }

    public void ConnectOrCreate()
    {
        PhotonNetwork.sendRate = 60;
        PhotonNetwork.sendRateOnSerialize = 30;
        PhotonNetwork.ConnectUsingSettings("1.0");
    }

    public override void OnJoinedLobby()
    {
        RoomOptions options = new RoomOptions();
        options.MaxPlayers = 4;
        options.IsOpen = true;
        options.EmptyRoomTtl = 100;
        PhotonNetwork.JoinOrCreateRoom("1", options, TypedLobby.Default);
    }

    public override void OnPhotonPlayerConnected(PhotonPlayer newPlayer)
    {
        base.OnPhotonPlayerConnected(newPlayer);
    }

    public static GameObject GetPlayer()
    {
        return staticPlayer;
    }

    public void RespawnPlayer()
    {
        if (dead)
        {
            dead = false;
            GameObject instantPlayer = PhotonNetwork.Instantiate(player.name, Vector3.zero, Quaternion.identity, 0);
            staticPlayer = instantPlayer;
            FindObjectOfType<CameraFollow>().target = instantPlayer.transform;
            PhotonNetwork.player.SetScore(1);
            endGameScreen.SetActive(false);
        }
    }

    [PunRPC]
    public void RpcGameOver()
    {
        if (dead)
        {
            endGameScreen.SetActive(true);
            endGameScreen.GetComponent<EndGameScreen>().OnEnable();
        }
    }

    public void GameOver()
    {
        photonView.RPC("RpcGameOver", PhotonTargets.All);
    }

    public void RestartGame()
    {
        photonView.RPC("RpcRestartGame", PhotonTargets.All);
    }

    [PunRPC]
    void RpcRestartGame()
    {
        FindObjectOfType<Spawner>().level = 0;
        foreach (GameObject go in Spawner.enemies)
        {
            Destroy(go);
        }
        RespawnPlayer();
        Kills = 0;
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(Kills);
        }
        else
        {
            Kills = (int)stream.ReceiveNext();
        }
    }
}
