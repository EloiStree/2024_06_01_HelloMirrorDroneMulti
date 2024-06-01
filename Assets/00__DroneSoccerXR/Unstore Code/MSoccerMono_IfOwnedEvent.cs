using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class MSoccerMono_IfOwnedEvent : NetworkBehaviour
{
    public UnityEvent m_ifOwned;
    public UnityEvent m_ifNotOwned;
    public UnityEvent<bool> m_isOwned;
    public bool m_debugIsOwned;

    public override void OnStartClient() {

        m_debugIsOwned = isOwned;
        if (isOwned) m_ifOwned.Invoke();
        if (!isOwned) m_ifNotOwned.Invoke();
        m_isOwned.Invoke(isOwned);


    }
}
