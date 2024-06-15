using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_LinkedStartPoint : MonoBehaviour
{
    public Transform m_whatToMove;
    public Transform m_startPoint;

    public void Reset()
    {
        m_whatToMove = transform;
        m_startPoint = transform.parent;
    }

    [ContextMenu("Teleport to start point")]
    public void TeleportToStartPoint()
    {
        m_whatToMove.position = m_startPoint.position;
        m_whatToMove.rotation = m_startPoint.rotation;
    }
}
