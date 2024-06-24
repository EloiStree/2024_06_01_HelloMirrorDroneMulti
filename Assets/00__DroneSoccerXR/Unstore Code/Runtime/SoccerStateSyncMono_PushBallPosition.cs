
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SoccerStateSyncMono_PushBallPosition : MonoBehaviour , MSoccerMono_RefreshablePush
{

    public DroneSoccerBallState m_ballPosition;
    public UnityEvent<DroneSoccerBallState> m_onBallPositionChanged;




    public GameObject m_isBallActive;
    public Transform m_ballCenter;
    public Transform m_arenaCenter;

    public float m_framePerSeconds = 12;

    public void RefreshAndPush()
    {
        m_ballPosition.m_dateTimeUtcTick = DateTime.UtcNow.Ticks;
        MSoccer_RelocationUtility.GetWorldToLocal_DirectionalPoint(
        transform.position,
        transform.rotation,
        m_arenaCenter,
        out Vector3 localPosition,
        out Quaternion localRotation
        );
        m_ballPosition.m_position = (localPosition);
        m_ballPosition.m_rotation = (localRotation);


        m_onBallPositionChanged.Invoke(m_ballPosition);
    }

    public IEnumerator Start()
    {
        while (true)
        {
           
            RefreshAndPush();
            yield return new WaitForSeconds(1f / m_framePerSeconds);
        }

    }


}
