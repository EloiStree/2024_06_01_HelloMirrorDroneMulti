using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;
using Org.BouncyCastle.Bcpg;

public class MSoccerMono_PlayerInput : NetworkBehaviour
{

    public static MSoccerMono_PlayerInput PlayerInstanceInScene;
    [Header("Client Information")]
    public float m_rotateHorizontalPercent;
    public float m_moveDownUpPercent;
    public float m_moveLeftRightPercent;
    public float m_moveBackForwardPercent;
    public int m_intCmdServer;

    public float m_localRotateHorizontalPercent;
    public float m_localMoveDownUpPercent;
    public float m_localMoveLeftRightPercent;
    public float m_localMoveBackForwardPercent;
    public int m_intCmdLocal;


    //4 * 4 bytes 16
    //0-99
    //9988776600
    //2147483647

    public void SetLocalRotateHorizontalPercent(float value) { m_localRotateHorizontalPercent = value; }
    public void SetLocalMoveDownUpPercent(float value) { m_localMoveDownUpPercent=value; }
    public void SetLocalMoveLeftRightPercent(float value) { m_localMoveLeftRightPercent=value; }
    public void SetLocalMoveBackForwardPercent (float value) { m_localMoveBackForwardPercent = value; }



    public void PushCurrentLocalValueToServer() {
        if (!isOwned)
            return;
        PushCurrentJoystickValueToServer(m_localRotateHorizontalPercent, m_localMoveDownUpPercent, m_localMoveLeftRightPercent, m_localMoveBackForwardPercent);
    }

    public void PushCurrentJoystickValueToServer(float rotateHorizontalPercent,
        float moveDownUpPercent,
        float moveLeftRightPercent,
        float moveBackForwardPercent) {
        if (!isOwned)
            return;
        ParserIntegerToDronePercentUtility.Pack(out int cmd, 0, rotateHorizontalPercent, moveDownUpPercent, moveLeftRightPercent, moveBackForwardPercent);
      
        if (cmd == m_intCmdLocal)
        {
            return;
        }
        m_intCmdLocal = cmd;
        CmdPushInputPlayerToServer(cmd);
    }


    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        PlayerInstanceInScene = this;
    }

    [Command]
    void CmdPushInputPlayerToServer(

        int cmdInteger
        ) {

        if(cmdInteger== m_intCmdServer){
            return;
        }
        m_intCmdServer = cmdInteger;
        ParserIntegerToDronePercentUtility.Unpack(cmdInteger,
            out int targetDrone,
            out float rotateHorizontal,
            out float downUp,
            out float leftRight,
            out float backForward
            );
        m_rotateHorizontalPercent= rotateHorizontal;
        m_moveDownUpPercent= downUp;
        m_moveLeftRightPercent= leftRight;
        m_moveBackForwardPercent= backForward;
        m_onServerPlayerInputChanged.Invoke(cmdInteger);

    }

    public UnityEvent<int> m_onServerPlayerInputChanged;

}
