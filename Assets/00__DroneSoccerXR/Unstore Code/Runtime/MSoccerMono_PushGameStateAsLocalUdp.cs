using System;
using System.Collections;
using System.Collections.Generic;
using System.Text;
using UnityEngine;
using UnityEngine.Events;


public class MSoccerMono_PushGameStateAsLocalUdp : MonoBehaviour
{
    public UnityEvent<byte[]> m_taggedBytesToPush;
    public UnityEvent<string> m_textToPush;


    public void SetArenaInformationAsByte(DroneSoccerMatchStaticInformation arenaInformation)
    {
       
        byte[] bytes = new byte[1 + 4 *13];
        bytes[0] = 10;
        BitConverter.GetBytes(arenaInformation.m_maxTimingOfSet).CopyTo(bytes, 1);
        BitConverter.GetBytes(arenaInformation.m_maxTimingOfMatch).CopyTo(bytes, 5);
        BitConverter.GetBytes(arenaInformation.m_numberOfSetsToWinMatch).CopyTo(bytes, 9);
        BitConverter.GetBytes(arenaInformation.m_numberOfPointsToForceWinSet).CopyTo(bytes, 13);
        BitConverter.GetBytes(arenaInformation.m_arenaWidthMeter).CopyTo(bytes, 17);
        BitConverter.GetBytes(arenaInformation.m_arenaHeightMeter).CopyTo(bytes, 21);
        BitConverter.GetBytes(arenaInformation.m_arenaDepthMeter).CopyTo(bytes, 25);
        BitConverter.GetBytes(arenaInformation.m_goalDistanceOfCenterMeter).CopyTo(bytes, 29);
        BitConverter.GetBytes(arenaInformation.m_goalCenterHeightMeter).CopyTo(bytes, 33);
        BitConverter.GetBytes(arenaInformation.m_goalInnerRadiusMeter).CopyTo(bytes, 37);
        BitConverter.GetBytes(arenaInformation.m_goalSizeRadiusMeter).CopyTo(bytes, 41);
        BitConverter.GetBytes(arenaInformation.m_goalDepthMeter).CopyTo(bytes, 45);
        BitConverter.GetBytes(arenaInformation.m_droneSphereRadiusMeter).CopyTo(bytes, 49);
        m_taggedBytesToPush.Invoke(bytes);



    }



    public void SetTimeValueAsByte(DroneSoccerTimeValue timeValue)
    {
     
        byte[] bytes = new byte[1+4+4+8];
        bytes[0] = 2;
        BitConverter.GetBytes(timeValue.m_secondsSinceMatchStarted).CopyTo(bytes, 1);
        BitConverter.GetBytes(timeValue.m_secondsSinceSetStarted).CopyTo(bytes, 5);
        BitConverter.GetBytes(timeValue.m_timeOfServerDateTimeUtcNowTicks).CopyTo(bytes, 9);
        m_taggedBytesToPush.Invoke(bytes);

    }
    public void SetPointsStateAsByte(DroneSoccerMatchState pointsState)
    {
        byte[] bytes = new byte[1 + 4*4 + 2*8 ];
        bytes[0] = 3;
        BitConverter.GetBytes(pointsState.m_redPoints).CopyTo(bytes, 1);
        BitConverter.GetBytes(pointsState.m_bluePoints).CopyTo(bytes, 5);
        BitConverter.GetBytes(pointsState.m_redSets).CopyTo(bytes, 9);
        BitConverter.GetBytes(pointsState.m_blueSets).CopyTo(bytes, 13);
        BitConverter.GetBytes(pointsState.m_utcTickInSecondsWhenMatchStarted).CopyTo(bytes, 17);
        BitConverter.GetBytes(pointsState.m_utcTickInSecondsWhenMatchFinished).CopyTo(bytes, 25);

        m_taggedBytesToPush.Invoke(bytes);

    }


   

