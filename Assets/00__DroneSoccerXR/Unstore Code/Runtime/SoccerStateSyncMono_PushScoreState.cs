using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoccerStateSyncMono_PushScoreState : MonoBehaviour, MSoccerMono_RefreshablePush
{
    public S_DroneSoccerMatchState m_timeInGame;
    public UnityEvent<S_DroneSoccerMatchState> m_onTimeChanged;
    public MSoccerMono_SyncBoard m_matchManager;

    public void RefreshAndPush()
    {
        m_timeInGame.m_bluePoints = m_matchManager.m_pointScoreBlue;
        m_timeInGame.m_redPoints = m_matchManager.m_pointScoreRed;
        m_timeInGame.m_blueSets = m_matchManager.m_setScoreBlue;
        m_timeInGame.m_redSets = m_matchManager.m_setScoreRed;
        m_timeInGame.m_utcTickInSecondsWhenMatchStarted = m_matchManager.m_dateTimeOfMatchStart;
        m_onTimeChanged.Invoke(m_timeInGame);
    }

    void Start()
    {
        m_matchManager.m_onScoreChangeDetected.AddListener(() => {
<<<<<<< HEAD
            m_timeInGame.m_bluePoints =(uint) m_matchManager.m_pointScoreBlue;
            m_timeInGame.m_redPoints = (uint)m_matchManager.m_pointScoreRed;
            m_timeInGame.m_blueSets = (uint)m_matchManager.m_setScoreBlue;
            m_timeInGame.m_redSets = (uint)m_matchManager.m_setScoreRed;
            m_timeInGame.m_utcTickInSecondsWhenMatchStarted= (ulong)m_matchManager.m_dateTimeOfMatchStart;
            m_onTimeChanged.Invoke(m_timeInGame);
=======
            RefreshAndPush();
>>>>>>> 7a6ba150380d69a1acddf3158bbb2d2228d86f1b
        });
    }
}
