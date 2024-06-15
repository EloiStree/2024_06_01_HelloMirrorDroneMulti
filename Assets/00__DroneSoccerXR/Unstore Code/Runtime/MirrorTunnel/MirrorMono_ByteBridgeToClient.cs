using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class MirrorMono_ByteBridgeToClient : NetworkBehaviour
{

    public static MirrorMono_ByteBridgeToClient PlayerSingleton;
    public NetworkIdentity m_singletonIdentity;

    public static Action<byte[]> m_receivedOnClientFromServer;
    private void OverrideSingletonIfOwned()
    {
        if (isOwned)
        {
            PlayerSingleton = this;
            PlayerSingleton.m_singletonIdentity = GetComponent<NetworkIdentity>();
        }
    }

    public override void OnStartClient()
    {
        base.OnStartClient();
        OverrideSingletonIfOwned();
    }
    
    public byte[] m_lastReceived;
    public UnityEvent<byte[]> m_onByteEventReceived;

    public void PushByteEventToClient(byte[] bytes)
    {
        if (m_singletonIdentity!=null)
        {
            if(isServer)
               RpcPushByteEventToClient(bytes);
        }
    }
    public void PushByteFromStaticToClient(byte[] bytes)
    {
        if (PlayerSingleton!=null)
        {
            PlayerSingleton.PushByteEventToClient(bytes);
        }
    }

    [TargetRpc]
    private void RpcPushByteEventToClient(byte[] bytes)
    {
            m_lastReceived = bytes;
            m_onByteEventReceived.Invoke(bytes);
            m_receivedOnClientFromServer?.Invoke(bytes);
    }
}
