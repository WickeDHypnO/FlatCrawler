using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerAnimator : MonoBehaviour
{

    public Animator upperBody;
    public Transform legsDirection;
    public Animator legs;

    public void Run(Vector3 direction)
    {
        if(Vector3.Angle(transform.forward,direction) > 90)
        {
            if (!legs.GetBool("RunBack"))
            {
                legs.SetBool("RunBack", true);
                legs.SetBool("Run", false);
                legs.SetBool("Idle", false);
            }
            legsDirection.rotation = Quaternion.LookRotation(direction);
        }
        else
        {
            if (!legs.GetBool("Run"))
            {
                legs.SetBool("RunBack", false);
                legs.SetBool("Run", true);
                legs.SetBool("Idle", false);
            }
            legsDirection.rotation = Quaternion.LookRotation(-direction);
        }
    }

    public void Idle()
    {
        if (!legs.GetBool("Idle"))
        {
            legs.SetBool("RunBack", false);
            legs.SetBool("Run", false);
            legs.SetBool("Idle", true);
        }
    }
}
