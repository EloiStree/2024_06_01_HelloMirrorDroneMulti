
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoLoadIfPlayerPrefAsServerMono : MonoBehaviour
{


    public UnityEvent m_onIsServer;
    public UnityEvent m_onIsClient;

    public UnityEvent m_onReconnect;
    private string m_isServer="IsServer";
    private string m_isClient="IsClient";
    void Start()
    {
        if (PlayerPrefs.GetInt(m_isServer) == 1)
        {
            m_onIsServer.Invoke();
        }
        else if (PlayerPrefs.GetInt(m_isClient) == 1)
        {
            m_onIsClient.Invoke();
        }
    }

    [ContextMenu("Auto Load Server")]
    public void SetAsAlwaysServer()
    {
        PlayerPrefs.SetInt(m_isServer, 1);
        PlayerPrefs.SetInt(m_isClient, 0);
    }
    [ContextMenu("Auto Load Client")]
    public void SetAsAlwaysClient()
    {
        PlayerPrefs.SetInt(m_isServer, 0);
        PlayerPrefs.SetInt(m_isClient, 1);
    }

    [ContextMenu("Remove Auto Load")]
    public void RemovePlayerPrefs()
    {
        PlayerPrefs.SetInt(m_isServer, 0);
        PlayerPrefs.SetInt(m_isClient, 0);
    }


    [ContextMenu("Try to reconnect")]
    public void TryToReconnect() {
        m_onReconnect.Invoke();
    }
}
