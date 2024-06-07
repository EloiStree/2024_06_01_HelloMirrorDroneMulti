using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_PushGameStateAsLocalUdp : MonoBehaviour
{
    public UnityEvent<byte[]> m_taggedBytesToPush;
    public UnityEvent<string> m_textToPush;
    public void SetArenaInformationAsByte(DroneSoccerMatchStaticInformation arenaInformation)
    {

    }
    public void SetTimeValueAsByte(DroneSoccerTimeValue timeValue)
    {

    }
    public void SetPointsStateAsByte(DroneSoccerMatchState pointsState)
    {

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
        Debug.Log(index + 8);
        BitConverter.GetBytes(drone.m_localPositionX).CopyTo(b, index);
        BitConverter.GetBytes(drone.m_localPositionY).CopyTo(b, index+2);
        BitConverter.GetBytes(drone.m_localPositionZ).CopyTo(b, index+4);
        b[index + 6]=drone.m_eulerAngleX;
        b[index + 7]=drone.m_eulerAngleY;
        b[index + 8]=drone.m_eulerAngleZ;
    }

    public void SetPublicRsaKeyClaimAsText(DroneSoccerPublicRsaKeyClaim publicRsaKeyClaim)
    {
    }
    public void SetIndexIntegerClaimAsByte(DroneSoccerIndexIntegerClaim indexIntegerClaim)
    {
    }
}
