using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_IsOnServerOrClient : MonoBehaviour
{

    public UnityEvent m_disableInWaitingOfDecision;

    public UnityEvent<bool> m_isNotOnServer;
    public UnityEvent<bool> m_isOnServer;
    public UnityEvent<bool> m_isHostOfGame;
    public UnityEvent<bool> m_isNotHostOfGame;


    public bool m_isDefinedYet;
    public bool m_isServer;
    public bool m_isHostofGame;
    public void Awake()
    {
        m_disableInWaitingOfDecision.Invoke(); 
    
    }
    public IEnumerator Start()
    {
        m_isDefinedYet = false;

        while (!MSoccerMono_IsOnServerSingleton.IsHostOrClientDefined())
        {
            yield return new WaitForSeconds(1f);
        }
        m_isDefinedYet = true;
        m_isServer = MSoccerMono_IsOnServerSingleton.IsOnServer();
        m_isOnServer.Invoke(m_isServer);
        m_isNotOnServer.Invoke(!m_isServer);

        while (!MirrorMono_RsaKeyIdentity.IsInstanciated())
        {
            yield return new WaitForSeconds(1f);

        } while (!MirrorMono_RsaKeyIdentity.IsInstanciatedAndConnected())
        {
            yield return new WaitForSeconds(1f);

        }
        m_isHostofGame = MSoccerMono_IsOnServerSingleton.IsHostOfGame();
        m_isHostOfGame.Invoke(m_isHostofGame);
        m_isNotHostOfGame.Invoke(!m_isHostofGame);
    }

}
