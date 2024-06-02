using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class MSoccerMono_SyncOnServerLocalDateTime : NetworkBehaviour
{



    public long m_estimatedServerLocalDateTime;
    public long m_serverLocalDateTimeSent;
    public long m_serverLocalDateTimeReceived;
    public long m_clientLocalDateTimeReceived;
    public long m_delayBetweenServerToLocalClock;
    public long m_delayBetweenServerToLocalClockMinimumFound;

    public int m_delaySentReceivedFromServer;
    public int m_delaySentReceivedFromServerHalf;
    public double m_sentEstimateLagInMs;


    public void GetEstimatedServerDateTime(out DateTime timeOfServer) {

        if (m_isOnServer)
            timeOfServer = DateTime.UtcNow;
        else 
            timeOfServer = DateTime.UtcNow.AddTicks( m_delayBetweenServerToLocalClockMinimumFound);
    }


    private void Update()
    {
        GetEstimatedServerDateTime(out DateTime now);
        m_estimatedServerLocalDateTime = now.Ticks;
    }

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
        m_serverLocalDateTimeSent = serverTime;
        m_clientLocalDateTimeReceived = DateTime.UtcNow.Ticks;
        m_delayBetweenServerToLocalClock = m_serverLocalDateTimeSent - m_clientLocalDateTimeReceived;
        ComputeMinimumDelay();



        CmdKeepAwareOfLocalTime(m_serverLocalDateTimeSent, m_clientLocalDateTimeReceived);
    }

    [Command]
    public void CmdKeepAwareOfLocalTime(long serverLocalTime, long clientLocalTime)
    {
        m_serverLocalDateTimeReceived = DateTime.UtcNow.Ticks;
        m_serverLocalDateTimeSent = serverLocalTime;
        m_clientLocalDateTimeReceived = clientLocalTime;
        m_delayBetweenServerToLocalClock = m_serverLocalDateTimeSent - m_clientLocalDateTimeReceived;
        ComputeMinimumDelay();

        m_delaySentReceivedFromServer = (int)(m_serverLocalDateTimeReceived - m_serverLocalDateTimeSent);
        m_delaySentReceivedFromServerHalf = (int)(m_delaySentReceivedFromServer * 0.5f);
        m_sentEstimateLagInMs = m_delaySentReceivedFromServerHalf / TimeSpan.TicksPerMillisecond;


    }

    private void ComputeMinimumDelay()
    {
        if (m_delayBetweenServerToLocalClockMinimumFound == 0)
            m_delayBetweenServerToLocalClockMinimumFound = m_delayBetweenServerToLocalClock;
        else if (Math.Abs(m_delayBetweenServerToLocalClock) < Math.Abs(m_delayBetweenServerToLocalClockMinimumFound) && m_delayBetweenServerToLocalClock != 0)
            m_delayBetweenServerToLocalClockMinimumFound = m_delayBetweenServerToLocalClock;
    }
}
