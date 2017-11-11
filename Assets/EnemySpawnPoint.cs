using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnPoint : MonoBehaviour {

	void OnEnable()
    {
        FindObjectOfType<Spawner>().spawns.Add(transform);
    }
}
