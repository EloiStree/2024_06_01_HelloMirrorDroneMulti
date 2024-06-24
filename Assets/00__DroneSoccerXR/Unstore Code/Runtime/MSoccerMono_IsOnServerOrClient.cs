using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using Mirror;
public class MSoccerMono_IsOnServerOrClient : MonoBehaviour
{

    public UnityEvent m_disableInWaitingOfDecision;
    public UnityEvent<bool> m_isOnServer;
    public UnityEvent<bool> m_isNotOnServer;
    public UnityEvent<bool> m_isHostOfGame;
    public UnityEvent<bool> m_isNotHostOfGame;

    public UnityEvent m_ifIsOnServer;
    public UnityEvent m_ifIsNotOnServer;
    public UnityEvent m_ifIsHostOfGame;
    public UnityEvent m_ifIsNotHostOfGame;


    public bool m_isDefinedYet;
    public bool m_isServer;
    public bool m_isHost;
    public void Awake()
    {
        m_disableInWaitingOfDecision.Invoke(); 
    }
    public IEnumerator Start()
    {
        m_isDefinedYet = false;

        while (!MSoccerMono_IsOnServerSingleton.IsHostOrClientDefined())
        {
            yield return new WaitForSeconds(0.1f);
        }

        while (!MSoccerMono_IsOnServerSingleton.HasPlayerInNetwork()) { 
        
            yield return new WaitForSeconds(0.1f);
        }


        m_isDefinedYet = true;
        m_isServer = MSoccerMono_IsOnServerSingleton.IsOnServer();
        m_isOnServer.Invoke(m_isServer);
        m_isNotOnServer.Invoke(!m_isServer);
        if (m_isServer) m_ifIsOnServer.Invoke();
        else m_ifIsNotOnServer.Invoke();

        while (!MirrorMono_RsaKeyIdentity.IsInstanciated())
        {
            yield return new WaitForSeconds(1f);

        } while (!MirrorMono_RsaKeyIdentity.IsInstanciatedAndConnected())
        {
            yield return new WaitForSeconds(1f);

        }
        m_isHost = MSoccerMono_IsOnServerSingleton.IsHostOfGame();
        m_isHostOfGame.Invoke(m_isHost);
        m_isNotHostOfGame.Invoke(!m_isHost);

        if (m_isHost) m_ifIsHostOfGame.Invoke();
        else  m_ifIsNotHostOfGame.Invoke();
    }



}
