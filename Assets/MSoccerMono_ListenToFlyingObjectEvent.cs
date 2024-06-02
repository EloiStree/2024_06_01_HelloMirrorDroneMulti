using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class MSoccerMono_ListenToFlyingObjectEvent : NetworkBehaviour
{


    public long m_serverLocalDateTime;
    public long m_clientLocalDateTime;
    public long m_delayBetweenServerToLocalClock;
    public bool m_isOnServer;
    public void OnEnable()
    {
        m_isOnServer = isServer;
      
            InvokeRepeating("PushServerTimeToClient", 0, 1);
        
    }


    public void PushServerTimeToClient() {

        m_isOnServer = isServer;
        if (m_isOnServer)
            RpcPushServerTimeToClient(DateTime.UtcNow.Ticks);

    }

    [TargetRpc]
    public void RpcPushServerTimeToClient(long serverTime) {
        m_serverLocalDateTime = serverTime;
        m_clientLocalDateTime = DateTime.UtcNow.Ticks;
        m_delayBetweenServerToLocalClock = m_serverLocalDateTime - m_clientLocalDateTime;

        CmdKeepAwareOfLocalTime(m_serverLocalDateTime, m_clientLocalDateTime);
    }

    [Command]
    public void CmdKeepAwareOfLocalTime(long serverLocalTime, long clientLocalTime) {
        m_serverLocalDateTime = serverLocalTime;
        m_clientLocalDateTime = clientLocalTime;
        m_delayBetweenServerToLocalClock = m_serverLocalDateTime - m_clientLocalDateTime;

    }


}
