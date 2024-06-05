using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;
using UnityEngine.Events;
public class MSoccerMono_EthereumAddressToUse : NetworkBehaviour
{

    public string m_ethereumAddressToUse = "";
    public UnityEvent<string> m_onEthereum;

    public void PushEthereumAddressToUse(string ethereumAddress) {

        if (isOwned && isClient && ethereumAddress.Length==64) { 
        
            CmdPushEthereumAddressToUse(ethereumAddress);
        }
    }

    [Command]
    public void CmdPushEthereumAddressToUse(string ethereumAddress) {

        if (ethereumAddress.Length > 64)
        {
            Debug.Log("Are you trying to hack me ? Ethereum Address are only 64 char max");
        }
        else { 
            m_ethereumAddressToUse = ethereumAddress;
            m_onEthereum.Invoke(ethereumAddress);
        }
    }
}
