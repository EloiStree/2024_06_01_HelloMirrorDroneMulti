using Mirror;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_SyncBoard : NetworkBehaviour
{
    public FacadeMatchMono_GlobalSetter m_toAffectBoard;


    [SyncVar(hook =nameof(RefreshUI))]
    public int m_pointScoreBlue;
    [SyncVar(hook = nameof(RefreshUI))]
    public int m_pointScoreRed;
    [SyncVar(hook = nameof(RefreshUI))]
    public int m_setScoreBlue;
    [SyncVar(hook = nameof(RefreshUI))]
    public int m_setScoreRed;
    [SyncVar(hook = nameof(RefreshUI))]
    public float m_setTimeLeftInSet;
    [SyncVar(hook = nameof(RefreshUI))]
    public float m_setTimeSinceSetStarted;
    [SyncVar(hook = nameof(RefreshUI))]
    public float m_setTimeSinceMatchStarted;
    [SyncVar(hook = nameof(RefreshUI))]
    public long m_timeNowUTC;



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
        m_timeNowUTC = System.DateTime.UtcNow.Ticks;

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



    public void RefreshUI() { 
    
        m_toAffectBoard.SetCurrentMatchScoreBlue(m_pointScoreBlue);
        m_toAffectBoard.SetCurrentMatchScoreRed(m_pointScoreRed);
        m_toAffectBoard.SetCurrentMatchSetBlue(m_setScoreBlue);
        m_toAffectBoard.SetCurrentMatchSetRed(m_setScoreRed);
        m_toAffectBoard.SetTimeLeftInSet(m_setTimeLeftInSet);
        m_toAffectBoard.SetTimeSinceSetStarted(m_setTimeSinceSetStarted);
        m_toAffectBoard.SetTimeSinceMatchStarted(m_setTimeSinceMatchStarted);
        m_toAffectBoard.SetTimeNow( new System.DateTime(m_timeNowUTC));
    }

    
}
