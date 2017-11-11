using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerRotation : Photon.PunBehaviour
{

    Rigidbody rigid;
    // Use this for initialization
    void Start()
    {
        rigid = GetComponent<Rigidbody>();
    }

    void LateUpdate()
    {
        if (photonView.isMine)
        {
            Plane playerPlane = new Plane(Vector3.up, transform.position);

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);

            float hitdist = 0.0f;
            if (playerPlane.Raycast(ray, out hitdist))
            {
                Vector3 targetPoint = ray.GetPoint(hitdist);
                Quaternion targetRotation = Quaternion.LookRotation(targetPoint - transform.position);
                rigid.MoveRotation(targetRotation);
            }
        }
    }
}
