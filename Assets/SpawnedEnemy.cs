using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnedEnemy : MonoBehaviour {

    void OnEnable()
    {
        Spawner.AddEnemy(gameObject);
    }
	
	void OnDestroy()
    {
        Spawner.DeleteEnemy(gameObject);
    }
}
