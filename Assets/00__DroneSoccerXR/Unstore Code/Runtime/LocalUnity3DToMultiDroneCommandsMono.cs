using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalUnity3DToMultiDroneCommandsMono : MonoBehaviour
{

    public int m_targetDrone20to20 = -1;
    public Vector2 m_joystickLeft;
    public Vector2 m_joystickRight;
   
    public void SetWithJoystickLeft(Vector3 joystickLeft) { 
        if(NewValue(joystickLeft,m_joystickLeft))
        m_joystickLeft= new Vector3(joystickLeft.x,joystickLeft.y,0); Refresh();
    }
    public void SetWithJoystickRight(Vector3 joystickRight) {

        if (NewValue(joystickRight, m_joystickRight))
            m_joystickRight = new Vector3(joystickRight.x,joystickRight.y,0); Refresh();
    }
    public void SetWithJoystickLeft(Vector2 joystickLeft)
    {
        if (NewValue(joystickLeft, m_joystickLeft))
            m_joystickLeft = new Vector2(joystickLeft.x,joystickLeft.y); Refresh();
    }
    public void SetWithJoystickRight(Vector2 joystickRight)
    {
        if (NewValue(joystickRight, m_joystickRight))
            m_joystickRight = new Vector2(joystickRight.x,joystickRight.y); Refresh();
    }
    public void SetJoystickLeftX(float x) {     if (m_joystickLeft.x != x  ) { m_joystickLeft.x = x  ; Refresh(); } }
    public void SetJoystickLeftY(float y) {     if (m_joystickLeft.y != y  ) { m_joystickLeft.y = y  ;  Refresh(); }}
public void SetJoystickRightX(float x) {        if (m_joystickRight.x != x ) { m_joystickRight.x = x ; Refresh(); }}
    public void SetJoystickRightY(float y) {    if (m_joystickRight.y != y ) { m_joystickRight.y = y ; Refresh(); }}
    public void SetRotationLeftRight(float x) { if (m_joystickLeft.x != x  ) { m_joystickLeft.x = x  ;  Refresh(); }}
    public void SetMoveDownUp(float y) {        if (m_joystickLeft.y != y  ) { m_joystickLeft.y = y  ;  Refresh(); }}
    public void SetMoveLeftRight(float x) {     if (m_joystickRight.x != x ) { m_joystickRight.x = x ; Refresh(); }}
    public void SetMoveBackForward(float y) {   if (m_joystickRight.y != y) { m_joystickRight.y = y ; Refresh(); }}

    public void Refresh() {

        if(MSoccerMono_PushMultiDroneCommands.OwnedPusherInScene)
       MSoccerMono_PushMultiDroneCommands.OwnedPusherInScene.
            PushDroneCommandsToFocusDrone(m_targetDrone20to20, m_joystickLeft.x,m_joystickLeft.y,m_joystickRight.x,m_joystickRight.y);
    }

    private bool NewValue(Vector2 newValue, Vector2 previousValue)
    {
        return newValue.x != previousValue.x || newValue.y != previousValue.y;
    }
    private bool NewValue(Vector3 newValue, Vector2 previousValue)
    {
        return newValue.x != previousValue.x || newValue.y != previousValue.y;
    }
}
