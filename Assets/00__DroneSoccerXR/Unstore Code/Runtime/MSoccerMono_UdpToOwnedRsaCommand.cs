using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_UdpToOwnedRsaCommand : MonoBehaviour
{

    public MirrorRsaPlayerOnNetworkRef m_rsaKeyIdentityFound;
    public GameObject m_gameObjectFound;
    public MSoccerMono_ConvertByteAndTextCommandRsaClient m_clientToServerFound;

    public void Start()
    {
        InvokeRepeating("CheckForPlayer", 1, 3);

    }


    [ContextMenu("Push Random integer for test")]
    public void PushRandomIntegerForTest() { 
        int i = UnityEngine.Random.Range(0,2099999999);
        byte[] b = BitConverter.GetBytes(i);
        PushByteCommand(b);
    }

    public void PushTextCommand(string commandAsText)
    {
        CheckForPlayer();
        if (m_rsaKeyIdentityFound == null)
            return;
        if (m_rsaKeyIdentityFound.IsPlayerStillValide()) { 
            GameObject target= m_rsaKeyIdentityFound.GetGameObject();
            m_gameObjectFound = target;
            if (target != null) { 
                MSoccerMono_ConvertByteAndTextCommandRsaClient clientToServer = target.GetComponent<MSoccerMono_ConvertByteAndTextCommandRsaClient>();
                m_clientToServerFound = clientToServer;
                if (clientToServer!=null)
                    clientToServer.PushActionAsTextOnClient(commandAsText);
            }
        }
    }


    public void PushByteCommand(byte[] commandAsByte)
    {
        CheckForPlayer();
        if (m_rsaKeyIdentityFound == null)
            return;
        if (m_rsaKeyIdentityFound.IsPlayerStillValide())
        {
            GameObject target = m_rsaKeyIdentityFound.GetGameObject();
            m_gameObjectFound = target;
            if (target != null)
            {
                MSoccerMono_ConvertByteAndTextCommandRsaClient clientToServer = target.GetComponent<MSoccerMono_ConvertByteAndTextCommandRsaClient>();
                m_clientToServerFound = clientToServer;
                if (clientToServer != null) { 
                    clientToServer.PushActionAsByteOnClient(commandAsByte);
                }
            }
        }
    }

    private void CheckForPlayer()
    {
        if (m_rsaKeyIdentityFound == null || m_rsaKeyIdentityFound.IsPlayerNotValideAnymore())
            m_rsaKeyIdentityFound = MirrorRsaPlayerOnNetworkRefDico.GetCurrentPlayer();
    }
}
