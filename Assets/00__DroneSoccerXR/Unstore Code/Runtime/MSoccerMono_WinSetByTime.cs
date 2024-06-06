using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_WinSetByTime : MonoBehaviour
{
    public MSoccerMono_SyncBoard m_syncBoard;

        
    public UnityEvent m_onBlueWinSetDetected;
    public UnityEvent m_onRedWinSetDetected;
    public float m_timeToWinSetInSeconds = 300;

    private void OnEnable()
    {
        m_syncBoard.m_onScoreChangeDetected.AddListener(CheckWinSet);
    }
    private void OnDisable()
    {
        m_syncBoard.m_onScoreChangeDetected.RemoveListener(CheckWinSet);
    }
    private void Update()
    {
        if(m_antiSpamCountdownDelay>0)
        {
            m_antiSpamCountdownDelay -= Time.deltaTime;
            return;
        }
        if(m_syncBoard.m_setTimeLeftInSet<=0)
        {
            CheckWinSet();
        }
    }
    public float m_antiSpamCountdownDelay = 5;
    private void CheckWinSet()
    {
        if (m_syncBoard.m_setTimeLeftInSet <= 0) { 
            if(m_syncBoard.m_pointScoreBlue > m_syncBoard.m_pointScoreRed)
            {
                m_onBlueWinSetDetected.Invoke();
                m_antiSpamCountdownDelay = 5f;
            }
            else if (m_syncBoard.m_pointScoreRed > m_syncBoard.m_pointScoreBlue)
            {
                m_onRedWinSetDetected.Invoke(); 
                m_antiSpamCountdownDelay = 5f;

            }
        }
    }
}
