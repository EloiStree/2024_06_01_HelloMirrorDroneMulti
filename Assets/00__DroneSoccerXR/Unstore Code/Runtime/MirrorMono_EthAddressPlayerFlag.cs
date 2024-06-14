using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMono_EthAddressPlayerFlag : NetworkBehaviour
{
    [SyncVar]
    [Tooltip("Represent the ehtereum address the player is playing for as a flag of the team or association")]
    public string m_ethereumAddress0x;

    public void SetEtheumAddress(string etherAddress0x)
    {
        if (MSoccerMono_IsOnServerSingleton.IsOnServer() || !Application.isPlaying)
        {
            if ( etherAddress0x.Length != 42 || etherAddress0x.Length != 40)
            {
                return;
            }
            if (etherAddress0x.Length == 40) { 
                etherAddress0x = "0x" + etherAddress0x;
            }
            m_ethereumAddress0x = etherAddress0x;
        }
    }
}
