using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MirrorMono_IndexIntegerOwner : NetworkBehaviour
{
    public static Dictionary<int, List<MirrorMono_IndexIntegerOwner>> OwnerRegister = new Dictionary<int, List<MirrorMono_IndexIntegerOwner>>();

    [SyncVar(hook = nameof(OwnerChanged))]
    public int m_indexOfUser;

    private void OwnerChanged(int previousOwner, int newOwner)
    {
        if (newOwner != previousOwner)
        {

            if (previousOwner != 0 )
                RemoveOwnerClaim(this, previousOwner);
            if (newOwner != 0)
                AddToOwner(this, newOwner);
        }
    }

    public void OnDestroy()
    {
        RemoveOwnerClaim(this, m_indexOfUser);
    }

    private void AddToOwner(MirrorMono_IndexIntegerOwner script, int newOwner)
    {
        if (!OwnerRegister.ContainsKey(newOwner))
        {
            OwnerRegister[newOwner] = new List<MirrorMono_IndexIntegerOwner>();
        }
        RemoveOwnerClaim(script, newOwner);
        OwnerRegister[newOwner].Add(script);
    }

    private void RemoveOwnerClaim(MirrorMono_IndexIntegerOwner script, int previousOwner)
    {
        if (!OwnerRegister.ContainsKey(previousOwner))
        {
            OwnerRegister[previousOwner] = new List<MirrorMono_IndexIntegerOwner>();
        }

        List<MirrorMono_IndexIntegerOwner> owned = OwnerRegister[previousOwner];
        for (int i = owned.Count - 1; i >= 0; i--)
        {
            if (owned[i] == script || owned[i] == null)
            {
                owned.RemoveAt(i);
            }
        }

    }

    public int GetOwner()
    {
        return m_indexOfUser;
    }

    public bool IsOwnedBy(int index) { 
        return m_indexOfUser == index;
    }

    [ServerCallback]
    [ContextMenu("Unclaim")]
    public void Unclaim()
    {

        m_indexOfUser = 0;
    }

    [ServerCallback]
    public void Claim(int publicKeyOwner)
    {
        m_indexOfUser = 0;
    }
    [ServerCallback]
    [ContextMenu("Reclaim")]
    public void Reclaim()
    {
        int s = m_indexOfUser;
        m_indexOfUser = 0;
        m_indexOfUser = s;
    }
}
