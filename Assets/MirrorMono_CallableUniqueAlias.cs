using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

/**
 
If you think that the alias name can change with time. You can use this class to tag the object with a unique alias name.
 */
public class MirrorMono_CallableUniqueAlias : NetworkBehaviour
{

    [SyncVar]
    public string[] m_textAlias;

    [SyncVar]
    public int[] m_integerIdAlias;


    [ServerCallback]
    [ContextMenu("Refresh")]
    public void Refresh() {

        string[] s = m_textAlias;
        int [] i = m_integerIdAlias;
        m_textAlias = new string[0];
        m_integerIdAlias = new int[0];
        m_textAlias = s;
        m_integerIdAlias = i;
    }
}




