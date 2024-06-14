using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LocalUnity3DToMultiDroneCommandsMono : MonoBehaviour
{

    public Vector2 m_joystickLeft;
    public Vector2 m_joystickRight;
    public void SetWithJoystickLeft(Vector3 joystickLeft) { 
        m_joystickLeft= new Vector3(joystickLeft.x,joystickLeft.y,0); Refresh();
    }
    public void SetWithJoystickRight(Vector3 joystickRight) { 
    
        m_joystickRight= new Vector3(joystickRight.x,joystickRight.y,0); Refresh();
    }
    public void SetWithJoystickLeft(Vector2 joystickLeft) { 
        m_joystickLeft= new Vector2(joystickLeft.x,joystickLeft.y); Refresh();
    }
    public void SetWithJoystickRight(Vector2 joystickRight) {
        m_joystickRight= new Vector2(joystickRight.x,joystickRight.y); Refresh();
    }
    public void SetJoystickLeftX(float x) { m_joystickLeft.x = x; Refresh(); }
    public void SetJoystickLeftY(float y) { m_joystickLeft.y = y; Refresh(); }
    public void SetJoystickRightX(float x) { m_joystickRight.x = x; Refresh(); }
    public void SetJoystickRightY(float y) { m_joystickRight.y = y; Refresh(); }
    public void SetRotationLeftRight(float x) { m_joystickLeft.x = x; Refresh(); }
    public void SetMoveDownUp(float y) { m_joystickLeft.y = y; Refresh(); }
    public void SetMoveLeftRight(float x) { m_joystickRight.x = x; Refresh(); }
    public void SetMoveBacKForward(float y) { m_joystickRight.y = y; Refresh(); }

    public void Refresh() {

        if(MSoccerMono_PushMultiDroneCommands.OwnedPusherInScene)
       MSoccerMono_PushMultiDroneCommands.OwnedPusherInScene.
            PushDroneCommandsToFocusDrone(m_joystickLeft.x,m_joystickLeft.y,m_joystickRight.x,m_joystickRight.y);
    }
}
