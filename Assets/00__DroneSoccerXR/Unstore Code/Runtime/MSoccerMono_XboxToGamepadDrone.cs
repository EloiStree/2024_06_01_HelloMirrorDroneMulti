using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class MSoccerMono_XboxToGamepadDrone : MonoBehaviour
{

    

    public MSoccerMono_InputRefAsGamepad m_inputRefAsGamepad;
    public MSoccerMono_PublicRSAToDrones m_rsaToDrones;
    public FixedSoccerId m_integerUniqueIDTarget ;
    public bool m_affectAllDrones = false;

    [Header("Debug")]
    public MSoccerMono_AbstractGamepad m_gamepadToAffect;
    private int m_previousIntegerUniqueIDTarget = -1;
   

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
                    m_rsaToDrones.GetAllGamepad(out List<MSoccerMono_AbstractGamepad> pads);
                    for (int i = 0; i < pads.Count; i++)
                    {
                        MSoccerMono_FixedSoccerIdTag tag =pads[i].GetComponent<MSoccerMono_FixedSoccerIdTag>();
                        if (tag != null)
                        { 
                            FixedSoccerId id = tag.m_fixedSoccerId;
                            if (id == m_integerUniqueIDTarget)
                            {
                                m_gamepadToAffect = pads[i];
                                break;
                            }
                        }
                    }
                    m_gamepadToAffect.SetJoystickLeftValue(m_inputRefAsGamepad.m_joystickLeftValue);
                    m_gamepadToAffect.SetJoystickRightValue(m_inputRefAsGamepad.m_joystickRightValue);
                }
            }
        }
    }
    
}
