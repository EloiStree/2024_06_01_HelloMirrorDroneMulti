﻿using Mirror;
using System;
using System.Collections.Generic;
using System.Text;
using UnityEngine;


[System.Serializable]
public class MirrorRsaPlayerOnNetworkRef {
    [SerializeField] NetworkBehaviour m_linkedPlayer;
    [SerializeField] int m_playerNetworkId;
    [SerializeField] string m_publicXmlKey;
    [SerializeField] bool m_isOwnedByLocalComputer;

    public string GetPublicKey() {
        return m_publicXmlKey;
    }
    public int GetInstanceId() {
        return m_playerNetworkId;
    }

    public MirrorRsaPlayerOnNetworkRef(NetworkBehaviour playerNetworkId, string publicKey) {
        if (playerNetworkId == null)
        {
            Debug.LogWarning("You can't create a player with a null player network id.");
            return;
        }
        if (playerNetworkId.netId == 0)
        {
            Debug.LogWarning("You can't create a player with a netId of 0.");
            return;
        }
        if (string.IsNullOrEmpty(publicKey))
        {
            Debug.LogWarning("You can't create a player with a null or empty public key.");
            return;
        }
        m_linkedPlayer = playerNetworkId;
        m_playerNetworkId = (int)playerNetworkId.netId;
        m_publicXmlKey = publicKey;
        m_isOwnedByLocalComputer = playerNetworkId.isOwned;
        if (m_isOwnedByLocalComputer)
            MirrorRsaPlayerOnNetworkRefDico.SetCurrentPlayer(this);
        MirrorRsaPlayerOnNetworkRefDico.InstanceInScene.AddPlayerRef(this);
    }

    public NetworkBehaviour GetPlayer() {
        return m_linkedPlayer;
    }

    public bool IsPlayerNotValideAnymore() {
        return !IsPlayerStillValide();
    }
    public bool IsPlayerStillValide() {
        if(m_linkedPlayer==null)
            return false;
        return true;
    }

    public GameObject GetGameObject()
    {
        if(m_linkedPlayer==null)
            return null;
        return m_linkedPlayer.gameObject;
    }

}
