using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class ScreenInputToGamepadJoystickMono : MonoBehaviour
{

    public UnityEvent<Vector2> m_joystickLeft;
    public UnityEvent<Vector2> m_joystickRight;


    public Vector2 m_startScreenPointFirstContact;
    public Vector2 m_startScreenPointSecondsContact;

    public Vector2 m_currentScreenPointFirstFinger;
    public Vector2 m_currentScreenPointSecondFinger;


    public Vector2 m_deltaContactPointOne;
    public Vector2 m_deltaContactPointTwo;

    public Vector2 m_deltaContactPointOnePercent;
    public Vector2 m_deltaContactPointTwoPercent;

    public int m_newContactPointsCount;
    public int m_previousContactPointsCount;

    public float m_screenPercentFromHeight = 0.25f;

    public int m_screenPixelToPercent;
    public void Awake()
    {
        Input.multiTouchEnabled = true;
        m_screenPercentFromHeight = Screen.height * m_screenPercentFromHeight;
    }

    public void SetInputTouchCount(int count)
    {
        m_newContactPointsCount = count;
    }
    public void SetTouchPositionOne(Vector2 position) { m_currentScreenPointFirstFinger = position; }
    public void SetTouchPositionTwo(Vector2 position) { m_currentScreenPointSecondFinger = position; }


    void Update()
    {
      
        bool isTouchingOrChanged = m_newContactPointsCount > 0;
       

            if (m_startScreenPointFirstContact==Vector2.zero && m_currentScreenPointFirstFinger!=Vector2.zero)
            {
                m_startScreenPointFirstContact = m_currentScreenPointFirstFinger;

                isTouchingOrChanged = true;
            }
            if(m_startScreenPointSecondsContact==Vector2.zero && m_currentScreenPointSecondFinger!=Vector2.zero)
            {
                m_startScreenPointSecondsContact = m_currentScreenPointSecondFinger;
                isTouchingOrChanged = true;
            }
            if(m_currentScreenPointFirstFinger==Vector2.zero|| m_newContactPointsCount==0)
            {
                m_startScreenPointFirstContact = Vector2.zero;
            }
            if(m_currentScreenPointSecondFinger==Vector2.zero || m_newContactPointsCount == 0)
            {
                m_startScreenPointSecondsContact = Vector2.zero;
            }

            m_deltaContactPointOne = m_currentScreenPointFirstFinger - m_startScreenPointFirstContact;
            m_deltaContactPointTwo = m_currentScreenPointSecondFinger - m_startScreenPointSecondsContact;

            m_deltaContactPointOnePercent = m_deltaContactPointOne / m_screenPercentFromHeight;
            m_deltaContactPointTwoPercent = m_deltaContactPointTwo / m_screenPercentFromHeight;

        if (
            isTouchingOrChanged) { 
        
            m_joystickLeft.Invoke(m_deltaContactPointOnePercent);
            m_joystickRight.Invoke(m_deltaContactPointTwoPercent);
        }

        

    }
}
