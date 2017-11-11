using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShotBehavior : Photon.PunBehaviour
{
    public float speed = 1f;
    public float explosionForce = 1f;
    public float explosionRadius = 1f;
    public float damage = 10f;
    public GameObject explpsionEffect;
    public ParticleSystem smoke;
    Rigidbody rigid;

    void Start()
    {
        rigid = GetComponent<Rigidbody>();
        Destroy(gameObject, 10f);
    }

    void FixedUpdate()
    {
        rigid.MovePosition(transform.position + transform.forward * speed);
    }

    void OnTriggerEnter()
    {
        foreach (Collider c in Physics.OverlapSphere(transform.position, explosionRadius))
        {
            if (c.GetComponent<Rigidbody>() && c.tag != "Shot")
            {
                //c.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);

                if (c.GetComponent<ZombieHealth>())
                {
                    if (photonView.isMine)
                    {
                        c.GetComponent<ZombieHealth>().DealDamage(damage * (explosionRadius - Vector3.Distance(transform.position, c.transform.position)));
                        //c.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
                    }
                }
                else
                {
                   // c.GetComponent<Rigidbody>().AddExplosionForce(explosionForce, transform.position, explosionRadius);
                }
            }
        }

        Instantiate(explpsionEffect, transform.position, Quaternion.identity);
        smoke.transform.parent = null;
        smoke.Stop();
        Destroy(smoke.gameObject, 2f);
        Destroy(gameObject);
    }
}
