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
        m_timeInGame.m_timeOfServerDateTimeUtcNowTicks = DateTime.UtcNow.Ticks;
        m_onTimeChanged.Invoke(m_timeInGame);
    }

    IEnumerator Start()
    {
        while (true)
        {
<<<<<<< HEAD
            m_timeInGame.m_secondsSinceMatchStarted = m_matchManager.m_setTimeSinceMatchStarted;
            m_timeInGame.m_secondsSinceSetStarted = m_matchManager.m_setTimeSinceSetStarted;
            m_timeInGame.m_timeOfServerDateTimeUtcNowTicks = (ulong)DateTime.UtcNow.Ticks;
            m_onTimeChanged.Invoke(m_timeInGame);
=======
            RefreshAndPush();
>>>>>>> 7a6ba150380d69a1acddf3158bbb2d2228d86f1b
            yield return new WaitForSeconds(0.25f);
        }
        
    }

}
