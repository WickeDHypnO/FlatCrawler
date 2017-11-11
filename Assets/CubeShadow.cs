using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CubeShadow : MonoBehaviour {

    public Transform target;
    public float offset = -0.25f;
	
	void Update () {
        transform.position = new Vector3(target.position.x, target.position.y + offset, target.position.z);
	}
}
