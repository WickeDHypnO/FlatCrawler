using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Spawner : Photon.PunBehaviour, IPunObservable {

    public List<Transform> spawns;
    public Text remainingMonsters;
    public Text waveNumber;
    public float delayBetweenWaves = 2f;
    public float delayBetweenSpawns = 1f;
    public static List<GameObject> enemies = new List<GameObject>();
    public int level = 1;
    public int levelIncrease = 1;
    public GameObject enemy;
    bool nextWave;

    static public Spawner instance;

    void Start () {
        enemies.Clear();
        instance = this;
    }

    public void StartSpawning()
    {
        if(PhotonNetwork.isMasterClient)
        StartCoroutine(StartNextWaveAfterDelay(0,false));
        if(instance)
        instance.waveNumber.text = instance.level.ToString();
    }

    public static void AddEnemy(GameObject enemy)
    {
        enemies.Add(enemy);
        if(instance)
        instance.remainingMonsters.text = enemies.Count.ToString();
    }

    public static void DeleteEnemy(GameObject enemy)
    {
        enemies.Remove(enemy);
        if(instance)
        instance.remainingMonsters.text = enemies.Count.ToString();
        if (enemies.Count <= 0)
        {
            if (instance)
            {
                instance.level += instance.levelIncrease;
                instance.waveNumber.text = instance.level.ToString();
                if (PhotonNetwork.isMasterClient)
                instance.StartCoroutine(instance.StartNextWaveAfterDelay(instance.delayBetweenWaves, true));
            }
        }
    }

    IEnumerator StartNextWaveAfterDelay(float delay, bool restarting)
    {
        if(restarting)
        photonView.RPC("RpcRespawnPlayer", PhotonTargets.All);
        yield return new WaitForSeconds(delay);
        for (int i = 0; i < level; i++)
        {
            foreach (Transform t in spawns)
            {
                PhotonNetwork.InstantiateSceneObject(enemy.name, t.position, t.rotation, 0, null);
            }
            yield return new WaitForSeconds(delayBetweenSpawns);
        }
    }

    [PunRPC]
    void RpcRespawnPlayer()
    {
        FindObjectOfType<GameManager>().RespawnPlayer();
    }

    public void OnPhotonSerializeView(PhotonStream stream, PhotonMessageInfo info)
    {
        if (stream.isWriting)
        {
            stream.SendNext(instance.level);
        }
        else
        {
            instance.level = (int)stream.ReceiveNext();
        }
    }
}
