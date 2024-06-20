using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_FollowMirrorPlayerDroneNoXR : MonoBehaviour
{

    public Transform m_whatToMove;
    public Transform m_whatToFollow;
    public float m_lerpPower = 1;
    public float m_lerpPowerRotation = 0.85f;

    public bool m_useLateUpdate = false;


    private void Update()
    {
        if(!m_useLateUpdate)
            UpdateLerp();
    }
    public void LateUpdate()
    {
        if (m_useLateUpdate)
            UpdateLerp();
    }

    private void UpdateLerp()
    {
        if (m_whatToFollow == null && MSoccerMono_PlayerInput.PlayerInstanceInScene != null)
        {

            Rigidbody droneRig = MSoccerMono_PlayerInput.PlayerInstanceInScene.GetComponentInChildren<Rigidbody>();
            if (droneRig != null)
                m_whatToFollow = droneRig.transform;
        }

        if (m_whatToFollow != null)
        {
            m_whatToMove.position = Vector3.Lerp(m_whatToFollow.position, m_whatToMove.position, Time.deltaTime * m_lerpPower);
            m_whatToMove.rotation = Quaternion.Lerp(m_whatToFollow.rotation, m_whatToMove.rotation, Time.deltaTime * m_lerpPowerRotation);
        }
    }
}
