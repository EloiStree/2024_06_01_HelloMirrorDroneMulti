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

    private byte m_byte_dronePositions=10;
    private byte m_byte_matchTimeValue=11;
    private byte m_byte_soccerBall=15;

    private byte m_byte_pointsState=20;
    
    private byte m_byte_indexIntegerClaim=60;
    private byte m_byte_publicRsaKeyClaim = 61;
    private byte m_byte_publicEllipticCurveClaim = 62;

    private byte m_byte_arenaInformation=30;
    private byte m_byte_soccerBallGoals=35;


    private CPS_DroneSoccerPositions m_dronePositions = new CPS_DroneSoccerPositions();
    private CPS_DroneSoccerTimeValue m_matchTimeValue = new CPS_DroneSoccerTimeValue();

    private CPS_DroneSoccerMatchStaticInformation m_arenaInformation = new CPS_DroneSoccerMatchStaticInformation();
    private CPS_DroneSoccerMatchState m_pointsState = new CPS_DroneSoccerMatchState();
    
    private CPS_DroneSoccerPublicXmlRsaKey1024Claim m_publicRsaKeyClaim = new CPS_DroneSoccerPublicXmlRsaKey1024Claim();
    private CPS_DroneSoccerIndexIntegerClaim m_indexIntegerClaim = new CPS_DroneSoccerIndexIntegerClaim();

    private CPS_DroneSoccerBallPosition m_soccerBall = new CPS_DroneSoccerBallPosition();
    private CPS_DroneSoccerBallGoals m_soccerBallGoals = new CPS_DroneSoccerBallGoals();

    public void SetArenaInformationAsByte(S_DroneSoccerMatchStaticInformation arenaInformation)
    {

        m_arenaInformation.Parse(m_byte_arenaInformation, arenaInformation, out byte[] bytes);
        m_taggedBytesToPush.Invoke(bytes);
    }



    public void SetTimeValueAsByte(S_DroneSoccerTimeValue timeValue)
    {
        m_matchTimeValue.Parse(m_byte_matchTimeValue, timeValue, out byte[] bytes);
        m_taggedBytesToPush.Invoke(bytes);

    }
    public void SetPointsStateAsByte(S_DroneSoccerMatchState pointsState)
    {
        m_pointsState.Parse(m_byte_pointsState, pointsState, out byte[] bytes);
        m_taggedBytesToPush.Invoke(bytes);

    }

    byte[] bytesofPositionToSend;
    public void SetPositionsAsByte(S_DroneSoccerPositions positions)
    {
        //if(bytesofPositionToSend==null||  bytesofPositionToSend.Length != dronePositionByteLength)
        m_dronePositions.Parse(m_byte_dronePositions, positions, out byte[] bytes);
        m_taggedBytesToPush.Invoke(bytesofPositionToSend);

    }


    //public void SetPublicRsaKeyClaimAsText(S_DroneSoccerPublicXmlRsaKey1024Claim publicRsaKeyClaim)
    //{
    //    m_arenaInformation.Parse(m_byte_arenaInformation, publicRsaKeyClaim, out byte[] bytes);
    //    m_textToPush.Invoke(sb.ToString());
    //}
    public void SetPublicRsaKeyClaimAsByte(S_DroneSoccerPublicXmlRsaKey1024Claim publicRsaKeyClaim)
    {
        m_publicRsaKeyClaim.Parse(m_byte_publicRsaKeyClaim, publicRsaKeyClaim, out byte[] bytes);
        m_taggedBytesToPush.Invoke(bytes);

    }
    public void SetIndexIntegerClaimAsByte(S_DroneSoccerIndexIntegerClaim indexIntegerClaim)
    {
        m_indexIntegerClaim.Parse(m_byte_indexIntegerClaim, indexIntegerClaim, out byte[] bytes);
        m_taggedBytesToPush.Invoke(bytes);
    }

    public void SetSoccerBallAsByte(S_DroneSoccerBallPosition state)
    {

        m_soccerBall.Parse(m_byte_soccerBall, state, out byte[] bytes);
        m_taggedBytesToPush.Invoke(bytes);
    }

   

    public void SetSoccerBallGoalsAsBytes(S_DroneSoccerBallGoals ballGoals)
    {
        m_soccerBallGoals.Parse(m_byte_soccerBallGoals, ballGoals, out byte[] bytes);
        m_taggedBytesToPush.Invoke(bytes);
    }
}






[System.Serializable]
public struct SphereProjectileCreated
{

    //public int m_poolId;
    //public int m_itemPoolId;
    //public long m_serverTime;
    //public Vector3 m_startPoint;
    //public Vector3 m_eulerStartRotation;
    //public Vector3 m_directionSpeed;
    //public float m_radius;

}

[System.Serializable]
public struct SphereProjectileDestroyed
{

    //public int m_poolId;
    //public int m_itemPoolId;
    //public long m_serverTime;

}


[System.Serializable]
public struct CapsuleProjectile 
{
    //public Vector3 m_currentPoint;
    //public Vector3 m_previousPoint;
    //public float m_radius;
    
}

