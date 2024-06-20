using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_ClientInterpolationFollow : MonoBehaviour
{

    public Transform m_willMaybeBe;
    public Transform m_targetToPredict;

    public PositionFrame m_futurePosition2Frame;
    public PositionFrame m_futurePosition1Frame;
    public PositionFrame m_lastReceived;
    public PositionFrame m_delayOf1Frame;
    public PositionFrame m_delayOf2Frame;
    public PositionFrame m_prediction;


    public int m_serverFramePerSeconds = 12;
    public float m_delayShouldBe;
    public float m_delayWas;
    public DateTime m_currentInfo;
    public DateTime m_lastInfo;

    public float m_rotation;
    private void Awake()
    {
        m_delayShouldBe = 1f / m_serverFramePerSeconds;
    }

    [SerializeField]
    public struct PositionFrame {
        public long m_timeNowUtc;
        public Vector3 m_position;
        public Quaternion m_rotation;
    }

    public Quaternion m_nextRotationDirection;
    public Vector3 m_nextPositionDirection;

    public bool m_useDebug;
    float m_delaySinceLastUpdate;
    public void NotifyNetworkMessageReceived()
    {
        m_lastInfo = m_currentInfo;
        m_currentInfo = DateTime.Now;
        m_delayWas = (float)(m_currentInfo - m_lastInfo).TotalSeconds;
        m_delayOf2Frame = m_delayOf1Frame;
        m_delayOf1Frame = m_lastReceived;
        m_lastReceived = new PositionFrame() { m_position = m_targetToPredict.position, m_rotation = m_targetToPredict.rotation, m_timeNowUtc = GetNow() };

        m_nextRotationDirection = m_lastReceived.m_rotation * Quaternion.Inverse(m_delayOf1Frame.m_rotation);
        m_nextPositionDirection = m_lastReceived.m_position - m_delayOf1Frame.m_position;

        m_futurePosition1Frame.m_position = m_lastReceived.m_position + m_nextPositionDirection;
        m_futurePosition2Frame.m_position = m_lastReceived.m_position + m_nextPositionDirection*2;

        m_futurePosition1Frame.m_rotation = m_lastReceived.m_rotation * m_nextRotationDirection;
        m_futurePosition2Frame.m_rotation = (m_lastReceived.m_rotation * m_nextRotationDirection) * m_nextRotationDirection;

        m_futurePosition1Frame.m_timeNowUtc = m_lastReceived.m_timeNowUtc + (long)m_delayShouldBe * TimeSpan.TicksPerSecond;
        m_futurePosition2Frame.m_timeNowUtc = m_lastReceived.m_timeNowUtc + (long)m_delayShouldBe*2 * TimeSpan.TicksPerSecond;

        if (m_useDebug) { 
            Debug.DrawLine(m_lastReceived.m_position, m_futurePosition2Frame.m_position, Color.green, m_delayWas * 2);
            Debug.DrawLine(m_lastReceived.m_position, m_lastReceived.m_position+ m_futurePosition2Frame.m_rotation * Vector3.forward, Color.magenta, m_delayWas*2);
        }
        m_timeSinceLastUpdate = 0;


    }


    public float m_lerpRotation=0.3f;
    public float m_lerpPosition=0.65f;
    public float delaySinceLast;
    public float percent;

    public float m_timeSinceLastUpdate = 0;
    private void Update()
    {
        m_timeSinceLastUpdate += Time.deltaTime;
        float percent = m_timeSinceLastUpdate / (m_delayShouldBe*2f );
        Vector3 willBePosition = Vector3.Lerp(m_lastReceived.m_position, m_futurePosition2Frame.m_position, percent);
        Quaternion willBeRotation = Quaternion.Lerp(m_lastReceived.m_rotation, m_futurePosition2Frame.m_rotation, percent);


        m_willMaybeBe.position = willBePosition;    
        m_willMaybeBe.rotation = willBeRotation;

    }



    public void Start()
    {
        //Should be attack to receiving messag instead
        InvokeRepeating("NotifyNetworkMessageReceived", 0, 1f / m_serverFramePerSeconds);
    }

    public long GetNow()
    {
            return DateTime.Now.Ticks;
    }

}
