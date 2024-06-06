using UnityEngine;
using UnityEngine.Events;

public class SMSoccerMono_PlayerDroneCommands : MonoBehaviour{

    public static SMSoccerMono_PlayerDroneCommands InstanceInScene;


    public DroneCommandWithIntAlias m_lastCommandIntAlias;
    public DroneCommandWithTextAlias m_lastCommandTextAlias;

    public UnityEvent<DroneCommandWithIntAlias> m_onCcommandToDroneIntAlias;
    public UnityEvent<DroneCommandWithTextAlias> m_onCommandToDroneTextAlias;

    
    public static void Push(MirrorRsaPlayerOnNetworkRef mirrorRsaPlayerOnNetworkRef, string droneAlias, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        DroneCommandWithTextAlias c= new DroneCommandWithTextAlias() { 
        m_aliasToAffect = droneAlias,
        m_playerRef= mirrorRsaPlayerOnNetworkRef,
        m_droneJoysticks = new DroneCommandFourAxis() { 
            rotateHorizontal = rotateHorizontal,
            downUp = downUp,
            leftRight = leftRight,
            backForward = backForward
        }
        };
        InstanceInScene.m_lastCommandTextAlias = c;
        InstanceInScene.m_onCommandToDroneTextAlias.Invoke(c);
        
    }

    public static void Push(MirrorRsaPlayerOnNetworkRef mirrorRsaPlayerOnNetworkRef, int droneIntAlias, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        DroneCommandWithIntAlias c = new DroneCommandWithIntAlias()
        {
            m_aliasToAffect = droneIntAlias,
            m_playerRef = mirrorRsaPlayerOnNetworkRef,
            m_droneJoysticks = new DroneCommandFourAxis()
            {
                rotateHorizontal = rotateHorizontal,
                downUp = downUp,
                leftRight = leftRight,
                backForward = backForward
            }
        };
        InstanceInScene.m_lastCommandIntAlias = c;
        InstanceInScene.m_onCcommandToDroneIntAlias.Invoke(c);

    }
    public void Awake() { 
        InstanceInScene = this;
    }
}
[System.Serializable]
public struct DroneCommandWithTextAlias
{
    public MirrorRsaPlayerOnNetworkRef m_playerRef;
    public string m_aliasToAffect;
    public DroneCommandFourAxis m_droneJoysticks;
}
[System.Serializable]
public struct DroneCommandWithIntAlias
{
    public MirrorRsaPlayerOnNetworkRef m_playerRef;
    public int m_aliasToAffect;
    public DroneCommandFourAxis m_droneJoysticks;
}
[System.Serializable]
public struct DroneCommandFourAxis
{
    public float rotateHorizontal;
    public float downUp;
    public float leftRight;
    public float backForward;
}
