using Mirror;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MSoccerMono_RsaKeyIdentity : NetworkBehaviour
{

    [SyncVar]
    public string m_publicXmlKey;

    [SyncVar]
    public bool m_isSignatureValid;

    public AbstractKeyPairRsaHolderMono m_clientFetchKey;


    public void Awake()
    {
       
    }

    public string m_messageToSign;
    public byte[] m_messageToSignAsByte;
    public byte[] m_signedByte;


    public bool m_isOnHost;
    public bool m_isClientAndOwned;
    public override void OnStartClient()
    {
        m_isOnHost= isServer;
        m_isClientAndOwned = isClient && isOwned;
        if (m_isClientAndOwned) {
            m_clientFetchKey.GetPublicXml(out string publicKey);
            CmdPushPublicKeyToServer(publicKey);
        }
    }

    [Command]
    public void CmdPushPublicKeyToServer(string pubicKey)
    {
        m_publicXmlKey= pubicKey;
        m_messageToSign = Guid.NewGuid().ToString();
        m_messageToSignAsByte = System.Text.Encoding.UTF8.GetBytes(m_messageToSign);
        RpcPushMessageToSign(m_messageToSignAsByte);
    }

    [ServerCallback]
    [TargetRpc]
    public void RpcPushMessageToSign(byte[] byteMessageToSign) {
        m_messageToSignAsByte = byteMessageToSign;
        m_messageToSign = System.Text.Encoding.UTF8.GetString(byteMessageToSign);
        print("Hello "+ m_messageToSign);
        byte[] signedByte= KeyPairRsaHolderToSignMessageUtility.SignData(m_messageToSignAsByte, m_clientFetchKey);
        m_signedByte = signedByte;
        CmdPushSignedMessage( signedByte);
    }


    [Command]
    public void CmdPushSignedMessage( byte[] signedMessage) {
        m_signedByte = signedMessage;
        m_isSignatureValid = KeyPairRsaHolderToSignMessageUtility.VerifySignature(m_messageToSignAsByte, m_signedByte, m_publicXmlKey);
        
        if(m_isSignatureValid) {
            print("Signature is valid");
        } else {
            print("Signature is invalid.");
        }
    }

}