    int dronePositionByteLength = 1 + 16 + (9 * 12);
    byte[] bytesofPositionToSend;
    public void SetPositionsAsByte(DroneSoccerPositions positions)
    {
        //if(bytesofPositionToSend==null||  bytesofPositionToSend.Length != dronePositionByteLength)
        bytesofPositionToSend = new byte[dronePositionByteLength];
        bytesofPositionToSend[0] = 1;
        BitConverter.GetBytes(positions.m_dateTimeUtcTick).CopyTo(bytesofPositionToSend, 1);
        BitConverter.GetBytes(positions.m_framePushed).CopyTo(bytesofPositionToSend, 9);
        GetByteAt(bytesofPositionToSend, 17,     ref  positions.m_redDrone0Stricker);
        GetByteAt(bytesofPositionToSend, 17+9,   ref  positions.m_redDrone1);
        GetByteAt(bytesofPositionToSend, 17+18, ref positions.m_redDrone2);
        GetByteAt(bytesofPositionToSend, 17+27, ref positions.m_redDrone3);
        GetByteAt(bytesofPositionToSend, 17+36, ref positions.m_redDrone4);
        GetByteAt(bytesofPositionToSend, 17+45, ref positions.m_redDrone5);
        GetByteAt(bytesofPositionToSend, 17+54, ref positions.m_blueDrone0Stricker);
        GetByteAt(bytesofPositionToSend, 17+63, ref positions.m_blueDrone1);
        GetByteAt(bytesofPositionToSend, 17+72, ref positions.m_blueDrone2);
        GetByteAt(bytesofPositionToSend, 17+81, ref positions.m_blueDrone3);
        GetByteAt(bytesofPositionToSend, 17+90, ref positions.m_blueDrone4);
        GetByteAt(bytesofPositionToSend, 17+99, ref positions.m_blueDrone5);
        m_taggedBytesToPush.Invoke(bytesofPositionToSend);

    }

    private void GetByteAt(byte[] b, int index, ref DronePositionCompressed drone)
    {
       
        BitConverter.GetBytes(drone.m_localPositionX).CopyTo(b, index);
        BitConverter.GetBytes(drone.m_localPositionY).CopyTo(b, index+2);
        BitConverter.GetBytes(drone.m_localPositionZ).CopyTo(b, index+4);
        b[index + 6]=drone.m_eulerAngleX;
        b[index + 7]=drone.m_eulerAngleY;
        b[index + 8]=drone.m_eulerAngleZ;
    }

    public void SetPublicRsaKeyClaimAsText(DroneSoccerPublicRsaKeyClaim publicRsaKeyClaim)
    {
        StringBuilder sb = new StringBuilder();
        sb.AppendLine("<DroneSoccerPublicKeyRSA>");
        sb.AppendLine("    <ClaimRSA>"+publicRsaKeyClaim.m_blueDrone0Stricker+"</ClaimRSA>");
        sb.AppendLine("    <ClaimRSA>"+publicRsaKeyClaim.m_blueDrone1 + "</ClaimRSA>");
        sb.AppendLine("    <ClaimRSA>"+publicRsaKeyClaim.m_blueDrone2 + "</ClaimRSA>");
        sb.AppendLine("    <ClaimRSA>"+publicRsaKeyClaim.m_blueDrone3 + "</ClaimRSA>");
        sb.AppendLine("    <ClaimRSA>"+publicRsaKeyClaim.m_blueDrone4 + "</ClaimRSA>");
        sb.AppendLine("    <ClaimRSA>"+publicRsaKeyClaim.m_blueDrone5 + "</ClaimRSA>");
        sb.AppendLine("    <ClaimRSA>"+publicRsaKeyClaim.m_redDrone0Stricker + "</ClaimRSA>");
        sb.AppendLine("    <ClaimRSA>"+publicRsaKeyClaim.m_redDrone1 + "</ClaimRSA>");
        sb.AppendLine("    <ClaimRSA>"+publicRsaKeyClaim.m_redDrone2 + "</ClaimRSA>");
        sb.AppendLine("    <ClaimRSA>"+publicRsaKeyClaim.m_redDrone3 + "</ClaimRSA>");
        sb.AppendLine("    <ClaimRSA>"+publicRsaKeyClaim.m_redDrone4 + "</ClaimRSA>");
        sb.AppendLine("    <ClaimRSA>" + publicRsaKeyClaim.m_redDrone5 + "</ClaimRSA>");
        sb.AppendLine("</DroneSoccerPublicKeyRSA>");
        m_textToPush.Invoke(sb.ToString());
    }
    public void SetIndexIntegerClaimAsByte(DroneSoccerIndexIntegerClaim indexIntegerClaim)
    {

        byte[] bytes =new byte[1+4*12];
        bytes[0] = 12;
        BitConverter.GetBytes(indexIntegerClaim.m_redDrone0Stricker).CopyTo(bytes, 1);
        BitConverter.GetBytes(indexIntegerClaim.m_redDrone1).CopyTo(bytes, 5);
        BitConverter.GetBytes(indexIntegerClaim.m_redDrone2).CopyTo(bytes, 9);
        BitConverter.GetBytes(indexIntegerClaim.m_redDrone3).CopyTo(bytes, 13);
        BitConverter.GetBytes(indexIntegerClaim.m_redDrone4).CopyTo(bytes, 17);
        BitConverter.GetBytes(indexIntegerClaim.m_redDrone5).CopyTo(bytes, 21);
        BitConverter.GetBytes(indexIntegerClaim.m_blueDrone0Stricker).CopyTo(bytes, 25);
        BitConverter.GetBytes(indexIntegerClaim.m_blueDrone1).CopyTo(bytes, 29);
        BitConverter.GetBytes(indexIntegerClaim.m_blueDrone2).CopyTo(bytes, 33);
        BitConverter.GetBytes(indexIntegerClaim.m_blueDrone3).CopyTo(bytes, 37);
        BitConverter.GetBytes(indexIntegerClaim.m_blueDrone4).CopyTo(bytes, 41);
        BitConverter.GetBytes(indexIntegerClaim.m_blueDrone5).CopyTo(bytes, 45);
        m_taggedBytesToPush.Invoke(bytes);
    }

