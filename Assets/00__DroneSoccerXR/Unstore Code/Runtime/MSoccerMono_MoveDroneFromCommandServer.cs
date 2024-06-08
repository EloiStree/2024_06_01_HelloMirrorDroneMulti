using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_MoveDroneFromCommandServer : MonoBehaviour
{
    public bool m_trustMode = true;
    public MSoccerMono_IsDroneExistingOnClient m_existingDrones;


    private void Awake()
    {
        m_existingDrones = MSoccerMono_IsDroneExistingOnClient.InstanceInScene;
    }

    public void MoveDrone(DroneCommandPlayerSelection cmd) { 
    
        if (!MSoccerMono_IsOnServerSingleton.IsOnServer())
            return;

        string rsa = cmd.m_playerRef.GetPublicKey();
        MSoccerMono_AbstractGamepad gamepad = null;

        bool isDojoDronePresent = false;
        //SHOULD HAVE A DICO OF DOJO DRONES BASED ON NETWORK ID AND RSA
        //IF DOJO DRONE IS PRESENT , USE IT ELSE Check for SOCCER DRONE
        //MSoccerMono_DroneDojoTag.GetPlayerDojoDrone(rsa, out found, out  gamepad);
        //if(!found|| gamepad == null)
        //{
        //    Debug.Log("Create Dojo drone if allowed in dojo");
        //}
        if (isDojoDronePresent)
        {

        }
        else
        {
            MSoccerMono_IsDroneExistingOnClient.GetFirstOwnedSoccerId(rsa, out bool found, out FixedSoccerId id);
            if (found)
            {

                MSoccerMono_IsDroneExistingOnClient.GetDroneSoccerIdGamepad(id, out gamepad);
            }

        }
       

       
        if (gamepad == null)
            return;

        gamepad.SetHorizontalRotation(cmd.m_droneJoysticks.rotateHorizontal);
        gamepad.SetHorizontaMove(cmd.m_droneJoysticks.leftRight);
        gamepad.SetVerticalMove(cmd.m_droneJoysticks.downUp);
        gamepad.SetFrontalMove(cmd.m_droneJoysticks.backForward);
    }
    public void MoveDrone(DroneCommandWithFixedSoccerId cmd)
    { 
        if (!MSoccerMono_IsOnServerSingleton.IsOnServer())
            return;

        FixedSoccerId id = cmd.m_soccerIdToAffect;
        if (!m_trustMode)
        { 
            if(!MSoccerMono_IsDroneExistingOnClient.IsOwnerOfFixedSoccerDrone(
                cmd.m_playerRef.GetPublicKey(), 
                cmd.m_soccerIdToAffect))
            return;
        }

        MSoccerMono_IsDroneExistingOnClient.GetDroneSoccerIdGamepad(id, out MSoccerMono_AbstractGamepad gamepad);
        if(gamepad == null)
            return;

        gamepad.SetHorizontalRotation(cmd.m_droneJoysticks.rotateHorizontal);
        gamepad.SetHorizontaMove(cmd.m_droneJoysticks.leftRight);
        gamepad.SetVerticalMove(cmd.m_droneJoysticks.downUp);
        gamepad.SetFrontalMove(cmd.m_droneJoysticks.backForward);


    }
}
