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

}




