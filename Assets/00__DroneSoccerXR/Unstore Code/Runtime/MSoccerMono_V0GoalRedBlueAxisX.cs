using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_V0GoalRedBlueAxisX : MonoBehaviour
{

    public Transform m_ballStartPointPosition;
    public Transform m_ballToOverwatchPosition;
    public Transform m_arenaCenter;
    public float m_axisDistanceX = 4f;
    public bool m_leftIsRedAndRightBlue = true;
    public UnityEvent m_goalForBlue;
    public UnityEvent m_goalForRed;

    void Update()
    {

        MSoccer_RelocationUtility.GetWorldToLocal_DirectionalPoint(
            m_ballToOverwatchPosition.position,
            m_ballToOverwatchPosition.rotation,
            m_arenaCenter,
            out Vector3 localPosition,
            out Quaternion localRotation
            );

        if(Mathf.Abs(localPosition.x) < m_axisDistanceX) {
            return;
        }
        bool isInRedGoal = localPosition.x < 0;
        bool isInBlueGoal = localPosition.x > 0;

        if (m_leftIsRedAndRightBlue) {

            isInRedGoal = !isInRedGoal;
            isInBlueGoal = !isInBlueGoal;
        }
        
        if (isInRedGoal) { 
            m_ballToOverwatchPosition.position = m_ballStartPointPosition.position;
            m_goalForRed.Invoke();
        }

        if (isInBlueGoal)
        {
            m_ballToOverwatchPosition.position = m_ballStartPointPosition.position;
            m_goalForBlue.Invoke();
        
        }
        
    }
}
