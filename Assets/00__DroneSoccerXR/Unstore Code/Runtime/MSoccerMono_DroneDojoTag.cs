using System;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_DroneDojoTag: MonoBehaviour {

    public MirrorMono_PublicRsaKeyOwner m_dojoDroneOwner;
    public static List<MSoccerMono_DroneDojoTag> m_droneDojoInScene= new List<MSoccerMono_DroneDojoTag>();

    public void Awake()
    {
        m_droneDojoInScene.Remove(this);
        m_droneDojoInScene.Add(this);
    }

    public void OnDestroy()
    {
        m_droneDojoInScene.Remove(this);
    }

    public static void GetPlayerDojoDrone(string rsa, out bool found, out MSoccerMono_AbstractGamepad gamepad)
    {
        for (int i = 0; i < m_droneDojoInScene.Count; i++)
        {
            if (i< m_droneDojoInScene.Count && m_droneDojoInScene[i].m_dojoDroneOwner.IsOwnedByExactly(rsa))
            {
                found = true;
                gamepad = m_droneDojoInScene[i].m_dojoDroneOwner.GetComponent<MSoccerMono_AbstractGamepad>();
                return;
            }
        }
        gamepad = null;
        found = false; 
    }
}