using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MSoccerMono_PushMultiDroneCommands : NetworkBehaviour
{

    public MSoccerMono_RsaKeyIdentity m_rsaKeyIdentity;

    
    public void PushDroneCommands(string droneAlias, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        if (isOwned && isClient)
        {
            CmdPushDroneCommands(droneAlias, rotateHorizontal, downUp, leftRight, backForward);
        }
    }

    [Command]
    void CmdPushDroneCommands(string droneAlias, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        
    }
}


public class SMSoccerMono_PlayerCommands {

    public static SMSoccerMono_PlayerCommands InstanceInScene;

    public void Awake() { 
        InstanceInScene = this;
    }

}
