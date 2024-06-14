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


    public void SetPublicRsaWithBytesArray(DroneSoccerPublicRsaKeyClaim rsaKey)
    {

        ConvertPublicRsaToBytesUtility.ParsePublicRsaKeyToBytesWithoutModule(rsaKey.m_redDrone0Stricker, out byte[] redDrone0Stricker);
        ConvertPublicRsaToBytesUtility.ParsePublicRsaKeyToBytesWithoutModule(rsaKey.m_redDrone1, out byte[] redDrone1);
        ConvertPublicRsaToBytesUtility.ParsePublicRsaKeyToBytesWithoutModule(rsaKey.m_redDrone2, out byte[] redDrone2);
        ConvertPublicRsaToBytesUtility.ParsePublicRsaKeyToBytesWithoutModule(rsaKey.m_redDrone3, out byte[] redDrone3);
        ConvertPublicRsaToBytesUtility.ParsePublicRsaKeyToBytesWithoutModule(rsaKey.m_redDrone4, out byte[] redDrone4);
        ConvertPublicRsaToBytesUtility.ParsePublicRsaKeyToBytesWithoutModule(rsaKey.m_redDrone5, out byte[] redDrone5);
        ConvertPublicRsaToBytesUtility.ParsePublicRsaKeyToBytesWithoutModule(rsaKey.m_blueDrone0Stricker, out byte[] blueDrone0Stricker);
        ConvertPublicRsaToBytesUtility.ParsePublicRsaKeyToBytesWithoutModule(rsaKey.m_blueDrone1, out byte[] blueDrone1);
        ConvertPublicRsaToBytesUtility.ParsePublicRsaKeyToBytesWithoutModule(rsaKey.m_blueDrone2, out byte[] blueDrone2);
        ConvertPublicRsaToBytesUtility.ParsePublicRsaKeyToBytesWithoutModule(rsaKey.m_blueDrone3, out byte[] blueDrone3);
        ConvertPublicRsaToBytesUtility.ParsePublicRsaKeyToBytesWithoutModule(rsaKey.m_blueDrone4, out byte[] blueDrone4);
        ConvertPublicRsaToBytesUtility.ParsePublicRsaKeyToBytesWithoutModule(rsaKey.m_blueDrone5, out byte[] blueDrone5);
        byte[] drones = new byte[1+(128* 12)];
        drones[0] = 11;
        Buffer.BlockCopy(redDrone0Stricker, 0,       drones, 1, 128);
        Buffer.BlockCopy(redDrone1, 0,               drones, 1 + 128, 128);
        Buffer.BlockCopy(redDrone2, 0,               drones, 1 + 256, 128);
        Buffer.BlockCopy(redDrone3, 0,               drones, 1 + 384, 128);
        Buffer.BlockCopy(redDrone4, 0,               drones, 1 + 512, 128);
        Buffer.BlockCopy(redDrone5, 0,               drones, 1 + 640, 128);
        Buffer.BlockCopy(blueDrone0Stricker, 0,      drones, 1 + 768, 128);
        Buffer.BlockCopy(blueDrone1, 0,              drones, 1 + 896, 128);
        Buffer.BlockCopy(blueDrone2, 0,              drones, 1 + 1024, 128);
        Buffer.BlockCopy(blueDrone3, 0,              drones, 1 + 1152, 128);
        Buffer.BlockCopy(blueDrone4, 0,              drones, 1 + 1280, 128);
        Buffer.BlockCopy(blueDrone5, 0,              drones, 1 + 1408, 128);
        m_taggedBytesToPush.Invoke(drones);




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
        sb.AppendLine("<xml>");
        sb.AppendLine(publicRsaKeyClaim.m_blueDrone0Stricker);
        sb.AppendLine(publicRsaKeyClaim.m_blueDrone1);
        sb.AppendLine(publicRsaKeyClaim.m_blueDrone2);
        sb.AppendLine(publicRsaKeyClaim.m_blueDrone3);
        sb.AppendLine(publicRsaKeyClaim.m_blueDrone4);
        sb.AppendLine(publicRsaKeyClaim.m_blueDrone5);
        sb.AppendLine(publicRsaKeyClaim.m_redDrone0Stricker);
        sb.AppendLine(publicRsaKeyClaim.m_redDrone1);
        sb.AppendLine(publicRsaKeyClaim.m_redDrone2);
        sb.AppendLine(publicRsaKeyClaim.m_redDrone3);
        sb.AppendLine(publicRsaKeyClaim.m_redDrone4);
        sb.AppendLine(publicRsaKeyClaim.m_redDrone5);
        sb.AppendLine("</xml>");
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

