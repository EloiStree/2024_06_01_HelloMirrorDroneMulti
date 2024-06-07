using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.Events;
public class MSoccerMono_GameCoreStateExportable : NetworkBehaviour
{

    [SyncVar(hook = nameof(ChangedHappened))]
    public DroneSoccerMatchState m_gamePointsState;

    [SyncVar(hook = nameof(ChangedHappened))]
    public DroneSoccerTimeValue m_gameTimeValue;

    [SyncVar(hook = nameof(ChangedHappened))]
    public DroneSoccerMatchStaticInformation m_gameArenaInformation;

    [SyncVar(hook = nameof(ChangedHappened))]
    public DroneSoccerPositions m_gamePositions;


    [SyncVar(hook = nameof(ChangedHappened))]
    public DroneSoccerPublicRsaKeyClaim m_publicRsaClaim;


    [SyncVar(hook =nameof(ChangedHappened))]
    public DroneSoccerIndexIntegerClaim m_indexIntegerClaim;

    private void  ChangedHappened(DroneSoccerIndexIntegerClaim p, DroneSoccerIndexIntegerClaim n)
    {
        m_onChanged.m_onIndexIntegerClaim.Invoke(n);
    }
    private void ChangedHappened(DroneSoccerPublicRsaKeyClaim p, DroneSoccerPublicRsaKeyClaim n)
    {
        m_onChanged.m_onPublicRsaClaim.Invoke(n);
    }
    private void ChangedHappened(DroneSoccerPositions p, DroneSoccerPositions n)
    {
        m_onChanged.m_onGamePositions.Invoke(n);
    }
    private void ChangedHappened(DroneSoccerMatchStaticInformation p, DroneSoccerMatchStaticInformation n)
    {
        m_onChanged.m_onGameArenaInformation.Invoke(n);
    }
    private void ChangedHappened(DroneSoccerTimeValue p, DroneSoccerTimeValue n)
    {
        m_onChanged.m_onGameTimeValue.Invoke(n);
    }
    private void ChangedHappened(DroneSoccerMatchState p, DroneSoccerMatchState n)
    {
        m_onChanged.m_onGamePointsState.Invoke(n);
    }


    public Events m_onChanged;
    [System.Serializable]
    public class Events
    {
        public UnityEvent<DroneSoccerMatchState> m_onGamePointsState;
        public UnityEvent<DroneSoccerTimeValue> m_onGameTimeValue;
        public UnityEvent<DroneSoccerMatchStaticInformation> m_onGameArenaInformation;
        public UnityEvent<DroneSoccerPositions> m_onGamePositions;
        public UnityEvent<DroneSoccerPublicRsaKeyClaim> m_onPublicRsaClaim;
        public UnityEvent<DroneSoccerIndexIntegerClaim> m_onIndexIntegerClaim;
    }


    [ServerCallback]
    [ContextMenu("Full Fresh call")]
    public void FullRefresh() {
        m_gamePointsState = m_gamePointsState.GetCopy();
        m_gameTimeValue = m_gameTimeValue.GetCopy();
        m_gameArenaInformation = m_gameArenaInformation.GetCopy();
        m_gamePositions = m_gamePositions.GetCopy();
        m_publicRsaClaim = m_publicRsaClaim.GetCopy();
        m_indexIntegerClaim = m_indexIntegerClaim.GetCopy();
    }

    [ServerCallback]
    public void SetArenaInformationAsCopy( DroneSoccerMatchStaticInformation arenaInformation) {
        m_gameArenaInformation = arenaInformation.GetCopy();
    }
    [ServerCallback]
    public void SetTimeValueAsCopy( DroneSoccerTimeValue timeValue) {
        m_gameTimeValue = timeValue.GetCopy();
    }
    [ServerCallback]
    public void SetPointsStateAsCopy( DroneSoccerMatchState pointsState) {
        m_gamePointsState = pointsState.GetCopy();
    }
    [ServerCallback]
    public void SetPositionsAsCopy( DroneSoccerPositions positions) {
        m_gamePositions = positions.GetCopy();
    }
    [ServerCallback]
    public void SetPublicRsaKeyClaimAsCopy( DroneSoccerPublicRsaKeyClaim publicRsaKeyClaim) {
        m_publicRsaClaim = publicRsaKeyClaim.GetCopy(); ;
    }
    [ServerCallback]
    public void SetIndexIntegerClaimAsCopy( DroneSoccerIndexIntegerClaim indexIntegerClaim) {
        m_indexIntegerClaim = indexIntegerClaim.GetCopy(); ;
    }
}





//Refresh with event
[System.Serializable]
public struct DroneSoccerMatchState
{   
    public int m_bluePoints;
    public int m_redPoints;
    public int m_blueSets;
    public int m_redSets;
    public long m_utcTickInSecondsWhenMatchStarted;
    public long m_utcTickInSecondsWhenMatchFinished;

