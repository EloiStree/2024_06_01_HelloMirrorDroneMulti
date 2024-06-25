using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.Events;
using System.Security.Cryptography;
using Org.BouncyCastle.Bcpg.OpenPgp;
public class MSoccerMono_GameCoreStateExportable : NetworkBehaviour
{

    [SyncVar(hook = nameof(ChangedHappened))]
    public S_DroneSoccerBallGoals m_droneSoccerBallGoals;


    [SyncVar(hook = nameof(ChangedHappened))]
    public S_DroneSoccerBallPosition m_droneSoccerBall;

    [SyncVar(hook = nameof(ChangedHappened))]
    public S_DroneSoccerMatchState m_gamePointsState;

    [SyncVar(hook = nameof(ChangedHappened))]
    public S_DroneSoccerTimeValue m_gameTimeValue;

    [SyncVar(hook = nameof(ChangedHappened))]
    public S_DroneSoccerMatchStaticInformation m_gameArenaInformation;

    [SyncVar(hook = nameof(ChangedHappened))]
    public S_DroneSoccerPositions m_gamePositions;


    [SyncVar(hook = nameof(ChangedHappened))]
    public S_DroneSoccerPublicXmlRsaKey1024Claim m_publicRsaClaim;

    [SyncVar(hook =nameof(ChangedHappened))]
    public S_DroneSoccerIndexIntegerClaim m_indexIntegerClaim;

    private void  ChangedHappened(S_DroneSoccerIndexIntegerClaim p, S_DroneSoccerIndexIntegerClaim n)

    {
        m_onChanged.m_onIndexIntegerClaim.Invoke(n);
    }
    private void ChangedHappened(S_DroneSoccerPublicXmlRsaKey1024Claim p, S_DroneSoccerPublicXmlRsaKey1024Claim n)
    {
        m_onChanged.m_onPublicRsaClaim.Invoke(n);
    }
    private void ChangedHappened(S_DroneSoccerPositions p, S_DroneSoccerPositions n)
    {
        m_onChanged.m_onGamePositions.Invoke(n);
    }
    private void ChangedHappened(S_DroneSoccerMatchStaticInformation p, S_DroneSoccerMatchStaticInformation n)
    {
        m_onChanged.m_onGameArenaInformation.Invoke(n);
    }
    private void ChangedHappened(S_DroneSoccerTimeValue p, S_DroneSoccerTimeValue n)
    {
        m_onChanged.m_onGameTimeValue.Invoke(n);
    }
    private void ChangedHappened(S_DroneSoccerMatchState p, S_DroneSoccerMatchState n)
    {
        m_onChanged.m_onGamePointsState.Invoke(n);
    }
    private void ChangedHappened(S_DroneSoccerBallPosition p, S_DroneSoccerBallPosition n)
    {
        m_onChanged.m_onSoccerBallState.Invoke(n);
    }

    private void ChangedHappened(S_DroneSoccerBallGoals p, S_DroneSoccerBallGoals n)
    {
        m_onChanged.m_onSoccerBallGoals.Invoke(n);
    }


    public void InvokeRefreshOnServerAndClients() {

        InvokeAllUnityEventForRefreshWithCurrentValue();
        if(MSoccerMono_IsOnServerSingleton.IsOnServer())
            RpcPushAllToRefresh();
    }

    [Mirror.ClientRpc]
    public void RpcPushAllToRefresh()
    {
        InvokeAllUnityEventForRefreshWithCurrentValue();

    }

    [ContextMenu("Invoke All Unity Event For Refresh With Current Value")]
    private void InvokeAllUnityEventForRefreshWithCurrentValue()
    {
        m_onChanged.m_onGamePointsState.Invoke(m_gamePointsState);
        m_onChanged.m_onGameTimeValue.Invoke(m_gameTimeValue);
        m_onChanged.m_onGameArenaInformation.Invoke(m_gameArenaInformation);
        m_onChanged.m_onGamePositions.Invoke(m_gamePositions);
        m_onChanged.m_onPublicRsaClaim.Invoke(m_publicRsaClaim);
        m_onChanged.m_onIndexIntegerClaim.Invoke(m_indexIntegerClaim);
        m_onChanged.m_onSoccerBallState.Invoke(m_droneSoccerBall);
        m_onChanged.m_onSoccerBallGoals.Invoke(m_droneSoccerBallGoals);
    }

