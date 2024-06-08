using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;
using UnityEngine.Events;
using Org.BouncyCastle.Bcpg;

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
            MSoccerMono_IsDroneExistingOnClient.TryGetDroneSoccerIdFromAlias(droneAlias,out bool found,  out FixedSoccerId droneSoccer);
            if (!found)
            {
                ParserIntegerToDronePercentUtility.Pack(out int cmd, 0, rotateHorizontal, downUp, leftRight, backForward);
                CmdPushDroneCommandsToDroneAlias(droneAlias, cmd);
            }
            else
            {
                ParserIntegerToDronePercentUtility.Pack(out int cmd, (int)droneSoccer, rotateHorizontal, downUp, leftRight, backForward);
                CmdPushDroneCommandsToFixedSoccerIntId( cmd);
            }
        }
    }
    
    public void PushDroneCommandsToFixedSoccerIntIds(FixedSoccerId fixedDroneId1To12, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        if (CanSendInfoToServer())
        {
            ParserIntegerToDronePercentUtility.Pack(out int cmd ,(int)fixedDroneId1To12, rotateHorizontal,  downUp, leftRight, backForward);
            CmdPushDroneCommandsToFixedSoccerIntId(cmd);
        }
    }
    private bool CanSendInfoToServer()
    {
        return this.isOwned && this.isClient && m_rsaKeyIdentity != null && m_rsaKeyIdentity.IsSignedValidatedByServer();
    }
    [Command]
    void CmdPushDroneCommandsToDroneAlias(string droneAlias,int cmdInteger)
    {
        ParserIntegerToDronePercentUtility.Unpack(cmdInteger,
            out int targetDrone,
            out float rotateHorizontal,
            out float downUp,
            out float leftRight,
            out float backForward
            );
        if (m_rsaKeyIdentity!=null && m_rsaKeyIdentity.IsSignedValidatedByServer())
            SMSoccerMono_PlayerDroneCommandsCallManager.Push(m_rsaKeyIdentity.GetRsaRef(), droneAlias, rotateHorizontal, downUp, leftRight, backForward);
    }
   
    [Command]
    void CmdPushDroneCommandsToFixedSoccerIntId(int cmdInteger)
    {
        ParserIntegerToDronePercentUtility.Unpack(cmdInteger,
           out int targetDrone,
           out float rotateHorizontal,
           out float downUp,
           out float leftRight,
           out float backForward
           );
        if (m_rsaKeyIdentity != null && m_rsaKeyIdentity.IsSignedValidatedByServer())
            SMSoccerMono_PlayerDroneCommandsCallManager.Push(m_rsaKeyIdentity.GetRsaRef(), (FixedSoccerId)targetDrone, rotateHorizontal, downUp, leftRight, backForward);
    }



    [ServerCallback]
    public void OnServerPushPlayerInputIntegerToManager(int integerCommand)
    {

        ParserIntegerToDronePercentUtility.Unpack(integerCommand,
            out int targetDrone,
            out float rotateHorizontal,
            out float downUp,
            out float leftRight,
            out float backForward
            );
        if (m_rsaKeyIdentity != null && m_rsaKeyIdentity.IsSignedValidatedByServer())
            SMSoccerMono_PlayerDroneCommandsCallManager.PushPlayerSelection(m_rsaKeyIdentity.GetRsaRef(), rotateHorizontal, downUp, leftRight, backForward);

    }

    public void PushDroneCommandsToFocusDrone(float joystickLeftXPercent, float joystickLeftYPercent, float joystickRightXPercent, float joystickRightYPercent)
    {
        ParserIntegerToDronePercentUtility.Pack(out int cmd, 0, joystickLeftXPercent, joystickLeftYPercent, joystickRightXPercent, joystickRightYPercent);
        CmdPushDroneCommandsToFocusDrone(cmd);
    }
    public void PushDroneCommandsToFocusDrone(int commandInteger)
    {
        CmdPushDroneCommandsToFocusDrone(commandInteger);
    }

    [Command]
    void CmdPushDroneCommandsToFocusDrone(int cmdInteger)
    {
        OnServerPushPlayerInputIntegerToManager(cmdInteger);


    }
}

