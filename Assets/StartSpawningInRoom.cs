using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartSpawningInRoom : MonoBehaviour {

    public Spawner spawner;

	void OnTriggerEnter()
    {
        spawner.StartSpawning();
    }
}
