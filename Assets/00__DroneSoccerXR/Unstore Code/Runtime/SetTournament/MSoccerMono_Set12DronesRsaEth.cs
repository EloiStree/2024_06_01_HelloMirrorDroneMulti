using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_Set12DronesRsaEth : MonoBehaviour
{


    public RsaEthDroneSoccerInSceneGroup m_toAffectGroup;
    public RsaEthDroneSoccerInSceneGroupSetter m_setter;

    [ContextMenu("Refresh Setter")]
    public void RefreshSetter()
    {
        m_setter.SetGroup(m_toAffectGroup);
    }

    private void Awake()
    {
        RefreshSetter();
    }
    public RsaEthDroneSoccerInSceneGroupSetter GetSetter() { return m_setter; }

    public void ResetAllToUnclaimed()
    {
        foreach (RsaEthDroneSoccerInSceneItem item in m_setter.m_allItems)
        {
            item.m_rsaKeyOwner.Unclaim();
            item.m_ethFlag.Unclaim();
        }
    }
}


[System.Serializable]
public class RsaEthDroneSoccerInSceneGroupSetter {

    public RsaEthDroneSoccerInSceneGroup m_group;
    public List<RsaEthDroneSoccerInSceneItem> m_allItems;
    public List<RsaEthDroneSoccerInSceneItem> m_allItemsRed;
    public List<RsaEthDroneSoccerInSceneItem> m_allItemsBlue;


    public void TryToSet(string playerRsa, string playerEth) { 
    
        for (int i = 0; i < m_allItems.Count; i++) {
            if (i < playerRsa.Length)
            {
                m_allItems[i].m_rsaKeyOwner.IsOwnedByExactly(playerRsa);
                m_allItems[i].m_ethFlag.SetEtheumAddress(playerEth);

            }
            
        }
    }

    public void SetGroup(RsaEthDroneSoccerInSceneGroup group) {
        m_group = group;
        m_allItems = new List<RsaEthDroneSoccerInSceneItem>();
        m_allItems.Add(m_group.m_droneRed0);
        m_allItems.Add(m_group.m_droneRed1);
        m_allItems.Add(m_group.m_droneRed2);
        m_allItems.Add(m_group.m_droneRed3);
        m_allItems.Add(m_group.m_droneRed4);
        m_allItems.Add(m_group.m_droneRed5);
        m_allItems.Add(m_group.m_droneBlue0);
        m_allItems.Add(m_group.m_droneBlue1);
        m_allItems.Add(m_group.m_droneBlue2);
        m_allItems.Add(m_group.m_droneBlue3);
        m_allItems.Add(m_group.m_droneBlue4);
        m_allItems.Add(m_group.m_droneBlue5);
        m_allItemsRed = new List<RsaEthDroneSoccerInSceneItem>();
        m_allItemsRed.Add(m_group.m_droneRed0);
        m_allItemsRed.Add(m_group.m_droneRed1);
        m_allItemsRed.Add(m_group.m_droneRed2);
        m_allItemsRed.Add(m_group.m_droneRed3);
        m_allItemsRed.Add(m_group.m_droneRed4);
        m_allItemsRed.Add(m_group.m_droneRed5);
        m_allItemsBlue = new List<RsaEthDroneSoccerInSceneItem>();
        m_allItemsBlue.Add(m_group.m_droneBlue0);
        m_allItemsBlue.Add(m_group.m_droneBlue1);
        m_allItemsBlue.Add(m_group.m_droneBlue2);
        m_allItemsBlue.Add(m_group.m_droneBlue3);
        m_allItemsBlue.Add(m_group.m_droneBlue4);
        m_allItemsBlue.Add(m_group.m_droneBlue5);

    }
    #region RSA
    public void SetAllWithOneRsaKey(string rsaKey) { 
        
        foreach (RsaEthDroneSoccerInSceneItem item in m_allItems) {
            item.m_rsaKeyOwner.Claim(rsaKey);
        }
    }
    public void SetTeamWithTwoRsaKey(string rsaTeamRed, string rsaTeamBlue) {
        SetTeamBlueRSAWithOneKey(rsaTeamRed);
        SetTeamBlueRSAWithOneKey(rsaTeamBlue);
    }
    public void SetTeamBlueRSAWithOneKey(string rsaKey) { 
        foreach (RsaEthDroneSoccerInSceneItem item in m_allItemsBlue) {
            item.m_rsaKeyOwner.Claim(rsaKey);
        }
    }
    public void SetTeamRedRSAWithOneKey(string rsaKey) { 
        foreach (RsaEthDroneSoccerInSceneItem item in m_allItemsRed) {
            item.m_rsaKeyOwner.Claim(rsaKey);
        }
    }
    public void SetPlayerOneByOneRsa(List<RsaEthDroneSoccerInSceneItem> rsaList , params string[] players ) { 
        for (int i = 0; i < rsaList.Count; i++) {
            if(i < players.Length)
                rsaList[i].m_rsaKeyOwner.Claim(players[i]);
            else
                rsaList[i].m_rsaKeyOwner.Claim("");
        }
    }
    public void SetPlayerOneByOneRsaRed(params string[] players) { 
        SetPlayerOneByOneRsa(m_allItemsRed, players);
    }
    public void SetPlayerOneByOneRsaBlue(params string[] players) { 
        SetPlayerOneByOneRsa(m_allItemsBlue, players);
    }
    public void SetPlayerOneByOneRsaRedBlue( string[] playersRed,  string[] playersBlue) { 
        SetPlayerOneByOneRsaRed(playersRed);
        SetPlayerOneByOneRsaBlue(playersBlue);
    }


