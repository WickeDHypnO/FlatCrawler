using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EndGameScreen : MonoBehaviour
{
    public Text killsText;
    public GameObject restartGameButton;

    public void OnEnable()
    {
        if (PhotonNetwork.isMasterClient && !IsAnyPlayerAlive())
        {
            restartGameButton.SetActive(true);
        }
        //killsText.text += FindObjectOfType<GameManager>().kills;
    }

    bool IsAnyPlayerAlive()
    {
        if (GameManager.players.Count > 1)
        {
            return true;
        }
        return false;
    }
}
