using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using System;

public class MirrorMono_PublicRsaKeyOwner : NetworkBehaviour
{
    public static Dictionary<string,List< MirrorMono_PublicRsaKeyOwner>> OwnerRegister = new Dictionary<string, List<MirrorMono_PublicRsaKeyOwner>>();

    [SyncVar(hook =nameof(OwnerChanged))]
    public string m_publicKeyOwner;

    private void OwnerChanged(string previousOwner, string newOwner)
    {
        if (newOwner != previousOwner) { 
        
            if(previousOwner != "" && previousOwner != null)
                RemoveOwnerClaim(this, previousOwner);
            if(newOwner != "" && newOwner != null)
                AddToOwner(this, newOwner);
        }
    }

    public void OnDestroy()
    {
        RemoveOwnerClaim(this,m_publicKeyOwner);
    }

    private void AddToOwner(MirrorMono_PublicRsaKeyOwner script, string newOwner)
    {
        if (!OwnerRegister.ContainsKey(newOwner))
        {
            OwnerRegister[newOwner] = new List<MirrorMono_PublicRsaKeyOwner>();
        }
        RemoveOwnerClaim(script, newOwner);
        OwnerRegister[newOwner].Add(script);
    }

    private void RemoveOwnerClaim(MirrorMono_PublicRsaKeyOwner script, string previousOwner)
    {
        if (!OwnerRegister.ContainsKey(previousOwner))
        {
            OwnerRegister[previousOwner] = new List<MirrorMono_PublicRsaKeyOwner>();
        }

        List<MirrorMono_PublicRsaKeyOwner> owned = OwnerRegister[previousOwner];
        for (int i= owned.Count-1; i>=0; i--)
        {
            if(owned[i] == script || owned[i]==null )
            {
                owned.RemoveAt(i);
            }
        }
        
    }

    [ServerCallback]
    [ContextMenu("Unclaim")]
    public void Unclaim()
    {

        m_publicKeyOwner = "";
    }

    [ServerCallback]
    public void Claim(string publicKeyOwner)
    {
        m_publicKeyOwner = publicKeyOwner;
    }
    [ServerCallback]
    [ContextMenu("Reclaim")]
    public void Reclaim()
    {
        string s = m_publicKeyOwner;
        m_publicKeyOwner = "";
        m_publicKeyOwner = s;
    }
}
