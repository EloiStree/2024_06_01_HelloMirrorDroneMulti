using System;
using System.Collections.Generic;
using UnityEngine;
/// <summary>
/// IID: Index, integer, Date
/// </summary>
public class MSoccerMono_TrustParserInputIID : MonoBehaviour
{

    public int m_lastIndexReceived;
    public int m_lastIntegerReceived;
    public long m_lastDateReceived;

    public List<DroneToAffect> m_dronesToAffect;


    [System.Serializable]
    public class DroneToAffect
    {
        public int m_indexAllowtoInteract;
        public int m_integerDroneId20to20;
        public MSoccerMono_AbstractGamepad m_gamepad;
    }


    
    public void PushIndexIntegerDateFromByte(byte[] bytes) {
    
        if(bytes==null || bytes.Length!=16) return;
        int index = BitConverter.ToInt32(bytes, 0);
        int integerCommand = BitConverter.ToInt32(bytes, 4);
        ulong longDate = BitConverter.ToUInt64(bytes, 8);

        DateTime date = new DateTime((long)longDate);
        m_lastIndexReceived = index;
        m_lastIntegerReceived = integerCommand;
        m_lastDateReceived = (long)longDate;
        PushIndexIntergerDateValue(index, integerCommand, date);    
    }


    public void PushIndexIntergerDateValue(int index, int integerCommand, DateTime date)
    {

        if (this.enabled == false)
            return;
        if (this.gameObject.activeInHierarchy == false)
            return;

        ParserIntegerToDronePercentUtility.PushIntegerValue(integerCommand,
            out int m_targetDrone,
            out float m_joystickRightYPercent,
            out float m_joystickRightXPercent,
            out float m_joystickLeftYPercent,
            out float m_joystickLeftXPercent
            );

        foreach (var item in m_dronesToAffect)
        {
            if (item.m_indexAllowtoInteract== index &&  item.m_integerDroneId20to20== m_targetDrone)
            {
                    item.m_gamepad.SetHorizontalRotation(m_joystickLeftXPercent);
                    item.m_gamepad.SetVerticalMove(m_joystickLeftYPercent);
                    item.m_gamepad.SetHorizontaMove(m_joystickRightXPercent);
                    item.m_gamepad.SetFrontalMove(m_joystickRightYPercent);
            }
        }
    }
}