    public DroneSoccerMatchState GetCopy() { 
    return new DroneSoccerMatchState() {
        m_bluePoints = m_bluePoints,
        m_redPoints = m_redPoints,
        m_blueSets = m_blueSets,
        m_redSets = m_redSets,
        m_utcTickInSecondsWhenMatchStarted = m_utcTickInSecondsWhenMatchStarted,
        m_utcTickInSecondsWhenMatchFinished = m_utcTickInSecondsWhenMatchFinished
    };
    
    
    }
}


//Refresh every seconds
[System.Serializable]
public struct DroneSoccerTimeValue {
    public float m_secondsSinceMatchStarted;
    public float m_secondsSinceSetStarted;
    public long m_timeOfServerDateTimeUtcNowTicks;

    public DroneSoccerTimeValue GetCopy(){ 
    return new DroneSoccerTimeValue() {
        m_secondsSinceMatchStarted = m_secondsSinceMatchStarted,
        m_secondsSinceSetStarted = m_secondsSinceSetStarted,
        m_timeOfServerDateTimeUtcNowTicks = m_timeOfServerDateTimeUtcNowTicks
    };
    }
}

//Set Once at start of the match
[System.Serializable]
public struct DroneSoccerMatchStaticInformation {
    [Header("Match Info")]
    public float m_maxTimingOfSet;//300 seconds
    public float m_maxTimingOfMatch;//15 minutes
    public float m_numberOfSetsToWinMatch;//2 sets
    [Tooltip("If a team reach this points, the set is won")]
    public float m_numberOfPointsToForceWinSet;//99 points

    [Header("Dimension")]
    public float m_arenaWidthMeter;//around 7 meters
    public float m_arenaHeightMeter;//around 6 meters
    public float m_arenaDepthMeter;//around 14 meters
    public float m_goalDistanceOfCenterMeter;//4 meters+
    public float m_goalCenterHeightMeter;//3 meters
    public float m_goalInnerRadiusMeter;// 60cm+
    public float m_goalSizeRadiusMeter;///70cm+
    public float m_goalDepthMeter;//around 5-20 cm
    public float m_droneSphereRadiusMeter;//40cm 0.4 meter


    public DroneSoccerMatchStaticInformation GetCopy() {
        return new DroneSoccerMatchStaticInformation() {
            m_maxTimingOfSet = m_maxTimingOfSet,
            m_maxTimingOfMatch = m_maxTimingOfMatch,
            m_numberOfSetsToWinMatch = m_numberOfSetsToWinMatch,
            m_numberOfPointsToForceWinSet = m_numberOfPointsToForceWinSet,
            m_arenaWidthMeter = m_arenaWidthMeter,
            m_arenaHeightMeter = m_arenaHeightMeter,
            m_arenaDepthMeter = m_arenaDepthMeter,
            m_goalDistanceOfCenterMeter = m_goalDistanceOfCenterMeter,
            m_goalCenterHeightMeter = m_goalCenterHeightMeter,
            m_goalInnerRadiusMeter = m_goalInnerRadiusMeter,
            m_goalSizeRadiusMeter = m_goalSizeRadiusMeter,
            m_goalDepthMeter = m_goalDepthMeter,
            m_droneSphereRadiusMeter = m_droneSphereRadiusMeter
        };
    }
}
//12 time per second refresh
[System.Serializable]
public struct DroneSoccerPositions
{
    public long m_dateTimeUtcTick;
    public long m_framePushed;
    public DronePositionCompressed m_redDrone0Stricker;
    public DronePositionCompressed m_redDrone1;
    public DronePositionCompressed m_redDrone2;
    public DronePositionCompressed m_redDrone3;
    public DronePositionCompressed m_redDrone4;
    public DronePositionCompressed m_redDrone5;
    public DronePositionCompressed m_blueDrone0Stricker;
    public DronePositionCompressed m_blueDrone1;
    public DronePositionCompressed m_blueDrone2;
    public DronePositionCompressed m_blueDrone3;
    public DronePositionCompressed m_blueDrone4;
    public DronePositionCompressed m_blueDrone5;

    public DroneSoccerPositions GetCopy()
    {
        return new DroneSoccerPositions()
        {
            m_dateTimeUtcTick = m_dateTimeUtcTick,
            m_framePushed = m_framePushed,
            m_redDrone0Stricker = m_redDrone0Stricker.GetCopy(),
            m_redDrone1 = m_redDrone1.GetCopy(),
            m_redDrone2 = m_redDrone2.GetCopy(),
            m_redDrone3 = m_redDrone3.GetCopy(),
            m_redDrone4 = m_redDrone4.GetCopy(),
            m_redDrone5 = m_redDrone5.GetCopy(),
            m_blueDrone0Stricker = m_blueDrone0Stricker.GetCopy(),
            m_blueDrone1 = m_blueDrone1.GetCopy(),
            m_blueDrone2 = m_blueDrone2.GetCopy(),
            m_blueDrone3 = m_blueDrone3.GetCopy(),
            m_blueDrone4 = m_blueDrone4.GetCopy(),
            m_blueDrone5 = m_blueDrone5.GetCopy()
        };
    }
}


