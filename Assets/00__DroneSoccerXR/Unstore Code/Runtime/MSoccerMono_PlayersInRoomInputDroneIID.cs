using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MSoccerMono_PlayersInRoomInputDroneIID : MonoBehaviour
{


    public MSoccerMono_IsDroneExistingOnClient m_drones;

    public LastValue[] m_dronesLastValue = new LastValue[12];
    public PlayerInfo[] m_players = new PlayerInfo[12];
    [System.Serializable]
    public class PlayerInfo {
        public int m_index;
        public FixedSoccerId[] m_ownedDrone;
        public MSoccerMono_AbstractGamepad [] m_gamepad;
    }

    private void Awake()
    {
        ResetArray();
        InvokeRepeating("RefreshPlayerBase", 0, 1);
    }

    private void Reset()
    {
        ResetArray();
    }


    public void RefreshPlayerBase()
    {

        int[] arrayIndex = MSoccerMono_IsDroneExistingOnClient.InstanceInScene.GetDroneIndexClaim0to11();
        for (int i = 0; i < arrayIndex.Length; i++)
        {
            if (i < m_dronesLastValue.Length)
            {
                
                m_dronesLastValue[i].m_index = arrayIndex[i];
                m_players[i].m_index = arrayIndex[i];
                m_players[i].m_ownedDrone = MSoccerMono_IsDroneExistingOnClient
                    .InstanceInScene.GetDroneOwnedFromIndex(arrayIndex[i]);
                m_players[i].m_gamepad = MSoccerMono_IsDroneExistingOnClient
                    .InstanceInScene.GetGamepadFromFixedSoccerId(m_players[i].m_ownedDrone);
            }
        }
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

        if (m_players == null || m_players.Length == 0)
            RefreshPlayerBase();
        m_lastIndex = index;
        m_lastValue = value;
        m_lastDrone20 = 0;

        
        

        foreach (int ig in m_ignoreValue)
            if (ig == value) return;

        bool owned = m_players.Any(x => x.m_index == index);
        if (owned == false) return;

        PlayerInfo player = m_players.First(x => x.m_index == index);
        int drone20 = value / 100000000;
        m_lastDrone20 = drone20;
        
        //bool isDroneCmd = value > 99999999;
        //if (!isDroneCmd) return;

        

        if (player.m_ownedDrone == null || player.m_ownedDrone.Length == 0) return;

        if (value == 0) {

            foreach (var i in player.m_gamepad) { 
                i.SetJoystickLeftValue(Vector3.zero);
                i.SetJoystickRightValue(Vector3.zero);
            }

               
        }
        else if (drone20 == 0)
        {
            SetGamepadFromDroneId(index, value, player.m_ownedDrone[0]);
        }
        else if (drone20 == -20 || drone20 == 20)
        {
            for (int i = 0; i < player.m_ownedDrone.Length; i++)
            {
                SetGamepadFromDroneId(index, value, player.m_ownedDrone[i]);
            }
        }
        else if (drone20 > 0 && drone20 < 13)
        {

            FixedSoccerId id = (FixedSoccerId)drone20;
            SetGamepadFromDroneId(index, value, id);
        }
        else if (drone20 < 0)
        {

            int i = (int)(-drone20) - 1;
            if (i < player.m_ownedDrone.Length)
                SetGamepadFromDroneId(index, value, player.m_ownedDrone[i]);
        }
        
    }

    private LastValue GetLastValueRef(int index)
    {
        for(int i = 0; i < m_dronesLastValue.Length; i++)
        {
            if (m_dronesLastValue[i].m_index == index)
                return m_dronesLastValue[i];
        }
        return null;
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
