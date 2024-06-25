using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoccerStateSyncMono_PushTiming : MonoBehaviour, MSoccerMono_RefreshablePush
{

    public S_DroneSoccerTimeValue m_timeInGame;
    public UnityEvent<S_DroneSoccerTimeValue> m_onTimeChanged;
    public MSoccerMono_SyncBoard m_matchManager;

    public void RefreshAndPush()
    {
        m_timeInGame.m_secondsSinceMatchStarted = m_matchManager.m_setTimeSinceMatchStarted;
        m_timeInGame.m_secondsSinceSetStarted = m_matchManager.m_setTimeSinceSetStarted;
        m_timeInGame.m_timeOfServerDateTimeUtcNowTicks =(ulong) DateTime.UtcNow.Ticks;
        m_onTimeChanged.Invoke(m_timeInGame);
    }

    IEnumerator Start()
    {
        while (true)
        {

            RefreshAndPush();
            yield return new WaitForSeconds(0.25f);
        }
        
    }

}
