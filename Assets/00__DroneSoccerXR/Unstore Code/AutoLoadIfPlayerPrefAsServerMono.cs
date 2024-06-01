using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AutoLoadIfPlayerPrefAsServerMono : MonoBehaviour
{


    public UnityEvent m_onIsServer;
    public UnityEvent m_onIsClient;
    void Start()
    {
        if (PlayerPrefs.GetInt("IsAlwaysServer") == 1)
        {
            m_onIsServer.Invoke();
        }
        if (PlayerPrefs.GetInt("IsAlwaysClient") == 1)
        {
            m_onIsClient.Invoke();
        }
    }

    [ContextMenu("Auto Load Server")]
    public void SetAsAlwaysServer()
    {
        PlayerPrefs.SetInt("IsAlwaysServer", 1);
    }
    [ContextMenu("Auto Load Client")]
    public void SetAsAlwaysClient()
    {
        PlayerPrefs.SetInt("IsAlwaysClient", 1);
    }

    [ContextMenu("Remove Auto Load")]
    public void RemovePlayerPrefs()
    {
        PlayerPrefs.SetInt("IsAlwaysServer", 0);
        PlayerPrefs.SetInt("IsAlwaysClient", 0);
    }
   

}
