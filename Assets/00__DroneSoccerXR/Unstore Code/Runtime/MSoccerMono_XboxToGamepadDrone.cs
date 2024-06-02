using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MSoccerMono_XboxToGamepadDrone : MonoBehaviour
{

    

    public MSoccerMono_InputRefAsGamepad m_inputRefAsGamepad;
    public MSoccerMono_PublicRSAToDrones m_rsaToDrones;
    public int m_integerUniqueIDTarget=-1;
    public bool m_affectAllDrones = false;

    [Header("Debug")]
    public MSoccerMono_AbstractGamepad m_gamepadToAffect;
    private int m_previousIntegerUniqueIDTarget = -1;
    void Start()
    {
        
    }

    void Update()
    {
        if(m_gamepadToAffect!=null)
        {
            if (m_inputRefAsGamepad != null)
            {
                if (m_affectAllDrones)
                {
                   m_rsaToDrones.GetAllGamepad(out List<MSoccerMono_AbstractGamepad> pads);
                    for (int i = 0; i < pads.Count; i++)
                    {
                        pads[i].SetJoystickLeftValue(m_inputRefAsGamepad.m_joystickLeftValue);
                        pads[i].SetJoystickRightValue(m_inputRefAsGamepad.m_joystickRightValue);
                    }
                }
                else
                {
                    m_gamepadToAffect.SetJoystickLeftValue(m_inputRefAsGamepad.m_joystickLeftValue);
                    m_gamepadToAffect.SetJoystickRightValue(m_inputRefAsGamepad.m_joystickRightValue);
                }
            }
        }
    }
    private void OnValidate()
    {
        if(m_integerUniqueIDTarget!=m_previousIntegerUniqueIDTarget)
        {
            m_previousIntegerUniqueIDTarget = m_integerUniqueIDTarget;
            if (m_rsaToDrones != null)
            {
                m_gamepadToAffect = m_rsaToDrones.GetGamepadByUniqueID(m_integerUniqueIDTarget);
            }
        }
    }
}
