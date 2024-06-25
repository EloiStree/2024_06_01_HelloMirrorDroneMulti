using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoccerStateSyncMono_PushScoreState : MonoBehaviour
{
    public S_DroneSoccerMatchState m_timeInGame;
    public UnityEvent<S_DroneSoccerMatchState> m_onTimeChanged;
    public MSoccerMono_SyncBoard m_matchManager;


    void Start()
    {
        m_matchManager.m_onScoreChangeDetected.AddListener(() => {
            m_timeInGame.m_bluePoints =(uint) m_matchManager.m_pointScoreBlue;
            m_timeInGame.m_redPoints = (uint)m_matchManager.m_pointScoreRed;
            m_timeInGame.m_blueSets = (uint)m_matchManager.m_setScoreBlue;
            m_timeInGame.m_redSets = (uint)m_matchManager.m_setScoreRed;
            m_timeInGame.m_utcTickInSecondsWhenMatchStarted= (ulong)m_matchManager.m_dateTimeOfMatchStart;
            m_onTimeChanged.Invoke(m_timeInGame);
        });
    }
}
