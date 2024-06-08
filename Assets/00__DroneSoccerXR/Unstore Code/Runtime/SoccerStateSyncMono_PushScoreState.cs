using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoccerStateSyncMono_PushScoreState : MonoBehaviour
{
    public DroneSoccerMatchState m_timeInGame;
    public UnityEvent<DroneSoccerMatchState> m_onTimeChanged;
    public MSoccerMono_SyncBoard m_matchManager;


    void Start()
    {
        m_matchManager.m_onScoreChangeDetected.AddListener(() => {
            m_timeInGame.m_bluePoints = m_matchManager.m_pointScoreBlue;
            m_timeInGame.m_redPoints = m_matchManager.m_pointScoreRed;
            m_timeInGame.m_blueSets = m_matchManager.m_setScoreBlue;
            m_timeInGame.m_redSets = m_matchManager.m_setScoreRed;
            m_timeInGame.m_utcTickInSecondsWhenMatchStarted= m_matchManager.m_dateTimeOfMatchStart;
            m_onTimeChanged.Invoke(m_timeInGame);
        });
    }
}
