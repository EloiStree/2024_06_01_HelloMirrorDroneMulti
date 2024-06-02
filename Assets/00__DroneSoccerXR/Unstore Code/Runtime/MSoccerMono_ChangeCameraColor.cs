using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using Random = UnityEngine.Random;

public class MSoccerMono_ChangeCameraColor : NetworkBehaviour
{


    public MSoccerMono_SyncOnServerLocalDateTime m_serverTime;


    public Color m_nextColor= Color.white;
    public Color m_nextColorBlack= Color.black;

    public void ChangeMainCameraColor(Color color) {
        if (isOwned && isLocalPlayer) { 
            if (Camera.main)
                Camera.main.backgroundColor = color;
        }
    }


    public long m_previousTime;
    public long m_currentTime;
    public void Update()
    {

        if (isOwned && isLocalPlayer) { 
            m_serverTime.GetEstimatedServerDateTime(out DateTime serverTime);
        
            m_currentTime = serverTime.Ticks/TimeSpan.TicksPerSecond;
            if (m_currentTime != m_previousTime) { 
                m_previousTime = m_currentTime;
                ChangeMainCameraColor(m_currentTime%2==0? m_nextColorBlack:m_nextColor);
            }
        }
        
    }


    [ContextMenu("Set new random color")]
    [ServerCallback]
    public void SetNewRandomColor() {
        RpcNextColor(new Color(Random.value, Random.value, Random.value,1));
    }


    [TargetRpc]
    public void RpcNextColor(Color newColor) {
        m_nextColor = newColor;
    }


}
