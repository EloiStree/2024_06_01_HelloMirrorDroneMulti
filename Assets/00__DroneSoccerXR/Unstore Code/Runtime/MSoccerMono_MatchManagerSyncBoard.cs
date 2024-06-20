using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEditor;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.PlayerLoop;

public class MSoccerMono_MatchManagerSyncBoard : MonoBehaviour
{
    public MSoccerMono_SyncBoard m_syncBoardMulti;

    public bool m_autoRestartDirectly;
    [Header("Config")]
    public float m_timeOfSetInSeconds = 180;
    public float m_resetAllDronePosionTimeInSeconds = 2;
    public float m_antispamCountdownDelayToUse = 3;
    public bool m_useTeleportOnGoal = true;

    [Header("Event")]
    public UnityEvent<bool> m_droneAuthorizedToFly;
    public UnityEvent m_onRequestResetAllDronePosition;

    [Header("Debug")]
    public bool m_isMatchInPause;
    public bool m_isMatchFinished;
    public float m_timeLeftInSet;
    public float m_timeSinceSetStarted;
    public float m_timeSinceMatchStarted;
    public bool m_droneAuthorizedToFlyBool;
    public int m_pointScoreBlue;
    public int m_pointScoreRed;
    public int m_setScoreBlue;
    public int m_setScoreRed;


    public void SetMatchInPause(bool isInPause=true) {
        if (IsMatchFinished())
            return;
        m_isMatchInPause = isInPause;
        m_syncBoardMulti.m_isMatchInPause = isInPause;
    }


    private void ActionAtEveryNewSeconds()
    {
        if (IsMatchFinished())
            return;
        m_syncBoardMulti.SetTimeLeftInSeconds(m_timeLeftInSet);
        m_syncBoardMulti.SetTimeSinceSetStartInSeconds(m_timeSinceSetStarted);
        m_syncBoardMulti.SetTimeSinceMatchStartInSeconds(m_timeSinceMatchStarted);
        m_syncBoardMulti.SetDateTimeNowUTC(); 
    }


    private void Awake()
    {
        StartNewMatch();
    }

    [ContextMenu("Restart Match")]
    public void StartNewMatch()
    {

        SetMatchAsFinished(false);
        SetMatchInPause(true);

        m_pointScoreBlue = 0;
        m_pointScoreRed = 0;
        m_setScoreBlue = 0;
        m_setScoreRed = 0;
        m_syncBoardMulti.SetPointScoreBlue(m_pointScoreBlue);
        m_syncBoardMulti.SetPointScoreRed(m_pointScoreRed);
        m_syncBoardMulti.SetSetScoreBlue(m_setScoreBlue);
        m_syncBoardMulti.SetSetScoreRed(m_setScoreRed);
        m_syncBoardMulti.SetDateTimeNowUTC();
        m_syncBoardMulti.m_redTeamIsWinner = false;
        m_syncBoardMulti.m_blueTeamIsWinner = false;
        m_syncBoardMulti.m_isMatchFinished = false;
        m_syncBoardMulti.m_isMatchInPause = false;
        m_syncBoardMulti.m_isMatchStarted = true;

        StartNewSet();
        m_timeSinceMatchStarted = 0;
        m_syncBoardMulti.SetTimeLeftInSeconds(m_timeLeftInSet);
        m_syncBoardMulti.SetTimeSinceSetStartInSeconds(m_timeSinceSetStarted);
        m_syncBoardMulti.SetTimeSinceMatchStartInSeconds(m_timeSinceMatchStarted);
        SetMatchInPause(false);
        m_onRequestResetAllDronePosition.Invoke();
    }

    public void SetMatchAsFinished(bool isMatchFinished) {
        m_isMatchFinished = isMatchFinished;
        m_syncBoardMulti.m_isMatchFinished = isMatchFinished;

        if (m_autoRestartDirectly && isMatchFinished)
            StartNewMatch();
    }

