using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathParticle : MonoBehaviour {

    public float scalingTime = 1f;
    float timer;
    float defaultScale;

    void Start()
    {
        defaultScale = transform.localScale.x;
    }

	void Update () {
        timer += Time.deltaTime;
        float currentScale = Mathf.Lerp(defaultScale, 0, timer / scalingTime);
        transform.localScale = new Vector3(currentScale, currentScale, currentScale);
	}
}