    public void SetSoccerBallAsByte(DroneSoccerBallState state)
    {

        long serverTickTime = state.m_dateTimeUtcTick;
        Vector3 e = state.m_rotation.eulerAngles;
        byte eulerX = (byte)((e.x % 360f / 360f) * 255f);
        byte eulerY = (byte)((e.y % 360f / 360f) * 255f);
        byte eulerZ = (byte)((e.z % 360f / 360f) * 255f);


        byte[] bytes = new byte[1 + 4 * 3 + 2 * 3];
        bytes[0] = 8;
        BitConverter.GetBytes(serverTickTime).CopyTo(bytes, 1);
        BitConverter.GetBytes(ClampShort(state.m_position.x)).CopyTo(bytes, 9);
        BitConverter.GetBytes(ClampShort(state.m_position.y)).CopyTo(bytes, 11);
        BitConverter.GetBytes(ClampShort(state.m_position.z)).CopyTo(bytes, 13);
        bytes[15] = eulerX;
        bytes[16] = eulerY;
        bytes[17] = eulerZ;
        m_taggedBytesToPush.Invoke(bytes);
    }

    private static ushort ClampShort(float value)
    {
        return (ushort)Mathf.Clamp(value, short.MinValue, short.MaxValue);
    }

    public void SetSoccerBallGoalsAsBytes(DroneSoccerBallGoals ballGoals)
    {
        byte[] bytes = new byte[1 + 4 * 5];
        bytes[0] = 14;
        BitConverter.GetBytes(ballGoals.m_goalDepthMeter).CopyTo(bytes, 1);
        BitConverter.GetBytes(ballGoals.m_goalDistanceOfCenterMeter).CopyTo(bytes, 5);
        BitConverter.GetBytes(ballGoals.m_goalCenterHeightMeter).CopyTo(bytes, 9);
        BitConverter.GetBytes(ballGoals.m_goalWidthRadiusMeter).CopyTo(bytes, 13);
        BitConverter.GetBytes(ballGoals.m_ballRadius).CopyTo(bytes, 17);
        m_taggedBytesToPush.Invoke(bytes);
    }
}






[System.Serializable]
public struct SphereProjectileCreated
{

    public int m_poolId;
    public int m_itemPoolId;
    public long m_serverTime;
    public Vector3 m_startPoint;
    public Vector3 m_eulerStartRotation;
    public Vector3 m_directionSpeed;
    public float m_radius;

}

[System.Serializable]
public struct SphereProjectileDestroyed
{

    public int m_poolId;
    public int m_itemPoolId;
    public long m_serverTime;

}


[System.Serializable]
public struct CapsuleProjectile 
{
    public Vector3 m_currentPoint;
    public Vector3 m_previousPoint;
    public float m_radius;
    
}

