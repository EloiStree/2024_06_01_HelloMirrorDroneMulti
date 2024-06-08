using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoccerStateSyncMono_PushTiming : MonoBehaviour
{

    public DroneSoccerTimeValue m_timeInGame;
    public UnityEvent<DroneSoccerTimeValue> m_onTimeChanged;
    public MSoccerMono_SyncBoard m_matchManager;

    IEnumerator Start()
    {
        while (true)
        {
            m_timeInGame.m_secondsSinceMatchStarted = m_matchManager.m_setTimeSinceMatchStarted;
            m_timeInGame.m_secondsSinceSetStarted = m_matchManager.m_setTimeSinceSetStarted;
            m_timeInGame.m_timeOfServerDateTimeUtcNowTicks = DateTime.UtcNow.Ticks;
            m_onTimeChanged.Invoke(m_timeInGame);
            yield return new WaitForSeconds(0.25f);
        }
        
    }

}
