using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MSoccerMono_FriendlyAutoMatchingIIDUnder12 : MonoBehaviour
{

    public MSoccerMono_PlayerRoomManagerIID m_playersRoom;

    public MSoccerMono_IsDroneExistingOnClient m_droneExistingOnClient;
    public MirrorMono_IndexIntegerOwner[] m_droneCurrentOwnerList;
    public int[] m_droneClaimState;
    public int[] m_playersInRoom;

    public void Awake()
    {

        RefreshListOfDroneUser();
    }
    private void OnEnable()
    {
        RefreshListOfDroneUser();

        m_playersRoom.AddPlayerChangedListener(NewPlayerInRoom);

    }
    private void OnDisable()
    {
        m_playersRoom.RemovePlayerChangedListener(NewPlayerInRoom);
    }


    void NewPlayerInRoom(int[] playersInRoom)
    {
        m_playersInRoom = playersInRoom;

        if (m_droneCurrentOwnerList.Length != 12) {

            return;
        }
        int count = m_playersInRoom.Length;
        if (count == 0)
        {
            foreach (var d in m_droneCurrentOwnerList)
            {
                d.Unclaim();
            }
        }
        if (count == 1)
        {
            foreach (var d in m_droneCurrentOwnerList)
            {
                d.Claim(m_playersInRoom[0]);
            }
        }
        if (count == 2)
        {
            for (int i = 0; i < 6; i++)
            {
                m_droneCurrentOwnerList[i].Claim(m_playersInRoom[0]);
            }
            for (int i = 6; i < 12; i++)
            {
                m_droneCurrentOwnerList[i].Claim(m_playersInRoom[1]);
            }
        }
        if (count > 2 && count < 13)
        { 
            if (count  >= 1) m_droneCurrentOwnerList[0].Claim(m_playersInRoom[0]);
            if (count  >= 2) m_droneCurrentOwnerList[6].Claim(m_playersInRoom[1]);
            if (count  >= 3) m_droneCurrentOwnerList[1].Claim(m_playersInRoom[2]);
            if (count  >= 4) m_droneCurrentOwnerList[7].Claim(m_playersInRoom[3]);
            if (count  >= 5) m_droneCurrentOwnerList[2].Claim(m_playersInRoom[4]);
            if (count  >= 6) m_droneCurrentOwnerList[8].Claim(m_playersInRoom[5]);
            if (count  >= 7) m_droneCurrentOwnerList[3].Claim(m_playersInRoom[6]);
            if (count  >= 8) m_droneCurrentOwnerList[9].Claim(m_playersInRoom[7]);
            if (count  >= 9) m_droneCurrentOwnerList[4].Claim(m_playersInRoom[8]);
            if (count  >= 10) m_droneCurrentOwnerList[10].Claim(m_playersInRoom[9]);
            if (count  >= 11) m_droneCurrentOwnerList[5].Claim(m_playersInRoom[10]);
            if (count  >= 12) m_droneCurrentOwnerList[11].Claim(m_playersInRoom[11]);
        }
        RefreshListOfDroneUser();
    }


    private void RefreshListOfDroneUser()
    {
        if (m_droneExistingOnClient != null)
        {
            m_droneCurrentOwnerList = m_droneExistingOnClient.m_droneByIndex;
            m_droneClaimState = m_droneCurrentOwnerList.Select(k => k.m_indexOfUser).ToArray();
        }
    }
}
