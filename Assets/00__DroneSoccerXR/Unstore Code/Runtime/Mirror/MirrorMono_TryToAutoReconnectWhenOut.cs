using Mirror.Examples.Basic;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorMono_TryToAutoReconnectWhenOut : MonoBehaviour
{
    public BasicNetManager m_connection;
    public float m_checkInterval = 1.0f;

    public bool m_isSupposeToBeClient = false;
    public bool m_isSupposeToBeServer = false;


    [ContextMenu("Is Client")]
    public void SetAsClient() { 
    
        m_isSupposeToBeClient=true;
    }
    [ContextMenu("Is Server")]
    public void SetAsServer() { 
    
        m_isSupposeToBeServer=true;
    }
    [ContextMenu("Is None")]
    public void SetToNotPreference() { 
        m_isSupposeToBeClient=false;
        m_isSupposeToBeServer=false;
    }

    public void Start()
    {
        StartCoroutine(CheckConnection());
    }
    public IEnumerator CheckConnection() {
        while (true) {

            if (m_isSupposeToBeServer &&  m_connection.numPlayers == 0) {
                m_connection.StartServer();
            }
            if (m_isSupposeToBeClient && m_connection.numPlayers == 0) {
                m_connection.StartClient();
            }
            yield return new WaitForSeconds(m_checkInterval);
        }
    }




}