    #endregion


    #region ETH
    public void SetAllWithOneEthKey(string ethAddress)
    {

        foreach (RsaEthDroneSoccerInSceneItem item in m_allItems)
        {
            item.m_ethFlag.SetEtheumAddress(ethAddress);
        }
    }
    public void SetTeamWithTwoEthKey(string ethAddressRed, string ethAddressBlue)
    {
        SetTeamRedEthWithOneKey(ethAddressRed);
        SetTeamBlueEthWithOneKey(ethAddressBlue);
    }
    public void SetTeamBlueEthWithOneKey(string ethAddress)
    {
        foreach (RsaEthDroneSoccerInSceneItem item in m_allItemsBlue)
        {
            item.m_ethFlag.SetEtheumAddress(ethAddress);
        }
    }
    public void SetTeamRedEthWithOneKey(string ethAddress)
    {
        foreach (RsaEthDroneSoccerInSceneItem item in m_allItemsRed)
        {
            item.m_ethFlag.SetEtheumAddress(ethAddress);
        }
    }
    public void SetPlayerOneByOneEth(List<RsaEthDroneSoccerInSceneItem> etherAddressList, params string[] players)
    {
        for (int i = 0; i < etherAddressList.Count; i++)
        {
            if (i < players.Length)
                etherAddressList[i].m_ethFlag.SetEtheumAddress(players[i]);
            else
                etherAddressList[i].m_ethFlag.SetEtheumAddress("");
        }
    }
    public void SetPlayerOneByOneEthRed(params string[] players)
    {
        SetPlayerOneByOneEth(m_allItemsRed, players);
    }
    public void SetPlayerOneByOneEthBlue(params string[] players)
    {
        SetPlayerOneByOneEth(m_allItemsBlue, players);
    }
    public void SetPlayerOneByOneEthRedBlue(string[] playersRed, string[] playersBlue)
    {
        SetPlayerOneByOneEthRed(playersRed);
        SetPlayerOneByOneEthBlue(playersBlue);
    }

    public void SetPlayerAsRedIndexRsa(int i, string playerRsa)
    {
        switch (i)
        {
            case 0: m_group.m_droneRed0.m_rsaKeyOwner.Claim(playerRsa); break;
            case 1: m_group.m_droneRed1.m_rsaKeyOwner.Claim(playerRsa); break;
            case 2: m_group.m_droneRed2.m_rsaKeyOwner.Claim(playerRsa); break;
            case 3: m_group.m_droneRed3.m_rsaKeyOwner.Claim(playerRsa); break;
            case 4: m_group.m_droneRed4.m_rsaKeyOwner.Claim(playerRsa); break;
            case 5: m_group.m_droneRed5.m_rsaKeyOwner.Claim(playerRsa); break;
            default:
                break;
        }
    }

    public void SetPlayerAsBlueIndexRsa(int i, string playerRsa)
    {
        switch (i)
        {
            case 0: m_group.m_droneBlue0.m_rsaKeyOwner.Claim(playerRsa); break;
            case 1: m_group.m_droneBlue1.m_rsaKeyOwner.Claim(playerRsa); break;
            case 2: m_group.m_droneBlue2.m_rsaKeyOwner.Claim(playerRsa); break;
            case 3: m_group.m_droneBlue3.m_rsaKeyOwner.Claim(playerRsa); break;
            case 4: m_group.m_droneBlue4.m_rsaKeyOwner.Claim(playerRsa); break;
            case 5: m_group.m_droneBlue5.m_rsaKeyOwner.Claim(playerRsa); break;
            default:
                break;
        }
    }
    #endregion
}


[System.Serializable]
public class RsaEthDroneSoccerInSceneGroup
{
    public RsaEthDroneSoccerInSceneItem m_droneRed0;
    public RsaEthDroneSoccerInSceneItem m_droneRed1;
    public RsaEthDroneSoccerInSceneItem m_droneRed2;
    public RsaEthDroneSoccerInSceneItem m_droneRed3;
    public RsaEthDroneSoccerInSceneItem m_droneRed4;
    public RsaEthDroneSoccerInSceneItem m_droneRed5;

    public RsaEthDroneSoccerInSceneItem m_droneBlue0;
    public RsaEthDroneSoccerInSceneItem m_droneBlue1;
    public RsaEthDroneSoccerInSceneItem m_droneBlue2;
    public RsaEthDroneSoccerInSceneItem m_droneBlue3;
    public RsaEthDroneSoccerInSceneItem m_droneBlue4;
    public RsaEthDroneSoccerInSceneItem m_droneBlue5;
}


[System.Serializable]
public class RsaEthDroneSoccerInSceneItem
{

    public MirrorMono_PublicRsaKeyOwner m_rsaKeyOwner;
    public MirrorMono_EthAddressPlayerFlag m_ethFlag;

}