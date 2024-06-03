using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_WinSetByPoint : MonoBehaviour
{
    public MSoccerMono_SyncBoard m_syncBoard;

    public int m_scoreToWinSet = 9;
    public int m_setToWinMatch = 2;

    public UnityEvent m_onBlueWinSetDetected;
    public UnityEvent m_onRedWinSetDetected;
    public UnityEvent m_onBlueWinMatchDetected;
    public UnityEvent m_onRedWinMatchDetected;


    private void OnEnable()
    {
        m_syncBoard.m_onScoreChangeDetected.AddListener(CheckWinSet);
        InvokeRepeating("CheckWinSet", 0, 0.5f);
    }
    private void OnDisable()
    {
        m_syncBoard.m_onScoreChangeDetected.RemoveListener(CheckWinSet);
    }
    private void Awake()
    {
    }

    private void Update()
    {
        if (m_antiSpamCountdownDelay > 0f)
        {
            m_antiSpamCountdownDelay -= Time.deltaTime;
            return;
        }
    }
    public float m_antiSpamCountdownDelay = 3;
    private void CheckWinSet()
    {
        if(m_antiSpamCountdownDelay>0f)
        {
            return;
        }

        if (m_syncBoard.m_pointScoreBlue >= m_scoreToWinSet)
        {
            m_onBlueWinSetDetected.Invoke();
            m_antiSpamCountdownDelay = 3;
        }
        else if (m_syncBoard.m_pointScoreRed >= m_scoreToWinSet)
        {
            m_onRedWinSetDetected.Invoke();
            m_antiSpamCountdownDelay = 3;
        }
        else if(m_syncBoard.m_setScoreBlue >= m_setToWinMatch)
        {
            m_onBlueWinMatchDetected.Invoke();
            m_antiSpamCountdownDelay = 3;
        }
        else if(m_syncBoard.m_setScoreRed >= m_setToWinMatch)
        {
            m_onRedWinMatchDetected.Invoke();
            m_antiSpamCountdownDelay = 3;
        }
    }
}
