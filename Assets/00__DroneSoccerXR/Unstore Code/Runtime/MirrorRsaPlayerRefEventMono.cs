using UnityEngine;
using UnityEngine.Events;

public class MirrorRsaPlayerRefEventMono : MonoBehaviour{

    public UnityEvent<MirrorRsaPlayerOnNetworkRef> m_onNewRsaPlayer;
    public UnityEvent<int> m_onRemovedNetworkId;
    public UnityEvent<string> m_onRemovedRsaKey;


    public bool m_useDebug = false;
    public void Start()
    {
        MirrorRsaPlayerOnNetworkRefDico.InstanceInScene.m_onNewRsaPlayer += OnNewRsaPlayer;
        MirrorRsaPlayerOnNetworkRefDico.InstanceInScene.m_onRemovedNetworkId += OnRemovedNetworkId;
        MirrorRsaPlayerOnNetworkRefDico.InstanceInScene.m_onRemovedRsaKey += OnRemovedRsaKey;

               
    }
    
    private void OnRemovedRsaKey(string publicRsaKey)
    {
        if (m_useDebug)
        {
            Debug.Log("Removed Rsa Key: " + publicRsaKey);
        }
        m_onRemovedRsaKey.Invoke(publicRsaKey);
    }

    private void OnRemovedNetworkId(int playerNetworkId)
    {
        if (m_useDebug)
        {
            Debug.Log("Removed Network Id: " + playerNetworkId);
        }

        m_onRemovedNetworkId.Invoke(playerNetworkId);
    }

    private void OnNewRsaPlayer(MirrorRsaPlayerOnNetworkRef refPlayer)
    {
        if (m_useDebug)
        {
            Debug.Log("New Rsa Player: " + refPlayer.GetPublicKey());
        }

        m_onNewRsaPlayer.Invoke(refPlayer);
    }
}
