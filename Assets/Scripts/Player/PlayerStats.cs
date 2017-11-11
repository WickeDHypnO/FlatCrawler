using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStats : MonoBehaviour {

    public int level;
    public int experience;

	void Start () {
		for(int i = 1; i < 20; i++)
        {
         //  Debug.Log(i + " : " + DebugLevelFuntion(i));
        }
	}

    float DebugLevelFuntion(int x)
    {
       return (int)(60f*Mathf.Pow(x,2.8f) - 60f);
    }
}
