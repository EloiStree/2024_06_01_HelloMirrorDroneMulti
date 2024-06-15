using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Mirror.SimpleWeb;
using kcp2k;

public class MirrorMono_BasicNetworkManagerWrapper : MonoBehaviour
{
    public static MirrorMono_BasicNetworkManagerWrapper m_instanceInScene;
    public static MirrorMono_BasicNetworkManagerWrapper Instance
    {
        get
        {
            if (m_instanceInScene == null)
            {
                m_instanceInScene = FindObjectOfType<MirrorMono_BasicNetworkManagerWrapper>();
            }
            return m_instanceInScene;
        }
    }

    public Mirror.NetworkManager m_networkManager;
    public KcpTransport m_classicWebTransport;
    public SimpleWebTransport m_webGlTransport;

    private void Awake()
    {
        m_instanceInScene = this;
    }

    public void SetServerToTarget(string serverIP)
    {
        m_networkManager.networkAddress = serverIP;
    }
    public void StartAsClient()
    {
        m_networkManager.StartClient();
    }
    public void StartAsServer()
    {
        m_networkManager.StartServer();
    }

    /// <summary>
    ///  Not tested yet
    /// </summary>
    /// <param name="port"></param>
    public void SetWebGlPort(int port)
    {
        m_webGlTransport.port = (ushort)port;
    }
    /// <summary>
    ///  Not tested  yet
    /// </summary>
    /// <param name="port"></param>

    public void SetClassicWebPort(int port)
    {
        m_classicWebTransport.port = (ushort)port;
    }


}
