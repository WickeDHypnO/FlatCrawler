using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomElements : MonoBehaviour {

    public List<GameObject> elements;
    public int enabledPercent = 50;

    void OnEnable()
    {
        foreach (GameObject go in elements)
        {
            if (Random.Range(1, 101) > enabledPercent)
            {
                go.SetActive(true);
            }
            else
            {
                go.SetActive(false);
            }
        }
    }
}
