using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_MaxTimeForMatch : MonoBehaviour
{
    public MSoccerMono_SyncBoard m_syncBoard;
    public float m_maxTimeForMatchInSeconds = 60 * 15;
    public bool m_useDrawAllowed = true;

    public UnityEvent m_onMatchMustEnd;
    public UnityEvent m_onRedTeamWin;
    public UnityEvent m_onBlueTeamWin;
    public UnityEvent m_onMatchIsDraw;
    private void Start()
    {
        InvokeRepeating("CheckMaxTimeForMatch", 0, 1);
    }


    public void CheckMaxTimeForMatch() { 
    
        if(m_syncBoard.m_setTimeSinceMatchStarted> m_maxTimeForMatchInSeconds)
        {
            if(m_syncBoard.m_setScoreBlue > m_syncBoard.m_setScoreRed)
            {
                m_onBlueTeamWin.Invoke();
            }
            else if (m_syncBoard.m_setScoreRed > m_syncBoard.m_setScoreBlue)
            {
                m_onRedTeamWin.Invoke();
            }
            else if(m_syncBoard.m_setScoreRed == m_syncBoard.m_setScoreBlue &&
                m_syncBoard.m_pointScoreBlue > m_syncBoard.m_pointScoreRed)
            {
                m_onBlueTeamWin.Invoke();
            }
            else if (m_syncBoard.m_setScoreRed == m_syncBoard.m_setScoreBlue &&
                m_syncBoard.m_pointScoreRed > m_syncBoard.m_pointScoreBlue)
            {
                m_onRedTeamWin.Invoke();
            }
            else if(m_useDrawAllowed)
            {
                m_onMatchIsDraw.Invoke();
            }
            else
            {
                int random = Random.Range(0, 2);
                if(random == 0)
                {
                    m_onBlueTeamWin.Invoke();
                }
                else
                {
                    m_onRedTeamWin.Invoke();
                }
            }
            m_onMatchMustEnd.Invoke();
        }
    }
}
