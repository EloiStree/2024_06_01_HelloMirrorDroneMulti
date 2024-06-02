using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_PlayerInputToDroneTello : MonoBehaviour
{

    public MSoccerMono_PlayerInput m_playerInput;
    public RCEasyMoveControllerLerpMono m_rcEasyDroneMove;

  
    void Update()
    {
        m_rcEasyDroneMove.SetHorizontalRotation(m_playerInput.m_rotateHorizontalPercent);
        m_rcEasyDroneMove.SetFrontalMove(m_playerInput.m_moveBackForwardPercent);
        m_rcEasyDroneMove.SetHorizontaMove(m_playerInput.m_moveLeftRightPercent);
        m_rcEasyDroneMove.SetVerticalMove(m_playerInput.m_moveDownUpPercent);
    }
}