    public bool IsMatchFinished() {
        return m_isMatchFinished;
    }


    [ContextMenu("Push current score value to Sync Multi")]
    public void PushInspectorScoreValue()
    {

        m_syncBoardMulti.SetPointScoreBlue(m_pointScoreBlue);
        m_syncBoardMulti.SetPointScoreRed(m_pointScoreRed);
        m_syncBoardMulti.SetSetScoreBlue(m_setScoreBlue);
        m_syncBoardMulti.SetSetScoreRed(m_setScoreRed); 
        m_syncBoardMulti.SetDateTimeNowUTC();
        m_syncBoardMulti.SetTimeLeftInSeconds(m_timeLeftInSet);
        m_syncBoardMulti.SetTimeSinceSetStartInSeconds(m_timeSinceSetStarted);
        m_syncBoardMulti.SetTimeSinceMatchStartInSeconds(m_timeSinceMatchStarted);
    }








    public void StartNewSet()
    {
        if (IsMatchFinished())
            return;
        m_timeSinceSetStarted = 0;
        ResetTimeLeftOfCurrentSet();
    }
    private void ResetTimeLeftOfCurrentSet()
    {
        m_timeLeftInSet = m_timeOfSetInSeconds;
    }

    public int m_timestampSeconds = 0;
    void Update()
    {
        m_antispamCountdownDelayPoint -= Time.deltaTime;
        m_antispamCountdownDelaySet-= Time.deltaTime;
        if (m_isMatchInPause)
            return;

        int timestampSeconds = (int)(DateTime.UtcNow.Ticks / TimeSpan.TicksPerSecond);
        if (timestampSeconds != m_timestampSeconds)
        {
            m_timestampSeconds = timestampSeconds;
            ActionAtEveryNewSeconds();
        }

       
        float deltaTime = Time.deltaTime;
        m_timeSinceSetStarted+=deltaTime;
        m_timeSinceMatchStarted+=deltaTime;
        m_timeLeftInSet = Math.Clamp( m_timeOfSetInSeconds - m_timeSinceSetStarted, 0, m_timeOfSetInSeconds);

    }



    public void AddPointToBlue()
    {
        if (IsMatchFinished())
            return;
        if (QuitIfNotOnServer())
            return;
        if (AntiSpamOnPoint())
            return;
        SetAntiSpamOnForSmallDelayPoint();
        m_pointScoreBlue++;
        m_syncBoardMulti.SetPointScoreBlue(m_pointScoreBlue);
        LockGoalAndTeleportPlayerAfterDelay();
    }


    public void AddPointToRed()
    {
        if (IsMatchFinished())
            return;
        if (QuitIfNotOnServer())
            return;
        if (AntiSpamOnPoint())
            return;
        SetAntiSpamOnForSmallDelayPoint();
        m_pointScoreRed++;
        m_syncBoardMulti.SetPointScoreRed(m_pointScoreRed);
        LockGoalAndTeleportPlayerAfterDelay();
    }
    public bool m_isOnServer;
    private bool QuitIfNotOnServer()
    {
        m_isOnServer = MSoccerMono_IsOnServerSingleton.IsOnServer();
        return !m_isOnServer;
    }
    public void SetBlueAsWinnerOfSet()
    {
        if (IsMatchFinished())
            return;
        if (QuitIfNotOnServer())
            return;
        if (AntiSpamOnSet())
            return;
        SetAntiSpamOnForSmallDelaySet();
        AddSetScoreToBlue(); 
        LockGoalAndTeleportPlayerAfterDelay();
        StartNewSet();
    }


    public void SetRedAsWinnerOfSet()
    {
        if (IsMatchFinished())
            return;
        if (QuitIfNotOnServer())
            return;
        if (AntiSpamOnSet())
            return;
        SetAntiSpamOnForSmallDelaySet();
        AddSetScoreToRed();
        LockGoalAndTeleportPlayerAfterDelay();
        StartNewSet();
    }


