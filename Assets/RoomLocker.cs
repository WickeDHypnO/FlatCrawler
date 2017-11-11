using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoomLocker : MonoBehaviour {

    public List<GameObject> doors;

    public void LockRoom()
    {
        ShutDownDoors();
    }

    public void UnlockRoom()
    {
        OpenDoors();
    }

    private void OpenDoors()
    {
        foreach (GameObject go in doors)
        {
            go.SetActive(false);
        }
    }

    private void ShutDownDoors()
    {
        foreach(GameObject go in doors)
        {
            go.SetActive(true);
        }
    }

    
}
