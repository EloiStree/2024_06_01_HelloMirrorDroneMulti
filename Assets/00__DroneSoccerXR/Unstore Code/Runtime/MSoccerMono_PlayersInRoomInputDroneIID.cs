using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_PlayersInRoomInputDroneIID : MonoBehaviour
{


    public MSoccerMono_IsDroneExistingOnClient m_drones;

    public LastValue[] m_dronesLastValue = new LastValue[12];


    private void Awake()
    {
        ResetArray();
    }

    private void Reset()
    {
        ResetArray();
    }

    private void ResetArray()
    {
        m_dronesLastValue = new LastValue[12];
        for (int i = 0; i < 12; i++)
        {
            m_dronesLastValue[i] = new LastValue();
            m_dronesLastValue[i].m_fixedDrone = (FixedSoccerId)i + 1;

        }
    }

    [System.Serializable]
    public class LastValue {

        public FixedSoccerId m_fixedDrone;
        public int m_index;
        public int m_value;
        public int m_drone20;
        public float m_horizontalRotation;
        public float m_donwUpMove;
        public float m_leftRightMove;
        public float m_backForwardMove;
    }


    public int m_lastIndex;
    public int m_lastValue;
    public int m_lastDrone20;

    public int[] m_ignoreValue = new int[] { 987654321, 123456789 };
    public void PushIn(int index, int value) {

        m_lastIndex = index;
        m_lastValue = value;
        m_lastDrone20 = 0;

        foreach (int ig in m_ignoreValue)
            if (ig == value) return;

        bool owned =MSoccerMono_IsDroneExistingOnClient.IsOwningOneDroneIntegerPlayer(index);
        if (owned == false) return;
        bool isDroneCmd = value > 99999999;
        if (!isDroneCmd) return;
        int drone20 = value / 100000000;

        MSoccerMono_IsDroneExistingOnClient.GetOwnedDroneIntegerPlayer(index, out List<FixedSoccerId> ids);
        if (ids ==null || ids.Count == 0) return;


        m_lastDrone20 = drone20;

        if (drone20 == 0) {
            SetGamepadFromDroneId(index,value, ids[0]);
        }
        if (drone20 > 0 && drone20 < 13) { 
        
            FixedSoccerId id = (FixedSoccerId)drone20;
            SetGamepadFromDroneId(index, value, id);

        }
        if (drone20 < 0) {

            int i = (int)(-drone20) - 1;
            if (i < ids.Count)
                SetGamepadFromDroneId(index, value, ids[i]);
        }
        
    }

    public void SetGamepadFromDroneId(int index, int cmd, FixedSoccerId drone)
    {
        ParserIntegerToDronePercentUtility.Unpack(
            cmd,
            out int drone20,
            out float rotateLeftRight,
            out float downUp,
            out float leftRight,
            out float backForward);

        MSoccerMono_IsDroneExistingOnClient.GetDroneSoccerIdGamepad(drone,
             out MSoccerMono_AbstractGamepad gamepad);
        SetPad(gamepad, rotateLeftRight, downUp, leftRight, backForward);

        int t = ((int)drone) - 1;
        LastValue lastValue = m_dronesLastValue[t];
        if (lastValue != null) 
        {
                lastValue.m_drone20 = drone20;
                lastValue.m_index= index;
                lastValue.m_value = cmd;
                lastValue.m_horizontalRotation = rotateLeftRight;
                lastValue.m_donwUpMove = downUp;
                lastValue.m_leftRightMove = leftRight;
                lastValue.m_backForwardMove = backForward;
                
        }
    }

    private void SetPad(MSoccerMono_AbstractGamepad gamepad, float rotateLeftRight, float downUp, float leftRight, float backForward)
    {
        gamepad.SetHorizontalRotation(rotateLeftRight);
        gamepad.SetVerticalMove(downUp);
        gamepad.SetHorizontaMove(leftRight);
        gamepad.SetFrontalMove(backForward);
    }
}
