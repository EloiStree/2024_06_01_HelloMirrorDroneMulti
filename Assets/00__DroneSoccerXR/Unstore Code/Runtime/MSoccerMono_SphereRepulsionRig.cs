using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_SphereRepulsionRig : MonoBehaviour
{

    public Transform m_centerAnchor;
    public Rigidbody m_rigibodyToAffect;

    public float m_continusRepulsionForce = 1000;
    public ForceMode m_continusForce = ForceMode.Force;
    public float m_enterRepulsionForce = 1000;
    public ForceMode m_enterForce = ForceMode.Force;


    private void Reset()
    {
        m_centerAnchor = transform;
        m_rigibodyToAffect = GetComponent<Rigidbody>();
    }

    public void OnCollisionEnter(Collision collision)
    {
        MSoccerMono_SphereRepulsionRig sphereRepulsionRig = collision.gameObject.GetComponent<MSoccerMono_SphereRepulsionRig>();
        if(sphereRepulsionRig == null)
        {
            sphereRepulsionRig= collision.gameObject.GetComponentInChildren<MSoccerMono_SphereRepulsionRig>();
        }
        if (sphereRepulsionRig != null)
        {
            Push(sphereRepulsionRig, m_enterRepulsionForce,m_continusForce);
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        MSoccerMono_SphereRepulsionRig sphereRepulsionRig = collision.gameObject.GetComponent<MSoccerMono_SphereRepulsionRig>();
        if (sphereRepulsionRig == null)
        {
            sphereRepulsionRig = collision.gameObject.GetComponentInChildren<MSoccerMono_SphereRepulsionRig>();
        }
        if (sphereRepulsionRig != null)
        {
            Push(sphereRepulsionRig, m_continusRepulsionForce, m_continusForce);

        }
    }

    private void Push(MSoccerMono_SphereRepulsionRig sphereRepulsionRig, float continusRepulsionForce, ForceMode continusForce)
    {
        Vector3 repulsion = sphereRepulsionRig.m_centerAnchor.position - m_centerAnchor.position;
        sphereRepulsionRig.m_rigibodyToAffect
            .AddForce(
            repulsion.normalized * continusRepulsionForce,
            continusForce);
    }

}
