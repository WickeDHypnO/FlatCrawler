using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerHealth : Photon.PunBehaviour, IPunObservable
{
    public float maxHealth = 100f;
    float health;
    bool flashing;
    float timer = 0f;
    public Slider healthSlider;
    public CanvasGroup redScreen;
    public GameObject deathPrefab;

    void OnEnable()
    {
        health = maxHealth;
        GameManager.players.Add(gameObject);
        if (photonView.isMine)
        {
            healthSlider = FindObjectOfType<GameManager>().playerHealthSlider;
            redScreen = FindObjectOfType<GameManager>().playerRedScreen;
            healthSlider.value = 1;
        }
        if (photonView.owner != PhotonNetwork.player)
        {
            foreach(GameObject go in FindObjectOfType<PlayerList>().playerPrefabs)
            {
                if(go.name == photonView.ownerId.ToString())
                {
                    healthSlider = go.GetComponentInChildren<Slider>();
                    healthSlider.value = 1;
                }
            } 
        }
    }

    void StartFlashing()
    {
        flashing = true;
        timer = 0f;
    }

    void Update()
    {
        if (flashing)
        {
            timer += Time.deltaTime;
            redScreen.alpha = Mathf.Lerp(1, 0, timer);
            if (timer >= 1f)
            {
                flashing = false;
                timer = 0f;
            }
        }
    }

    public void TakeDamage(float delta)
    {
        health -= delta;
        healthSlider.value = health / maxHealth;
        if (photonView.isMine)
        {
            StartFlashing();
            if (health <= 0)
            {
                PhotonNetwork.player.SetScore(0);
                FindObjectOfType<GameManager>().dead = true;
                flashing = false;
                redScreen.alpha = 0;
                FindObjectOfType<GameManager>().GameOver();
                PhotonNetwork.Destroy(gameObject);
            }
        }
    }

    void OnDestroy()
    {
        Instantiate(deathPrefab, transform.position, transform.rotation);
        GameManager.players.Remove(gameObject);
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(health);
        }
        else
        {
            health = (float)stream.ReceiveNext();
        }
    }
}
