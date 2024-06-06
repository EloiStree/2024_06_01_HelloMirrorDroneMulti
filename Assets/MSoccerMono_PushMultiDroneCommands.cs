using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.Events;

public class MSoccerMono_PushMultiDroneCommands : NetworkBehaviour
{

    public static MSoccerMono_PushMultiDroneCommands InstanceInScene;
    public MSoccerMono_RsaKeyIdentity m_rsaKeyIdentity;



    public override void OnStartClient()
    {
        base.OnStartClient();
        if (this.isOwned && this.isClient) { 
            InstanceInScene = this; 
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
    private bool CanSendInfoToServer()
    {
        return this.isOwned && this.isClient && m_rsaKeyIdentity != null && m_rsaKeyIdentity.m_isSignatureValid;
    }
    [Command]
    void CmdPushDroneCommandsToDroneAlias(string droneAlias, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        SMSoccerMono_PlayerDroneCommands.Push(m_rsaKeyIdentity.GetRsaRef(), droneAlias, rotateHorizontal, downUp, leftRight, backForward);
    }
    [Command]
    void CmdPushDroneCommandsToIntAlias(int droneIntAlias, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {

        SMSoccerMono_PlayerDroneCommands.Push(m_rsaKeyIdentity.GetRsaRef(), droneIntAlias, rotateHorizontal, downUp, leftRight, backForward);
    }
}