    public Events m_onChanged;
    [System.Serializable]
    public class Events
    {

        public UnityEvent<S_DroneSoccerMatchState> m_onGamePointsState;
        public UnityEvent<S_DroneSoccerTimeValue> m_onGameTimeValue;
        public UnityEvent<S_DroneSoccerMatchStaticInformation> m_onGameArenaInformation;
        public UnityEvent<S_DroneSoccerPositions> m_onGamePositions;
        public UnityEvent<S_DroneSoccerPublicXmlRsaKey1024Claim> m_onPublicRsaClaim;
        public UnityEvent<S_DroneSoccerIndexIntegerClaim> m_onIndexIntegerClaim;
        public UnityEvent<S_DroneSoccerBallPosition> m_onSoccerBallState;
        public UnityEvent<S_DroneSoccerBallGoals> m_onSoccerBallGoals;

    }





    [ContextMenu("Full Fresh call no Rsa")]
    public void FullRefreshNotRsa() {

        CPS.CPS_DroneSoccerMatchState.GetCopy(m_gamePointsState, out m_gamePointsState);
        CPS.CPS_DroneSoccerMatchStaticInformation.GetCopy(m_gameArenaInformation, out m_gameArenaInformation);
        CPS.CPS_DroneSoccerPositions.GetCopy(m_gamePositions, out m_gamePositions);
        CPS.CPS_DroneSoccerBallPosition.GetCopy(m_droneSoccerBall, out m_droneSoccerBall);
        CPS.CPS_DroneSoccerBallGoals.GetCopy(m_droneSoccerBallGoals, out m_droneSoccerBallGoals);
        CPS.CPS_DroneSoccerIndexIntegerClaim.GetCopy(m_indexIntegerClaim, out m_indexIntegerClaim);
        CPS.CPS_DroneSoccerPublicXmlRsaKey1024Claim.GetCopy(m_publicRsaClaim, out m_publicRsaClaim);
        CPS.CPS_DroneSoccerTimeValue.GetCopy(m_gameTimeValue, out m_gameTimeValue);
       


    }


    [ContextMenu("Randomize Info")]

    public void RandomizedAndRefresh()
    {


        R.Randomized(ref m_gamePointsState);
        R.Randomized(ref m_gameTimeValue);
        R.Randomized(ref m_gameArenaInformation);
        R.Randomized(ref m_gamePositions);
        R.Randomized(ref m_indexIntegerClaim);

        FullRefreshNotRsa();
    }

    [ContextMenu("Randomize RSA")]

    public void RandomizedRsaRefresh()
    {

        R.Randomized(ref m_publicRsaClaim);
        CPS.CPS_DroneSoccerPublicXmlRsaKey1024Claim.GetCopy(m_publicRsaClaim, out m_publicRsaClaim);
        FullRefreshNotRsa();

    }



    public class R
    {
        internal static void Randomized(ref S_DroneSoccerTimeValue value)
        {
            value.m_secondsSinceSetStarted= UnityEngine.Random.Range(0, 300);
            value.m_secondsSinceMatchStarted= UnityEngine.Random.Range(0, 900);
            value.m_timeOfServerDateTimeUtcNowTicks=(ulong) DateTime.UtcNow.Ticks;
        }

        internal static void Randomized(ref S_DroneSoccerMatchStaticInformation value)
        {
            value.m_maxTimingOfSetInSeconds= UnityEngine.Random.Range(0, 300);
            value.m_maxTimingOfMatchInSeconds= UnityEngine.Random.Range(0, 900);
            value.m_numberOfSetsToWinMatch= UnityEngine.Random.Range(0, 2);
            value.m_numberOfPointsToForceWinSet= UnityEngine.Random.Range(0, 99);
            value.m_arenaWidthMeter= UnityEngine.Random.Range(0, 7);
            value.m_arenaHeightMeter= UnityEngine.Random.Range(0, 6);
            value.m_arenaDepthMeter= UnityEngine.Random.Range(0, 14);
            value.m_goalDistanceOfCenterMeter= UnityEngine.Random.Range(0, 4);
            value.m_goalCenterHeightMeter= UnityEngine.Random.Range(0, 3);
            value.m_goalInnerRadiusMeter= UnityEngine.Random.Range(0, 0.6f);
            value.m_goalOuterRadiusMeter= UnityEngine.Random.Range(0, 0.7f);
            value.m_goalDepthMeter= UnityEngine.Random.Range(0, 0.2f);
            value.m_droneSphereRadiusMeter= UnityEngine.Random.Range(0, 0.4f);
        }

