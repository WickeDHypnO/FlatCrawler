using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour {

    public Transform target;
    public float offsetX;
    public float offsetZ;

	void Update () {
        if(target)
        transform.position = new Vector3(target.position.x + offsetX, transform.position.y, target.position.z + offsetZ);
	}
}
