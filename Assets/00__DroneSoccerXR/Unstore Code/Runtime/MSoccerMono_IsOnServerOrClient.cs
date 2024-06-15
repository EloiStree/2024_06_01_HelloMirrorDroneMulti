using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_IsOnServerOrClient : MonoBehaviour
{

    public UnityEvent m_disableInWaitingOfDecision;

    public UnityEvent<bool> m_isOnClient;
    public UnityEvent<bool> m_isOnServer;


    public void Awake()
    {
        m_disableInWaitingOfDecision.Invoke(); 
    
    }
    public IEnumerator Start()
    {

        while(!MSoccerMono_IsOnServerSingleton.IsHostOrClientDefined())
        {
            yield return new WaitForSeconds(1f);
        }
        bool isOnServer = MSoccerMono_IsOnServerSingleton.IsOnServer();
        m_isOnServer.Invoke(isOnServer);
        m_isOnClient.Invoke(!isOnServer);
    }

}