        internal static void Randomized(ref S_DroneSoccerMatchState value)
        {
            value.m_bluePoints=(uint) UnityEngine.Random.Range(0, 100);
            value.m_redPoints= (uint)UnityEngine.Random.Range(0, 100);
            value.m_blueSets= (uint)UnityEngine.Random.Range(0, 100);
            value.m_redSets= (uint)UnityEngine.Random.Range(0, 100);
            value.m_utcTickInSecondsWhenMatchStarted=(ulong) DateTime.UtcNow.Ticks;
            value.m_utcTickInSecondsWhenMatchFinished= (ulong)DateTime.UtcNow.Ticks;
        }

        internal static void Randomized(ref S_DroneSoccerPositions value)
        {
            
        }

        internal static void Randomized(ref S_DroneSoccerPublicXmlRsaKey1024Claim value)
        {
            value.m_redDrone0Stricker= GetRSA();
            value.m_redDrone1= GetRSA();
            value.m_redDrone2= GetRSA();
            value.m_redDrone3= GetRSA();
            value.m_redDrone4= GetRSA();
            value.m_redDrone5= GetRSA();
            value.m_blueDrone0Stricker= GetRSA();
            value.m_blueDrone1= GetRSA();
            value.m_blueDrone2= GetRSA();
            value.m_blueDrone3= GetRSA();
            value.m_blueDrone4= GetRSA();
            value.m_blueDrone5= GetRSA();

        }
        public static string GetRSA() { 
            return RSA.Create().ToXmlString(false);
        }

        internal static void Randomized(ref S_DroneSoccerIndexIntegerClaim value)
        {
            value.m_redDrone0Stricker= UnityEngine.Random.Range(0, 100);
            value.m_redDrone1= UnityEngine.Random.Range(0, 100);
            value.m_redDrone2= UnityEngine.Random.Range(0, 100);
            value.m_redDrone3= UnityEngine.Random.Range(0, 100);
            value.m_redDrone4= UnityEngine.Random.Range(0, 100);
            value.m_redDrone5= UnityEngine.Random.Range(0, 100);
            value.m_blueDrone0Stricker= UnityEngine.Random.Range(0, 100);
            value.m_blueDrone1= UnityEngine.Random.Range(0, 100);
            value.m_blueDrone2= UnityEngine.Random.Range(0, 100);
            value.m_blueDrone3= UnityEngine.Random.Range(0, 100);
            value.m_blueDrone4= UnityEngine.Random.Range(0, 100);
            value.m_blueDrone5= UnityEngine.Random.Range(0, 100);
        }
    }



    [ServerCallback]
    public void SetArenaInformationAsCopy( S_DroneSoccerMatchStaticInformation arenaInformation) {
        m_gameArenaInformation = arenaInformation;
    }
    [ServerCallback]
    public void SetTimeValueAsCopy( S_DroneSoccerTimeValue timeValue) {
        m_gameTimeValue = timeValue;
    }
    [ServerCallback]
    public void SetPointsStateAsCopy( S_DroneSoccerMatchState pointsState) {
        m_gamePointsState = pointsState;
    }
    [ServerCallback]
    public void SetPositionsAsCopy( S_DroneSoccerPositions positions) {
        m_gamePositions = positions;
    }
    [ServerCallback]
    public void SetPublicRsaKeyClaimAsCopy( S_DroneSoccerPublicXmlRsaKey1024Claim publicRsaKeyClaim) {
        m_publicRsaClaim = publicRsaKeyClaim;
    }
    [ServerCallback]
    public void SetIndexIntegerClaimAsCopy( S_DroneSoccerIndexIntegerClaim indexIntegerClaim) {
        m_indexIntegerClaim = indexIntegerClaim ;
    }
}
