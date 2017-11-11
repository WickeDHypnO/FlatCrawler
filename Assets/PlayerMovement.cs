using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : Photon.PunBehaviour
{

    float horizontal, vertical;
    Rigidbody rigid;
    public float speed = 1f;
    bool owner = true;
    public PlayerAnimator anim;

    void Start()
    {
        if (!photonView.isMine)
        {
            owner = false;
            return;
        }
        rigid = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if(!owner)
        {
            return;
        }
        if (Input.GetAxis("Horizontal") != 0)
        {
            horizontal = Input.GetAxis("Horizontal");
        }
        else
        {
            horizontal = 0;
        }
        if (Input.GetAxis("Vertical") != 0)
        {
            vertical = Input.GetAxis("Vertical");
        }
        else
        {
            vertical = 0;
        }
    }

    void FixedUpdate()
    {
        if(horizontal != 0 || vertical != 0)
        {
            rigid.MovePosition(transform.position + Vector3.right * speed * horizontal + Vector3.forward * speed * vertical);
            anim.Run(Vector3.right * horizontal + Vector3.forward * vertical);
        }
        else
        {
            if(owner)
            anim.Idle();
        }

    }
}