[System.Serializable]
public struct DronePositionCompressed
{
    public short m_localPositionX;
    public short m_localPositionY;
    public short m_localPositionZ;
    public byte m_eulerAngleX;
    public byte m_eulerAngleY;
    public byte m_eulerAngleZ;

    public void SetPosition(Vector3 localPosition)
    {
        m_localPositionX = (short)(Mathf.Clamp(localPosition.x, short.MinValue, short.MaxValue));
        m_localPositionY = (short)(Mathf.Clamp(localPosition.y, short.MinValue, short.MaxValue));
        m_localPositionZ = (short)(Mathf.Clamp(localPosition.z, short.MinValue, short.MaxValue));
    }
    public void SetRotation(Quaternion localRotation)
    {
        Vector3 euler = localRotation.eulerAngles;
        Convert360AngleTo255(euler.x, out m_eulerAngleX);
        Convert360AngleTo255(euler.y, out m_eulerAngleY);
        Convert360AngleTo255(euler.z, out m_eulerAngleZ);
    }

    private void Convert360AngleTo255(float angle, out byte angle255)
    {
        angle255 = (byte)((angle % 360) / 360f * 255f);
    }
    private void Convert255AngleTo360(float angle255, out byte angle360)
    {
        angle360 = (byte)(angle255 / 255f * 360f);
    }
    public Vector3 GetPosition()
    {
        return new Vector3(m_localPositionX, m_localPositionY, m_localPositionZ);
    }
    public Quaternion GetRotation()
    {
        Convert255AngleTo360(m_eulerAngleX, out byte x);
        Convert255AngleTo360(m_eulerAngleY, out byte y);
        Convert255AngleTo360(m_eulerAngleZ, out byte z);
        return Quaternion.Euler(x, y,z);
    }

    public DronePositionCompressed GetCopy()
    {
        return new DronePositionCompressed()
        {
            m_localPositionX = m_localPositionX,
            m_localPositionY = m_localPositionY,
            m_localPositionZ = m_localPositionZ,
            m_eulerAngleX = m_eulerAngleX,
            m_eulerAngleY = m_eulerAngleY,
            m_eulerAngleZ = m_eulerAngleZ
        };
    }
}



[System.Serializable]
public struct DroneSoccerPublicRsaKeyClaim
{
    public string m_redDrone0Stricker;
    public string m_redDrone1;
    public string m_redDrone2;
    public string m_redDrone3;
    public string m_redDrone4;
    public string m_redDrone5;
    public string m_blueDrone0Stricker;
    public string m_blueDrone1;
    public string m_blueDrone2;
    public string m_blueDrone3;
    public string m_blueDrone4;
    public string m_blueDrone5;

    public DroneSoccerPublicRsaKeyClaim GetCopy()
    {

        return new DroneSoccerPublicRsaKeyClaim()
        {
            m_redDrone0Stricker = m_redDrone0Stricker,
            m_redDrone1 = m_redDrone1,
            m_redDrone2 = m_redDrone2,
            m_redDrone3 = m_redDrone3,
            m_redDrone4 = m_redDrone4,
            m_redDrone5 = m_redDrone5,
            m_blueDrone0Stricker = m_blueDrone0Stricker,
            m_blueDrone1 = m_blueDrone1,
            m_blueDrone2 = m_blueDrone2,
            m_blueDrone3 = m_blueDrone3,
            m_blueDrone4 = m_blueDrone4,
            m_blueDrone5 = m_blueDrone5
        };
    }
}
[System.Serializable]
public struct DroneSoccerIndexIntegerClaim
{
    public int m_redDrone0Stricker;
    public int m_redDrone1;
    public int m_redDrone2;
    public int m_redDrone3;
    public int m_redDrone4;
    public int m_redDrone5;
    public int m_blueDrone0Stricker;
    public int m_blueDrone1;
    public int m_blueDrone2;
    public int m_blueDrone3;
    public int m_blueDrone4;
    public int m_blueDrone5;

    public DroneSoccerIndexIntegerClaim GetCopy()
    {
        return new DroneSoccerIndexIntegerClaim()
        {
            m_redDrone0Stricker = m_redDrone0Stricker,
            m_redDrone1 = m_redDrone1,
            m_redDrone2 = m_redDrone2,
            m_redDrone3 = m_redDrone3,
            m_redDrone4 = m_redDrone4,
            m_redDrone5 = m_redDrone5,
            m_blueDrone0Stricker = m_blueDrone0Stricker,
            m_blueDrone1 = m_blueDrone1,
            m_blueDrone2 = m_blueDrone2,
            m_blueDrone3 = m_blueDrone3,
            m_blueDrone4 = m_blueDrone4,
            m_blueDrone5 = m_blueDrone5
        };

    }
}