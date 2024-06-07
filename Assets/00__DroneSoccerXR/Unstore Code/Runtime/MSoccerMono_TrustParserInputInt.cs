using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_TrustParserInputInt : MonoBehaviour
{


    public int m_lastIndexReceived;
    public List<DroneToAffect> m_dronesToAffect;


    [System.Serializable]
    public class DroneToAffect
    {
        public int m_integerUniqueId20to20;
        public MSoccerMono_AbstractGamepad m_gamepad;
    }



    public void PushIntegerValueFromByte(byte[] bytes)
    {

        if (bytes == null || bytes.Length != 4) return;

        int cmdInteger = BitConverter.ToInt32(bytes, 0);

        m_lastIndexReceived = cmdInteger;
        PushIntegerValue(cmdInteger);
    }

    public void PushIntegerValue(int integerCommand)
    {

        if (this.enabled == false)
            return;
        if (this.gameObject.activeInHierarchy == false)
            return;

        ParserIntegerToDronePercentUtility.PushIntegerValue(integerCommand,
            out int targetDrone,
            out float joystickLeftXPercent,
            out float joystickLeftYPercent,
            out float joystickRightXPercent,
            out float joystickRightYPercent
            );

        foreach (var item in m_dronesToAffect)
        {
            if (item.m_integerUniqueId20to20 == targetDrone)
            {
                
                    item.m_gamepad.SetHorizontalRotation(joystickLeftXPercent);
                    item.m_gamepad.SetVerticalMove(joystickLeftYPercent);
                    item.m_gamepad.SetHorizontaMove(joystickRightXPercent);
                    item.m_gamepad.SetFrontalMove(joystickRightYPercent);
                

            }
        }
    }
}
