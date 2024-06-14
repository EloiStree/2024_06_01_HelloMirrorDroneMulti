using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.Events;
using System.Security.Cryptography;
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


    [ContextMenu("Full Fresh call no Rsa")]
    public void FullRefreshNotRsa() {
        m_gamePointsState       = m_gamePointsState.GetCopy();
        m_gameTimeValue         = m_gameTimeValue.GetCopy();
        m_gameArenaInformation  = m_gameArenaInformation.GetCopy();
        m_gamePositions         = m_gamePositions.GetCopy();
        m_indexIntegerClaim     = m_indexIntegerClaim.GetCopy();
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
        m_publicRsaClaim= m_publicRsaClaim.GetCopy();
        FullRefreshNotRsa();

    }



    public class R
    {
        internal static void Randomized(ref DroneSoccerTimeValue value)
        {
            value.m_secondsSinceSetStarted= UnityEngine.Random.Range(0, 300);
            value.m_secondsSinceMatchStarted= UnityEngine.Random.Range(0, 900);
            value.m_timeOfServerDateTimeUtcNowTicks= DateTime.UtcNow.Ticks;
        }

        internal static void Randomized(ref DroneSoccerMatchStaticInformation value)
        {
            value.m_maxTimingOfSet= UnityEngine.Random.Range(0, 300);
            value.m_maxTimingOfMatch= UnityEngine.Random.Range(0, 900);
            value.m_numberOfSetsToWinMatch= UnityEngine.Random.Range(0, 2);
            value.m_numberOfPointsToForceWinSet= UnityEngine.Random.Range(0, 99);
            value.m_arenaWidthMeter= UnityEngine.Random.Range(0, 7);
            value.m_arenaHeightMeter= UnityEngine.Random.Range(0, 6);
            value.m_arenaDepthMeter= UnityEngine.Random.Range(0, 14);
            value.m_goalDistanceOfCenterMeter= UnityEngine.Random.Range(0, 4);
            value.m_goalCenterHeightMeter= UnityEngine.Random.Range(0, 3);
            value.m_goalInnerRadiusMeter= UnityEngine.Random.Range(0, 0.6f);
            value.m_goalSizeRadiusMeter= UnityEngine.Random.Range(0, 0.7f);
            value.m_goalDepthMeter= UnityEngine.Random.Range(0, 0.2f);
            value.m_droneSphereRadiusMeter= UnityEngine.Random.Range(0, 0.4f);
        }

        internal static void Randomized(ref DroneSoccerMatchState value)
        {
            value.m_bluePoints= UnityEngine.Random.Range(0, 100);
            value.m_redPoints= UnityEngine.Random.Range(0, 100);
            value.m_blueSets= UnityEngine.Random.Range(0, 100);
            value.m_redSets= UnityEngine.Random.Range(0, 100);
            value.m_utcTickInSecondsWhenMatchStarted= DateTime.UtcNow.Ticks;
            value.m_utcTickInSecondsWhenMatchFinished= DateTime.UtcNow.Ticks;
        }

        internal static void Randomized(ref DroneSoccerPositions value)
        {
            
        }

        internal static void Randomized(ref DroneSoccerPublicRsaKeyClaim value)
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

        internal static void Randomized(ref DroneSoccerIndexIntegerClaim value)
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
        m_localPositionX = (short)(Mathf.Clamp(localPosition.x * 1000f, short.MinValue, short.MaxValue));
        m_localPositionY = (short)(Mathf.Clamp(localPosition.y * 1000f, short.MinValue, short.MaxValue));
        m_localPositionZ = (short)(Mathf.Clamp(localPosition.z * 1000f, short.MinValue, short.MaxValue));
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
        return new Vector3(m_localPositionX/1000f, m_localPositionY / 1000f, m_localPositionZ / 1000f);
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
            m_redDrone0Stricker = m_redDrone0Stricker.ToString(),
            m_redDrone1 = m_redDrone1.ToString(),
            m_redDrone2 = m_redDrone2.ToString(),
            m_redDrone3 = m_redDrone3.ToString(),
            m_redDrone4 = m_redDrone4.ToString(),
            m_redDrone5 = m_redDrone5.ToString(),
            m_blueDrone0Stricker = m_blueDrone0Stricker.ToString(),
            m_blueDrone1 = m_blueDrone1.ToString(),
            m_blueDrone2 = m_blueDrone2.ToString(),
            m_blueDrone3 = m_blueDrone3.ToString(),
            m_blueDrone4 = m_blueDrone4.ToString(),
            m_blueDrone5 = m_blueDrone5.ToString()
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
            m_redDrone0Stricker =(int) m_redDrone0Stricker,
            m_redDrone1 =(int) m_redDrone1,
            m_redDrone2 =(int) m_redDrone2,
            m_redDrone3 =(int) m_redDrone3,
            m_redDrone4 =(int) m_redDrone4,
            m_redDrone5 = (int)m_redDrone5,
            m_blueDrone0Stricker = (int)m_blueDrone0Stricker,
            m_blueDrone1 =(int) m_blueDrone1,
            m_blueDrone2 =(int) m_blueDrone2,
            m_blueDrone3 =(int) m_blueDrone3,
            m_blueDrone4 =(int) m_blueDrone4,
            m_blueDrone5 = (int)m_blueDrone5
        };

    }
}