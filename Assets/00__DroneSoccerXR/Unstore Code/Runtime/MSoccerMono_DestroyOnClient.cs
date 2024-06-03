using Mirror;
using System.Collections;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_DestroyOnClient : MonoBehaviour
{
    public UnityEvent<bool> m_onIsServer;
    public UnityEvent m_onIsServerTrue;
    public UnityEvent m_onIsServerFalse;

    public MonoBehaviour[] m_toDestroyOnClientScript;
    public GameObject[] m_toDestroyOnClientObject;
    public bool m_isHostOrPlayerDefined = false;

    public IEnumerator Start()
    {
         m_isHostOrPlayerDefined = false;
        while(!m_isHostOrPlayerDefined)
        {
            yield return new WaitForSeconds(1);
            if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive)
            {
                m_isHostOrPlayerDefined = true;
            }
        }
        bool isServer = IsOnServer();
        if(isServer)
        {
            m_onIsServer.Invoke(isServer);
            if (isServer) { 
                m_onIsServerTrue.Invoke();
            }
            else { 
                m_onIsServerFalse.Invoke();
                foreach (var item in m_toDestroyOnClientScript)
                {
                    if (item != null)
                        Destroy(item);
                }
                foreach (var item in m_toDestroyOnClientObject)
                {
                    if(item!=null)
                        Destroy(item);
                }
            }


        }
    }

    public bool IsOnServer()
    {
        if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive && NetworkServer.active)
        {
            Debug.Log("This is the host server.");
            return true;
        }
        else
        {
            Debug.Log("This is a client not on host.");
            return false;
        }
    }
}