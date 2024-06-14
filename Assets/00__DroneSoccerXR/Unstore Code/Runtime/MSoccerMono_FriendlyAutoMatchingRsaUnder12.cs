using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_FriendlyAutoMatchingRsaUnder12 : MonoBehaviour
{

    public bool m_setOffForTornamentMode = false;
    public MirrorRsaPlayerRefDicoRefreshMono m_playerSignedInGame;
    public MSoccerMono_Set12DronesRsaEth m_soccerSetter;
    public List<MirrorRsaPlayerOnNetworkRef> m_playersInGame= new List<MirrorRsaPlayerOnNetworkRef>();
    public List<MirrorRsaPlayerOnNetworkRef> m_activePlayer = new List<MirrorRsaPlayerOnNetworkRef>();



    private void OnEnable()
    {
        MirrorRsaPlayerOnNetworkRefDico.InstanceInScene.GetCurrentPlayerConnected(out m_playersInGame);
        MirrorRsaPlayerOnNetworkRefDico.InstanceInScene.m_onNewRsaPlayer += OnNewRsaPlayer;
        MirrorRsaPlayerOnNetworkRefDico.InstanceInScene.m_onRemovedNetworkId += OnRemovedNetworkId;
        MirrorRsaPlayerOnNetworkRefDico.InstanceInScene.m_onRemovedRsaKey += OnRemovedRsaKey;
    }

    private void OnRemovedNetworkId(int obj)
    {
        FlushEmpty();
    }

    private void OnRemovedRsaKey(string obj)
    {
        FlushEmpty();
    }

    private void FlushEmpty()
    {
        for(int i = m_playersInGame.Count-1; i >=0; i--)
        {
            if (m_playersInGame[i]==null || m_playersInGame[i].IsPlayerNotValideAnymore())
            {
                m_playersInGame.RemoveAt(i);
            }
        }
    }

    private void OnNewRsaPlayer(MirrorRsaPlayerOnNetworkRef player)
    {
        m_playersInGame.Add(player);
        if (m_setOffForTornamentMode == true)
            return;

        if (m_playersInGame.Count == 0) { 
        
            m_soccerSetter.ResetAllToUnclaimed();
            m_activePlayer = new List<MirrorRsaPlayerOnNetworkRef>();
            Debug.Log("No player waiting for some");
            RefreshEther();
        }
        else if (m_playersInGame.Count == 1)
        {
            m_soccerSetter.GetSetter().SetAllWithOneRsaKey(player.GetPublicKey());
            m_soccerSetter.GetSetter().SetAllWithOneEthKey(GetEtherAddressOf(player));
            Debug.Log("Start Wait for player Mode");
            m_activePlayer= new List<MirrorRsaPlayerOnNetworkRef>();
            m_activePlayer.Add(player);
            RefreshEther();
        }
        else if(m_playersInGame.Count == 2)
        {
            m_soccerSetter.GetSetter().SetTeamRedRSAWithOneKey(m_playersInGame[0].GetPublicKey());
            string getEther = GetEtherAddressOf(m_playersInGame[0]);
            m_soccerSetter.GetSetter().SetTeamRedEthWithOneKey(getEther);

            m_soccerSetter.GetSetter().SetTeamBlueRSAWithOneKey(m_playersInGame[1].GetPublicKey());
            getEther= GetEtherAddressOf(m_playersInGame[0]);
            m_soccerSetter.GetSetter().SetTeamBlueEthWithOneKey(getEther);
            Debug.Log("Start 1 vs 1");
            m_activePlayer = new List<MirrorRsaPlayerOnNetworkRef>();
            m_activePlayer.Add(m_playersInGame[0]);
            m_activePlayer.Add(m_playersInGame[1]);
            RefreshEther();

        }

        else if (m_playersInGame.Count>2 && m_playersInGame.Count<=12){

            Debug.Log("Start 3 to 12 match making");
            string[] playerAll = new string[12];
            for (int i = 0; i < 12; i++)
            {
                string rsa =i<m_playersInGame.Count?  m_playersInGame[i].GetPublicKey():"";
                if (i % 2 == 0)
                {
                    m_soccerSetter.GetSetter().SetPlayerAsRedIndexRsa(i, rsa);
                }
                else { 
                    m_soccerSetter.GetSetter().SetPlayerAsBlueIndexRsa(i, rsa);
                }
            }

            m_activePlayer = new List<MirrorRsaPlayerOnNetworkRef>();
            m_activePlayer.AddRange(m_playersInGame);
            RefreshEther();
        }
        else if (m_playersInGame.Count>12)
        {
            Debug.Log("Extra player mode.");
        }
    }

    private void RefreshEther()
    {
        for (int i = 0; i < m_activePlayer.Count; i++)
        {
            string rsa= m_activePlayer[i].GetPublicKey();
            string eth = GetEtherAddressOf(m_activePlayer[i]);
            m_soccerSetter.GetSetter().TryToSet(rsa, eth);
        }
    }

    private string GetEtherAddressOf(MirrorRsaPlayerOnNetworkRef player)
    {
        if(player==null)            return "";
        if(player.GetGameObject()==null)            return "";

        MSoccerMono_EthereumAddressToUse eth = player.GetGameObject().GetComponent<MSoccerMono_EthereumAddressToUse>();
        if (eth != null)
        {
            return (eth.m_ethereumAddressToUse);
        }
        else
        {
            return "";
        }
    }

 
}
