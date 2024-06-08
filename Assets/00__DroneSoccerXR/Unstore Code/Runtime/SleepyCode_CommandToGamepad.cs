using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SleepyCode_CommandToGamepad : MonoBehaviour
{

    public MirrorMono_CallableUniqueAlias[] m_alias;

    public MSoccerMono_AbstractGamepad m_lastAffected;
    private void Awake()
    {
        m_alias = FindObjectsOfType<MirrorMono_CallableUniqueAlias>();
    }
    public void Push(DroneCommandWithTextAlias command)
    {
        foreach (var alias in m_alias)
        {
            foreach (var t in alias.m_textAlias) {
                if (t == command.m_aliasToAffect)
                {
                    var rsa= alias.GetComponent<MirrorMono_PublicRsaKeyOwner>();
                    var gamepad= alias.GetComponent<MSoccerMono_AbstractGamepad>();
                    if( gamepad!=null)
                    {
                        m_lastAffected = gamepad;
                        if (rsa.m_publicKeyOwner != command.m_playerRef.GetPublicKey())
                        {
                            return;
                        }
                        gamepad.SetHorizontalRotation (command.m_droneJoysticks.rotateHorizontal);
                        gamepad.SetVerticalMove ( command.m_droneJoysticks.downUp);
                        gamepad.SetHorizontaMove(  command.m_droneJoysticks.leftRight);
                        gamepad.SetFrontalMove (command.m_droneJoysticks.backForward);

                    }

                   
                }
            }
        }
    }
    public void Push(DroneCommandWithIntAlias command)
    {
        foreach (var alias in m_alias)
        {
            foreach (var t in alias.m_integerIdAlias)
            {
                if (t == command.m_aliasToAffect)
                {
                    var rsa = alias.GetComponent<MirrorMono_PublicRsaKeyOwner>();
                    var gamepad = alias.GetComponent<MSoccerMono_AbstractGamepad>();
                    if (gamepad != null)
                    {
                        m_lastAffected = gamepad;
                        if (rsa.m_publicKeyOwner != command.m_playerRef.GetPublicKey())
                        {
                            return;
                        }
                        gamepad.SetHorizontalRotation(command.m_droneJoysticks.rotateHorizontal);
                        gamepad.SetVerticalMove(command.m_droneJoysticks.downUp);
                        gamepad.SetHorizontaMove(command.m_droneJoysticks.leftRight);
                        gamepad.SetFrontalMove(command.m_droneJoysticks.backForward);

                    }


                }
            }
        }
    }
}
