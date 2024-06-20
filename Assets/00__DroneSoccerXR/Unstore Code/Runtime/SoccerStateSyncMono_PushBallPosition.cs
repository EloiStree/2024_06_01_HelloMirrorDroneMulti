﻿
using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class SoccerStateSyncMono_PushBallPosition : MonoBehaviour
{

    public DroneSoccerBallState m_ballPosition;
    public UnityEvent<DroneSoccerBallState> m_onBallPositionChanged;


    public GameObject m_isBallActive;
    public Transform m_ballCenter;
    public Transform m_arenaCenter;

    public float m_framePerSeconds = 12;

    public IEnumerator Start()
    {
        while (true)
        {
            m_ballPosition.m_useBall = m_isBallActive.activeInHierarchy;
            m_ballPosition.m_dateTimeUtcTick = DateTime.UtcNow.Ticks;
            MSoccer_RelocationUtility.GetWorldToLocal_DirectionalPoint(
           transform.position,
           transform.rotation,
           m_arenaCenter,
           out Vector3 localPosition,
           out Quaternion localRotation
           );
            m_ballPosition.m_position=(localPosition);
            m_ballPosition.m_rotation=(localRotation);
            m_ballPosition.m_radius = m_ballCenter.localScale.x / 2;


            m_onBallPositionChanged.Invoke(m_ballPosition);
            yield return new WaitForSeconds(1f / m_framePerSeconds);
        }

    }


}