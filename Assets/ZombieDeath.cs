using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZombieDeath : MonoBehaviour {

    public Rigidbody[] particles;
    public float minForce = 10f;
    public float maxForce = 20f;

    void Start () {
        foreach(Rigidbody r in particles)
        {
            float x = Random.Range(-1, 1) == 0 ? -1 * Random.Range(minForce, maxForce) : Random.Range(minForce, maxForce);
            float z = Random.Range(-1, 1) == 0 ? -1 * Random.Range(minForce, maxForce) : Random.Range(minForce, maxForce);
            Vector3 force = new Vector3(x, 0, z);
            r.AddForce(force, ForceMode.Impulse);
        }
        Destroy(gameObject, 3);
	}

}
