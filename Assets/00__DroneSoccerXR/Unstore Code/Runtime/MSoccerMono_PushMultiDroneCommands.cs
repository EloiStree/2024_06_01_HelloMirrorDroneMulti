using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.Events;

public class MSoccerMono_PushMultiDroneCommands : NetworkBehaviour
{

    public MSoccerMono_PlayerInput m_playerInputDefault;
    public static MSoccerMono_PushMultiDroneCommands OwnedPusherInScene;
    public MirrorMono_RsaKeyIdentity m_rsaKeyIdentity;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (this.isOwned && this.isClient) { 
            OwnedPusherInScene = this; 
        }
    }

    public void PushDroneCommandsToDroneAlias(string droneAlias, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        if (CanSendInfoToServer())
        {
            CmdPushDroneCommandsToDroneAlias(droneAlias, rotateHorizontal, downUp, leftRight, backForward);
        }
    }
    public void PushDroneCommandsToIntAlias(int droneAlias, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        if (CanSendInfoToServer())
        {
            CmdPushDroneCommandsToIntAlias(droneAlias, rotateHorizontal, downUp, leftRight, backForward);
        }
    }

    public void PushDroneCommandsToFixedSoccerIntIds(FixedSoccerId fixedDroneId1To12, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        if (CanSendInfoToServer())
        {
            CmdPushDroneCommandsToFixedSoccerIntId((int)fixedDroneId1To12, rotateHorizontal, downUp, leftRight, backForward);
        }
    }
    private bool CanSendInfoToServer()
    {
        return this.isOwned && this.isClient && m_rsaKeyIdentity != null && m_rsaKeyIdentity.IsSignedValidatedByServer();
    }
    [Command]
    void CmdPushDroneCommandsToDroneAlias(string droneAlias, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        if(m_rsaKeyIdentity!=null && m_rsaKeyIdentity.IsSignedValidatedByServer())
            SMSoccerMono_PlayerDroneCommandsCallManager.Push(m_rsaKeyIdentity.GetRsaRef(), droneAlias, rotateHorizontal, downUp, leftRight, backForward);
    }
    [Command]
    void CmdPushDroneCommandsToIntAlias(int droneIntAlias, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {

        if (m_rsaKeyIdentity != null && m_rsaKeyIdentity.IsSignedValidatedByServer())
            SMSoccerMono_PlayerDroneCommandsCallManager.Push(m_rsaKeyIdentity.GetRsaRef(), droneIntAlias, rotateHorizontal, downUp, leftRight, backForward);
    }
    [Command]
    void CmdPushDroneCommandsToFixedSoccerIntId(int droneFixedSoccerId, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {

        if (m_rsaKeyIdentity != null && m_rsaKeyIdentity.IsSignedValidatedByServer())
            SMSoccerMono_PlayerDroneCommandsCallManager.Push(m_rsaKeyIdentity.GetRsaRef(), (FixedSoccerId)droneFixedSoccerId, rotateHorizontal, downUp, leftRight, backForward);
    }

    public void PushDroneCommandsToFocusDrone( float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        m_playerInputDefault.SetLocalRotateHorizontalPercent(rotateHorizontal);
        m_playerInputDefault.SetLocalMoveLeftRightPercent(leftRight);
        m_playerInputDefault.SetLocalMoveDownUpPercent(downUp);
        m_playerInputDefault.SetLocalMoveBackForwardPercent(backForward);
    }
}

