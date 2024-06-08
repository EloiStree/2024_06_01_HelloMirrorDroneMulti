using Mirror;
using System;
using System.Collections.Generic;
/// <summary>
/// This class take byte and text to convert as command and push on server to ask to be executed with RSA identity;
/// </summary>
public class MSoccerMono_ConvertByteAndTextCommandRsaClient : NetworkBehaviour {


    public MSoccerMono_PushMultiDroneCommands m_playerCommandPusherRSA;
    public MirrorMono_RsaKeyIdentity m_rsaKeyIdentity;
    public byte[] m_lastByteReceived;
    public int m_lastCommandReceived;
    public int m_lastTargetDroneReceived;
    public int m_lastTargetDrone;
    public float m_lastLeftXSent;
    public List<FixedSoccerId> m_ownedDrone;

    public void PushActionAsByteOnClient(byte[] byteCommand) {

        if(m_rsaKeyIdentity==null)
            return;
        if (m_rsaKeyIdentity.GetRsaRef().IsPlayerNotValideAnymore())
            return;
        if (!m_rsaKeyIdentity.IsSignedValidatedByServer())
            return;

        int command = 0;
        if (byteCommand.Length == 4) 
            command = BitConverter.ToInt32(byteCommand, 0);
        else if (byteCommand.Length == 12) // (i)ID
            command = BitConverter.ToInt32(byteCommand, 0);
        else if (byteCommand.Length == 16)// IID
            command = BitConverter.ToInt32(byteCommand, 4);
        else return;
        
        //-20 to 20 99887766
        ParserIntegerToDronePercentUtility.Unpack(command,
             out int targetDrone,
             out float joystickLeftXPercent,
             out float joystickLeftYPercent,
             out float joystickRightXPercent,
             out float joystickRightYPercent
             );
        m_lastTargetDroneReceived = targetDrone;
        m_lastByteReceived = byteCommand;
        m_lastCommandReceived = command;

        ///ADD PRE FILTER HERE
        ///

        string rsaKey = m_rsaKeyIdentity.GetRsaRef().GetPublicKey();


        if (targetDrone == 0) { 
            m_playerCommandPusherRSA.PushDroneCommandsToFocusDrone(joystickLeftXPercent, joystickLeftYPercent, joystickRightXPercent, joystickRightYPercent );
        }

        // Try to parse Target Drone negative to a owned drone
        if (targetDrone < 0) {
            int ownedIndex = Math.Abs(targetDrone)-1;

            //Fetch drone owned in order of fixed soccer id that the player owned.
            MSoccerMono_IsDroneExistingOnClient.GetOwnedSoccerDrone(rsaKey,out m_ownedDrone);
            if (ownedIndex < m_ownedDrone.Count) {
                targetDrone = (int)m_ownedDrone[ownedIndex];
            }
        }
        m_lastTargetDrone = targetDrone;
        if (targetDrone >= 1 && targetDrone <= 12) {
            FixedSoccerId fixedSoccerId = (FixedSoccerId)targetDrone;
            if (MSoccerMono_IsDroneExistingOnClient.IsOwnerOfFixedSoccerDrone(rsaKey, fixedSoccerId)) {
                m_lastLeftXSent = joystickLeftXPercent;
                //ONLY IF YOU ARE SURE YOU OWN THE DRONE !!! TO AVOID NETWORK COST.
                m_playerCommandPusherRSA.PushDroneCommandsToFixedSoccerIntIds(fixedSoccerId, joystickLeftXPercent, joystickLeftYPercent, joystickRightXPercent, joystickRightYPercent);
            }
        }
    }


    public void PushActionAsTextOnClient(string  command)
    {
        if (m_rsaKeyIdentity == null)
            return;
        if (m_rsaKeyIdentity.GetRsaRef().IsPlayerNotValideAnymore())
            return;
        if (!m_rsaKeyIdentity.IsSignedValidatedByServer())
            return;

        ParserTextCmdToDronePercentUtility.PushTextAsCommand(command,
             out bool isValideCommand,
             out string targetDrone,
             out float joystickLeftXPercent,
             out float joystickLeftYPercent,
             out float joystickRightXPercent,
             out float joystickRightYPercent
             );
        if(!isValideCommand)
            return;
        if(string.IsNullOrEmpty(targetDrone))
            return;
        string rsaKey = m_rsaKeyIdentity.GetRsaRef().GetPublicKey();
        if (MSoccerMono_IsDroneExistingOnClient.IsExistingAsAliasAndOwned(targetDrone, rsaKey)) {
            m_playerCommandPusherRSA.PushDroneCommandsToDroneAlias(targetDrone, joystickLeftXPercent, joystickLeftYPercent, joystickRightXPercent, joystickRightYPercent);
        }
    }
}
