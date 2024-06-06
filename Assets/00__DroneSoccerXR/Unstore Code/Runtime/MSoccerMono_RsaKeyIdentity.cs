using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using System.Linq;



public class MSoccerMono_RsaKeyIdentity : NetworkBehaviour
{

    [SyncVar]
    public string m_publicXmlKey;

    [SyncVar(hook =nameof(NotifyPlayerProvedIdentityRight))]
    public bool m_isSignatureValid;

    public MirrorRsaPlayerOnNetworkRef m_playerRef;


    public UnityEvent<string> m_onPlayerProvedIdentity;

    public AbstractKeyPairRsaHolderMono m_clientFetchKey;


    public void NotifyPlayerProvedIdentityRight(bool _ ,bool newValue) {
        if (newValue) {
           // MirrorRsaPlayerOnNetworkRefDico.InstanceInScene.RemovePlayerNotValide();
            m_playerRef = new MirrorRsaPlayerOnNetworkRef(this, m_publicXmlKey);
            m_onPlayerProvedIdentity.Invoke(m_publicXmlKey);
        }
        else {
            m_playerRef = null;
        }
    }

    public Debug m_debug= new Debug();
    [System.Serializable]
    public class Debug {
        public string m_messageToSign;
        public byte[] m_messageToSignAsByte;
        public byte[] m_signedByte;
        public bool m_isOnHost;
        public bool m_isClientAndOwned;
    }
  

    public override void OnStartClient()
    {
        m_debug.m_isOnHost = isServer;
        m_debug.m_isClientAndOwned = isClient && isOwned;
        if (m_debug.m_isClientAndOwned)
        {
            ReloadThePublicKeyAndStartToSignIt();
        }
    }

    [ContextMenu("Reload the public key and sign it")]
    public void ReloadThePublicKeyAndStartToSignIt()
    {
        if (m_debug.m_isClientAndOwned) { 
        
            m_clientFetchKey.GetPublicXml(out string publicKey);
            CmdPushPublicKeyToServer(publicKey);
        }
    }

    [Command]
     void CmdPushPublicKeyToServer(string pubicKey)
    {
        m_publicXmlKey= pubicKey;
        m_debug.m_messageToSign = Guid.NewGuid().ToString();
        m_debug.m_messageToSignAsByte = System.Text.Encoding.UTF8.GetBytes(m_debug.m_messageToSign);
        RpcPushMessageToSign(m_debug.m_messageToSignAsByte);
    }

    [ServerCallback]
    [TargetRpc]
     void RpcPushMessageToSign(byte[] byteMessageToSign) {
        m_debug.m_messageToSignAsByte = byteMessageToSign;
        m_debug.m_messageToSign = System.Text.Encoding.UTF8.GetString(byteMessageToSign);
        print("Hello "+ m_debug.m_messageToSign);
        byte[] signedByte= KeyPairRsaHolderToSignMessageUtility.SignData(m_debug.m_messageToSignAsByte, m_clientFetchKey);
        m_debug.m_signedByte = signedByte;
        CmdPushSignedMessage( signedByte);
    }


    [Command]
     void CmdPushSignedMessage( byte[] signedMessage) {
        m_debug.m_signedByte = signedMessage;
        m_isSignatureValid = KeyPairRsaHolderToSignMessageUtility.VerifySignature(m_debug.m_messageToSignAsByte, m_debug.m_signedByte, m_publicXmlKey);
        
        if(m_isSignatureValid) {
            print("Signature is valid");
        } else {
            print("Signature is invalid.");
        }
    }

    public MirrorRsaPlayerOnNetworkRef GetRsaRef()
    {
        return m_playerRef;
    }
}
