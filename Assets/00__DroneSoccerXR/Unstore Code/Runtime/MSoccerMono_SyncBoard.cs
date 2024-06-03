using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Random = UnityEngine.Random;

public class MSoccerMono_SyncBoard : NetworkBehaviour
{
    public FacadeMatchMono_GlobalSetter m_toAffectBoard;

    [SyncVar(hook =nameof(RefreshUIScore))]
    [SerializeField] public int m_pointScoreBlue;
    [SyncVar(hook = nameof(RefreshUIScore))]
    [SerializeField] public int m_pointScoreRed;
    [SyncVar(hook = nameof(RefreshUIScore))]
    [SerializeField] public int m_setScoreBlue;
    [SyncVar(hook = nameof(RefreshUIScore))]
    [SerializeField] public int m_setScoreRed;

    [SyncVar(hook = nameof(RefreshUI))]
    [SerializeField] public float m_setTimeLeftInSet;
    [SyncVar(hook = nameof(RefreshUI))]
    [SerializeField] public float m_setTimeSinceSetStarted;
    [SyncVar(hook = nameof(RefreshUI))]
    [SerializeField] public float m_setTimeSinceMatchStarted;
    [SyncVar(hook = nameof(RefreshUI))]
    [SerializeField] public long m_dateTime;

    [SyncVar(hook = nameof(RefreshUI))]
    [SerializeField] public long m_dateTimeOfMatchStart;


    [SyncVar(hook = nameof(RefreshUIState))]
    [SerializeField] public bool m_isMatchInPause;

    [SyncVar(hook = nameof(RefreshUIState))]
    [SerializeField] public bool m_isMatchStarted;

    [SyncVar(hook = nameof(RefreshUIState))]
    [SerializeField] public bool m_isMatchFinished;

    [SyncVar(hook = nameof(RefreshUIState))]
    [SerializeField] public bool m_redTeamIsWinner;
    [SyncVar(hook = nameof(RefreshUIState))]
    [SerializeField] public bool m_blueTeamIsWinner;



    public void SetAsMatchStartingAt(DateTime dateTime)
    {
        m_dateTimeOfMatchStart = dateTime.Ticks;
    }
    public void SetAsMatchStartingNowUTC()
    {
        SetAsMatchStartingAt(DateTime.UtcNow);
    }


    public UnityEvent m_onChangeDetected;
    public UnityEvent m_onScoreChangeDetected;
    public void AddPointScoreBlue() => m_pointScoreBlue++;
    public void AddPointScoreRed() => m_pointScoreRed++;
    public void AddSetScoreBlue() => m_setScoreBlue++;
    public void AddSetScoreRed() => m_setScoreRed++;
    public void SetPointScoreBlue(int pointValue) => m_pointScoreBlue = pointValue;
    public void SetPointScoreRed(int pointValue) => m_pointScoreRed = pointValue;
    public void SetSetScoreBlue(int pointValue) => m_setScoreBlue = pointValue;
    public void SetSetScoreRed(int pointValue) => m_setScoreRed = pointValue;
    public void SetTimeLeftInSeconds(float secondValue) => m_setTimeLeftInSet = secondValue;
    public void SetTimeSinceSetStartInSeconds(float secondValue) => m_setTimeSinceSetStarted = secondValue;
    public void SetTimeSinceMatchStartInSeconds(float secondValue) => m_setTimeSinceMatchStarted = secondValue;
    public void SetDateTime(System.DateTime dateTime) => m_dateTime = dateTime.Ticks;

    public void SetDateTimeNowUTC() => m_dateTime = DateTime.UtcNow.Ticks;
    public void SetDateTimeNow() => m_dateTime = DateTime.Now.Ticks;


    public void Reset()
    {
        m_pointScoreBlue = 0;
        m_pointScoreRed = 0;
        m_setScoreBlue = 0;
        m_setScoreRed = 0;
        m_setTimeLeftInSet = 0;
        m_setTimeSinceSetStarted = 0;
        m_setTimeSinceMatchStarted = 0;
        m_dateTime = System.DateTime.UtcNow.Ticks;
    }



    [ServerCallback]
    [ContextMenu("Push random Value")]
    public void PushRandomValue()
    {

        m_pointScoreBlue = Random.Range(0, 30);
        m_pointScoreRed = Random.Range(0, 30);
        m_setScoreBlue = Random.Range(0, 9);
        m_setScoreRed = Random.Range(0, 9);
        m_setTimeLeftInSet = Random.Range(200,300);
        m_setTimeSinceSetStarted = Random.Range(180,300);
        m_setTimeSinceMatchStarted = Random.Range(180, 300);
        m_dateTime = System.DateTime.UtcNow.Ticks;

    }



    public void RefreshUIScore(int oldVal, int newVal)
    {
        RefreshUI();
        m_onScoreChangeDetected.Invoke();
    }
    public void RefreshUI(int oldVal, int newVal)
    {
        RefreshUI();
    }

    public void RefreshUI(float oldVal, float newVal)
    {
        RefreshUI();
    }

    public void RefreshUI(long oldVal, long newVal)
    {
        RefreshUI();
    }

    public void RefreshUIState(bool oldVal, bool newVal)
    {
        RefreshUI();
    }
    public void RefreshUI() { 
    
        m_toAffectBoard.SetCurrentMatchScoreBlue(m_pointScoreBlue);
        m_toAffectBoard.SetCurrentMatchScoreRed(m_pointScoreRed);
        m_toAffectBoard.SetCurrentMatchSetBlue(m_setScoreBlue);
        m_toAffectBoard.SetCurrentMatchSetRed(m_setScoreRed);
        m_toAffectBoard.SetTimeLeftInSet(m_setTimeLeftInSet);
        m_toAffectBoard.SetTimeSinceSetStarted(m_setTimeSinceSetStarted);
        m_toAffectBoard.SetTimeSinceMatchStarted(m_setTimeSinceMatchStarted);
        m_toAffectBoard.SetTimeNow( new System.DateTime(m_dateTime));
        m_onChangeDetected.Invoke();
    }

    
}
