﻿using Codice.CM.Common.Tree.Partial;
using Mirror;
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
    public static bool IsOnServer()
    {
        if (NetworkManager.singleton != null && NetworkManager.singleton.isNetworkActive && NetworkServer.active)
        {
            return true;
        }
        else
        {
            return false;
        }
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