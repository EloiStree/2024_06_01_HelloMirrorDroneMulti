using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_PlayerInRoomManager : MonoBehaviour
{

    public MSoccerMono_IsDroneExistingOnClient m_droneExistingOnClient;
    public MirrorMono_IndexIntegerOwner [] m_droneCurrentOwnerList;
    public List<int> m_droneCurrentOwnerListAllowed = new List<int>();


    public int m_notifyPlayerWantToPlay = 123456789;
    public int m_notifyPlayerWantToPlayConfirmIt = 987654321;

    public Dictionary<int, long> m_playerWantToPlay = new Dictionary<int, long>();
    public Dictionary<int, long> m_playerWantToPlayConfirmed = new Dictionary<int, long>();

    public Dictionary<int, bool> m_isBanDico = new Dictionary<int, bool>();
    public Dictionary<int, bool> m_isAllowdedDico = new Dictionary<int, bool>();

    //List<int> m_playerInRoomWantToPlay = new List<int>();
    //List<int> m_playerInRoomWantToPlayConfirmed = new List<int>();
    public List<int> m_playerInRoom = new List<int>();

    public int m_lastReceivedIndex;
    public int m_lastReceivedValue;

    public UnityEvent<List<int>> m_onPlayConnectedCountChanged;

    public UnityEvent<int> m_onAddedPlayer;
    public UnityEvent<int> m_onRemovedPlayer;

    public bool m_allowGuest=true;


    public bool m_useAllowedPlayerList;
    public List<int> m_allowedPlayerList = new List<int>();
    public bool m_useBanList;
    public List<int> m_banList= new List<int>();

    public void Start()
    {
        RefreshBanAllowedList();

        RefreshListOfDroneUser();
    }

    [ContextMenu("Refresh Allowed Player List")]
    public  void RefreshBanAllowedList()
    {
        m_isBanDico.Clear();
        m_isAllowdedDico.Clear();
        foreach (var item in m_banList)
        {
            if (!m_isBanDico.ContainsKey(item))
                m_isBanDico.Add(item, true);
        }

        foreach (var item in m_allowedPlayerList)
        {
            if (!m_isAllowdedDico.ContainsKey(item))
                m_isAllowdedDico.Add(item, true);
        }


        RemovePlayerBasedOnList();

    }

    public void RemovePlayerBasedOnList() {

        List<int> list = new List<int>();
        if (m_useBanList) { 
            foreach (var item in m_isBanDico.Keys.ToList())
            {     
                if(m_playerWantToPlayConfirmed.ContainsKey(item))
                    list.Add(item);
            }
        }
        if (m_useAllowedPlayerList) {
            foreach (var player in m_playerWantToPlayConfirmed.Keys.ToList())
            {
                if (!m_isAllowdedDico.ContainsKey(player))
                {
                    list.Add(player);

                }
            }
        }

        RemovePlayerIfIngame(list);
        RefreshListOfPlayerInRoom();

    }


    public void RemovePlayerIfIngame(params int[] players) {

        foreach (var player in players)
            RemovePlayerIfIngame(player);
    }


    public void RemovePlayerIfIngame(IEnumerable<int>players)
    {

        foreach (var player in players)
            RemovePlayerIfIngame(player);
    }


    private void RemovePlayerIfIngame(int item)
    {
        if (m_playerWantToPlayConfirmed.ContainsKey(item))
        {
            m_playerWantToPlayConfirmed.Remove(item);
            if (m_playerWantToPlay.ContainsKey(item))
            {
                m_playerWantToPlay.Remove(item);

            }
            m_onRemovedPlayer.Invoke(item);
        }
    }

    public void PushInIID(int index, int value, long date) => PushInIID(index, value);
    public void PushInIID(int index, int value)
    {
        if (!m_allowGuest && index < 0)
            return;
        m_lastReceivedIndex = index;
        m_lastReceivedValue = value;

        if (m_useBanList && m_isBanDico.ContainsKey(index))
            return;
        if (m_useAllowedPlayerList && !m_isAllowdedDico.ContainsKey(index))
            return;

        if (value == m_notifyPlayerWantToPlay)
            SaveWantToPlay(index);
        if (value == m_notifyPlayerWantToPlayConfirmIt)
            SaveWantToPlayConfirmed(index);
            
    }

    private void SaveWantToPlay(int index)
    {
        if (!m_playerWantToPlay.ContainsKey(index)) { 
            m_playerWantToPlay.Add(index, DateTime.UtcNow.Ticks);
           // m_playerInRoomWantToPlay = m_playerWantToPlay.Keys.ToList();
        }
        else m_playerWantToPlay[index]= DateTime.UtcNow.Ticks;
    }
    private void SaveWantToPlayConfirmed(int index)
    {
        if (m_playerWantToPlay.ContainsKey(index))
        {
            if (!m_playerWantToPlayConfirmed.ContainsKey(index)) { 
                m_playerWantToPlayConfirmed.Add(index, DateTime.UtcNow.Ticks);
                // m_playerInRoomWantToPlayConfirmed = m_playerWantToPlay.Keys.ToList();
                RefreshListOfPlayerInRoom();
                m_onAddedPlayer.Invoke(index);
            }
            else m_playerWantToPlayConfirmed[index] = DateTime.UtcNow.Ticks;
        }
    }

    private void RefreshListOfPlayerInRoom()
    {
        m_playerInRoom = m_playerWantToPlayConfirmed.Keys.ToList();
        m_onPlayConnectedCountChanged.Invoke(m_playerInRoom);
    }

    private void Awake()
    {
        InvokeRepeating("RefreshListOfDroneUser", 0, 1);
    }

    private void RefreshListOfDroneUser()
    {
        if (m_droneExistingOnClient != null) { 
            m_droneCurrentOwnerList = m_droneExistingOnClient.m_droneByIndex;
            m_droneCurrentOwnerListAllowed=  m_droneCurrentOwnerList.Select(k=>k.m_indexOfUser).ToList();
        }
    }
}
