using UnityEngine;
using UnityEngine.Events;

public class SMSoccerMono_PlayerDroneCommandsCallManager : MonoBehaviour{

    public static SMSoccerMono_PlayerDroneCommandsCallManager InstanceInScene;


    public DroneCommandWithIntAlias m_lastCommandIntAlias;
    public DroneCommandWithTextAlias m_lastCommandTextAlias;
    public DroneCommandWithFixedSoccerId m_lastCommandFixedSoccerId;

    public UnityEvent<DroneCommandWithIntAlias> m_onCcommandToDroneIntAlias;
    public UnityEvent<DroneCommandWithTextAlias> m_onCommandToDroneTextAlias;
    public UnityEvent<DroneCommandWithFixedSoccerId> m_onCommandToDroneFixedSoccerId;


    public static void Push(MirrorRsaPlayerOnNetworkRef mirrorRsaPlayerOnNetworkRef, string droneAlias, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        if (!MSoccerMono_IsOnServerSingleton.IsOnServer())
            return;
        DroneCommandWithTextAlias c = new DroneCommandWithTextAlias()
        {
            m_aliasToAffect = droneAlias,
            m_playerRef = mirrorRsaPlayerOnNetworkRef,
            m_droneJoysticks = new DroneCommandFourAxis()
            {
                rotateHorizontal = rotateHorizontal,
                downUp = downUp,
                leftRight = leftRight,
                backForward = backForward
            }
        };
        InstanceInScene.m_lastCommandTextAlias = c;
        InstanceInScene.m_onCommandToDroneTextAlias.Invoke(c);

    }

    public static void Push(MirrorRsaPlayerOnNetworkRef mirrorRsaPlayerOnNetworkRef, FixedSoccerId droneSoccerId, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {

        if (!MSoccerMono_IsOnServerSingleton.IsOnServer())
            return;
        DroneCommandWithFixedSoccerId c = new DroneCommandWithFixedSoccerId()
        {
            m_soccerIdToAffect = droneSoccerId,
            m_playerRef = mirrorRsaPlayerOnNetworkRef,
            m_droneJoysticks = new DroneCommandFourAxis()
            {
                rotateHorizontal = rotateHorizontal,
                downUp = downUp,
                leftRight = leftRight,
                backForward = backForward
            }
        };
        InstanceInScene.m_lastCommandFixedSoccerId = c;
        InstanceInScene.m_onCommandToDroneFixedSoccerId.Invoke(c);
    }

    public static void Push(MirrorRsaPlayerOnNetworkRef mirrorRsaPlayerOnNetworkRef, int droneIntAlias, float rotateHorizontal, float downUp, float leftRight, float backForward)
    {
        if (!MSoccerMono_IsOnServerSingleton.IsOnServer())
            return;
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
public struct DroneCommandWithFixedSoccerId
{
    public MirrorRsaPlayerOnNetworkRef m_playerRef;
    public FixedSoccerId m_soccerIdToAffect;
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
