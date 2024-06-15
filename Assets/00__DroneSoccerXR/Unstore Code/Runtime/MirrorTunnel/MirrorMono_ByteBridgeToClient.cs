using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;

public class MirrorMono_ByteBridgeToClient : NetworkBehaviour
{

    public static MirrorMono_ByteBridgeToClient PlayerSingleton;
    public static List<MirrorMono_ByteBridgeToClient> m_singletonIdentities= new List<MirrorMono_ByteBridgeToClient>();

    public static Action<byte[]> m_receivedOnClientFromServer;
    private void OverrideSingletonIfOwned()
    {
        if (isOwned)
        {
            PlayerSingleton = this;
        }
            m_singletonIdentities.Add(this);
    }

    public override void OnStopClient()
    {
        base.OnStopClient();
        m_singletonIdentities.Remove(this);
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
        if (isServer) {
                foreach (var item in m_singletonIdentities) {
                if (item != null) { 
                    item.RpcPushByteEventToClient(bytes);
                }        
            }
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
