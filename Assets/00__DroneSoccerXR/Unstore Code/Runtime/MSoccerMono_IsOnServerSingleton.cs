using Codice.CM.Common.Tree.Partial;
using Mirror;
using UnityEditorInternal;
using UnityEngine;

public class MSoccerMono_IsOnServerSingleton: MonoBehaviour
{
    public bool m_areWeOnServer = false;

    private void Awake()
    {
        m_areWeOnServer = IsOnServer();
    }
    public void Start()
    {
        InvokeRepeating("AreWeOnServerUpdate", 0, 1);
    }

    private void AreWeOnServerUpdate() {

        m_areWeOnServer = IsOnServer();
    }

    public static bool IsNetworkActive() {
        return NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive;
    }
    public static bool HasPlayerInNetwork() {
        return NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive && NetworkManager.singleton.numPlayers > 0;
    }
    public static int GetPlayerActiveCount() {
        if(HasPlayerInNetwork()) return NetworkManager.singleton.numPlayers;
        else return 0;
    }
    public static bool IsOnServer()
    {
        return NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive && NetworkServer.active;
    }
    public static bool IsHostOfGame() {

        if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive
           ) {
            MirrorMono_RsaKeyIdentity.GetOwnedMirrorIdentity(out NetworkIdentity id );
            return id != null && id.isClient && id.isServer;
        }
        return false;
    }
    public static bool IsHostOrClientDefined() { 
    
        if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}