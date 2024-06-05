using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Unity.VisualScripting;
using UnityEngine.Events;
public class MSoccerMono_EthereumAddressToUse : NetworkBehaviour
{

    [SyncVar(hook = nameof(RefreshAddressUsed))]

    public string m_ethereumAddressToUse = "";
    public UnityEvent<string> m_onEthereum;

    public int m_maxChanged = 2;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (m_ethereumAddressToUse.Length > 0)
            PushEthereumAddressToUse(m_ethereumAddressToUse);
    }

    public void PushEthereumAddressToUse(string ethereumAddress) {

        if (isOwned && isClient) { 
        
            CmdPushEthereumAddressToUse(ethereumAddress);
        }
    }

    [Command]
    private void CmdPushEthereumAddressToUse(string ethereumAddress) {

        if (ethereumAddress.Length > 70)
        {
            Debug.Log("Are you trying to hack me ? Ethereum Address are only 64 char max");
        }
        else {
            if (m_maxChanged > 0) {
                m_ethereumAddressToUse = ethereumAddress;
                m_onEthereum.Invoke(ethereumAddress);
                m_maxChanged--;
            }
        }
    }

    public void RefreshAddressUsed(string oldAddress, string newAddress) {

        m_onEthereum.Invoke(newAddress);
    }
}