    public void SetBlueAsWinnerOfMatch()
    {
        if (IsMatchFinished())
            return;
        if (m_syncBoardMulti.m_redTeamIsWinner || m_syncBoardMulti.m_blueTeamIsWinner)
            return;
        if (QuitIfNotOnServer())
            return; 
        LockGoalAndTeleportPlayerAfterDelay();
        m_syncBoardMulti.m_redTeamIsWinner = false;
        m_syncBoardMulti.m_blueTeamIsWinner = true;
        m_syncBoardMulti.m_isMatchInPause = true;
        m_syncBoardMulti.m_isMatchFinished = true;
        SetMatchInPause();
        UnityEngine.Debug.Log("Blue WIN");
        SetMatchAsFinished(true);

    }
    public void SetRedAsWinnerOfMatch()
    {
        if (IsMatchFinished())
            return;
        if (m_syncBoardMulti.m_redTeamIsWinner || m_syncBoardMulti.m_blueTeamIsWinner)
            return;
        if (QuitIfNotOnServer())
            return;
     
        LockGoalAndTeleportPlayerAfterDelay();
        m_syncBoardMulti.m_redTeamIsWinner = true;
        m_syncBoardMulti.m_blueTeamIsWinner = false;
        m_syncBoardMulti.m_isMatchInPause = true;
        m_syncBoardMulti.m_isMatchFinished = true;
        SetMatchInPause();
        UnityEngine.Debug.Log("Red WIN");
        SetMatchAsFinished(true);
    }


    public void EndMatchAsItIs() {

        if (IsMatchFinished())
            return;
        if (QuitIfNotOnServer())
            return;
        m_syncBoardMulti.m_isMatchInPause = true;
        m_syncBoardMulti.m_isMatchFinished = true;

        SetMatchInPause();
        SetMatchAsFinished(true);
    }



    private void ResetPlayerAtStartPosition()
    {
        m_onRequestResetAllDronePosition.Invoke();
    }




    public float m_antispamCountdownDelayPoint = 3;
    public float m_antispamCountdownDelaySet = 3;

    private void AddSetScoreToBlue()
    {
        if (QuitIfNotOnServer())
            return;
        m_setScoreBlue++;
        m_syncBoardMulti.SetSetScoreBlue(m_setScoreBlue);
        m_pointScoreBlue = 0;
        m_pointScoreRed = 0;
        m_syncBoardMulti.SetPointScoreBlue(m_pointScoreBlue);
        m_syncBoardMulti.SetPointScoreRed(m_pointScoreRed);
    }

    private bool AntiSpamOnPoint()
    {
        return m_antispamCountdownDelayPoint > 0f;
    }
    private bool AntiSpamOnSet()
    {
        return m_antispamCountdownDelaySet > 0f;
    }

    private void SetAntiSpamOnForSmallDelayPoint()
    {
        m_antispamCountdownDelayPoint = m_antispamCountdownDelayToUse;
    }
    private void SetAntiSpamOnForSmallDelaySet()
    {
        m_antispamCountdownDelaySet = m_antispamCountdownDelayToUse;
    }

    private void AddSetScoreToRed()
    {
        if (QuitIfNotOnServer())
            return; 
        m_setScoreRed++;
        m_syncBoardMulti.SetSetScoreRed(m_setScoreRed);
        m_pointScoreBlue = 0;
        m_pointScoreRed = 0;
        m_syncBoardMulti.SetPointScoreBlue(m_pointScoreBlue);
        m_syncBoardMulti.SetPointScoreRed(m_pointScoreRed);
    }

    private void LockGoalAndTeleportPlayerAfterDelay()
    {
        if (QuitIfNotOnServer())
            return;
        if(m_useTeleportOnGoal) 
            Invoke("ResetPlayerAtStartPosition", m_resetAllDronePosionTimeInSeconds);
    }
}
