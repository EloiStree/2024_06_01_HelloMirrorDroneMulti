
using System;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_AbstractGamepad: MonoBehaviour
{

    [Range(-1f,1f)]
    public float m_rotateHorizontalPercent;
    [Range(-1f, 1f)]
    public float m_moveBackForwardPercent;
    [Range(-1f, 1f)]
    public float m_moveLeftRightPercent;
    [Range(-1f, 1f)]
    public float m_moveDownUpPercent;
    public UnityEvent<float> m_onRotateHorizontalPercent;
    public UnityEvent<float> m_onMoveBackForwardPercent;
    public UnityEvent<float> m_onMoveLeftRightPercent;
    public UnityEvent<float> m_onMoveDownUpPercent;


    private void OnValidate()
    {
        m_onRotateHorizontalPercent.Invoke(m_rotateHorizontalPercent);
        m_onMoveBackForwardPercent.Invoke(m_moveBackForwardPercent);
        m_onMoveLeftRightPercent.Invoke(m_moveLeftRightPercent);
        m_onMoveDownUpPercent.Invoke(m_moveDownUpPercent);
    }
    public void SetHorizontalRotation(float percent)
    {
        percent = Mathf.Clamp(percent, -1f, 1f);
        m_rotateHorizontalPercent = percent;
        m_onRotateHorizontalPercent.Invoke(percent);
    }

    public void SetFrontalMove(float percent)
    {
        percent = Mathf.Clamp(percent, -1f, 1f);
        m_moveBackForwardPercent = percent;
        m_onMoveBackForwardPercent.Invoke(percent);
    }

    public void SetHorizontaMove(float percent)
    {
        percent = Mathf.Clamp(percent, -1f, 1f);
        m_moveLeftRightPercent = percent;
        m_onMoveLeftRightPercent.Invoke(percent);
    }

    public void SetVerticalMove(float percent)
    {
        percent = Mathf.Clamp(percent, -1f, 1f);
        m_moveDownUpPercent = percent;
        m_onMoveDownUpPercent.Invoke(percent);
    }

    public void SetJoystickLeftValue(Vector2 joystickLeftValue)
    {
        SetHorizontalRotation(joystickLeftValue.x);
        SetVerticalMove(joystickLeftValue.y);
    }

    public void SetJoystickRightValue(Vector2 joystickRightValue)
    {
        SetHorizontaMove(joystickRightValue.x);
        SetFrontalMove(joystickRightValue.y);
    }
}
