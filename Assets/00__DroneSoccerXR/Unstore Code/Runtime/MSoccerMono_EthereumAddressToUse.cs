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
    public string m_pushAddressWhenConnected;
    public UnityEvent<string> m_onEthereumChanged;

    public int m_maxChanged = 2;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (m_pushAddressWhenConnected.Length > 0)
            PushEthereumAddressToUse(m_pushAddressWhenConnected);
    }

    public void PushEthereumAddressToUse(string ethereumAddress) {
        m_pushAddressWhenConnected = ethereumAddress;
        if (isOwned && isClient) { 
        
            CmdPushEthereumAddressToUse(m_pushAddressWhenConnected);
        }
    }

    [Command]
    private void CmdPushEthereumAddressToUse(string ethereumAddress) {

        if (ethereumAddress.Length > 70)
        {
            Debug.Log("Are you trying to hack me ? Ethereum Address are only 64 char max");
        }
        else {
            Debug.Log("Test");
            if (m_maxChanged > 0) {
                m_ethereumAddressToUse = ethereumAddress;
                m_onEthereumChanged.Invoke(ethereumAddress);
                m_maxChanged--;
            }
        }
    }

    public void RefreshAddressUsed(string oldAddress, string newAddress) {

        m_onEthereumChanged.Invoke(newAddress);
    }
}
