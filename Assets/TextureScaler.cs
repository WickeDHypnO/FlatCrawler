using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextureScaler : MonoBehaviour {

    public float multiplier;

    [ContextMenu("Scale textures")]
	void OnEnable () {
		foreach(MeshRenderer mr in GetComponentsInChildren<MeshRenderer>())
        {
            mr.material.SetTextureScale("_MainTex", new Vector2(mr.transform.lossyScale.x * multiplier, mr.transform.lossyScale.z * multiplier));
        }
	}
}
