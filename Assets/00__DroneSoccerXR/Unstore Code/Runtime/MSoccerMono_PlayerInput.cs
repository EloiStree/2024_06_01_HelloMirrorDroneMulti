using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MSoccerMono_PlayerInput : NetworkBehaviour
{

    public static MSoccerMono_PlayerInput PlayerInstanceInScene;

    [Header("Client Information")]
    public float m_rotateHorizontalPercent;
    public float m_moveDownUpPercent;
    public float m_moveLeftRightPercent;
    public float m_moveBackForwardPercent;


    public float m_localRotateHorizontalPercent;
    public float m_localMoveDownUpPercent;
    public float m_localMoveLeftRightPercent;
    public float m_localMoveBackForwardPercent;


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
        CmdPushInputPlayerToServer(rotateHorizontalPercent, moveDownUpPercent, moveLeftRightPercent, moveBackForwardPercent);
    }


    public override void OnStartLocalPlayer()
    {
        base.OnStartLocalPlayer();
        PlayerInstanceInScene = this;
        Debug.Log("Hello I am the local player", this.gameObject);
    }

    [Command]
    void CmdPushInputPlayerToServer(
        float rotateHorizontalPercent,
        float moveDownUpPercent,
        float moveLeftRightPercent,
        float moveBackForwardPercent
        ) {
        m_rotateHorizontalPercent= rotateHorizontalPercent;
        m_moveDownUpPercent= moveDownUpPercent;
        m_moveLeftRightPercent= moveLeftRightPercent;
        m_moveBackForwardPercent= moveBackForwardPercent;

    }
}
