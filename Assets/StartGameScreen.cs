using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StartGameScreen : Photon.PunBehaviour
{
    public InputField playerNameInput;
    public GameObject[] masterClientOptions;
    public GameObject connectButton;
    public GameObject startButton;
    public GameObject joinedText;

    public InputField startingLevel;
    public InputField levelIncrease;

    public override void OnCreatedRoom()
    {
        base.OnCreatedRoom();
        GameManager.playerName = playerNameInput.text;
        PhotonNetwork.player.NickName = playerNameInput.text;
        Debug.Log("inRoom");
        connectButton.SetActive(false);
        startButton.SetActive(true);
        foreach (GameObject go in masterClientOptions)
        {
            go.SetActive(true);
        }
    }

    public override void OnJoinedRoom()
    {
        base.OnJoinedRoom();
        GameManager.playerName = playerNameInput.text;
        PhotonNetwork.player.NickName = playerNameInput.text;
        if (!PhotonNetwork.isMasterClient)
        {
            connectButton.SetActive(false);
            joinedText.SetActive(true);
        }
    }

    public void StartGame()
    {
        gameObject.SetActive(false);
    }
}
