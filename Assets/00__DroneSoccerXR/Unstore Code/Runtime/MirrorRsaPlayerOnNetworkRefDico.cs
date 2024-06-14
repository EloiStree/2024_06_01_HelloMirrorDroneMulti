using System;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class MirrorRsaPlayerOnNetworkRefDico
{
    public static MirrorRsaPlayerOnNetworkRefDico InstanceInScene = new MirrorRsaPlayerOnNetworkRefDico();
    public  Dictionary<int, MirrorRsaPlayerOnNetworkRef> m_playerRefDicoId = new Dictionary<int, MirrorRsaPlayerOnNetworkRef>();
    public  Dictionary<string, List<MirrorRsaPlayerOnNetworkRef>> m_playerRefDicoKey = new Dictionary<string, List<MirrorRsaPlayerOnNetworkRef>>();


    public  void GetCurrentPlayerConnected(out List<MirrorRsaPlayerOnNetworkRef> players) {
        players = m_playerRefDicoId.Values.ToList();
    }
    public  void AddPlayerRef(MirrorRsaPlayerOnNetworkRef playerRef) {
        if(playerRef==null) {
            return;
        }
        if(playerRef.GetPlayer()==null) {
            return;
        }
        if(playerRef.GetPlayer().netId==0) {
            Debug.LogWarning("You can't add a player with a netId of 0.");
            return;
        }

        if (m_playerRefDicoId.ContainsKey(playerRef.GetInstanceId())){

            Debug.LogWarning("You can't add a two time the smale player network id");
            return;
        }
        m_playerRefDicoId.Add(playerRef.GetInstanceId(), playerRef);

        if (m_playerRefDicoKey.ContainsKey(playerRef.GetPublicKey())==false) {
            m_playerRefDicoKey.Add(playerRef.GetPublicKey(), new List<MirrorRsaPlayerOnNetworkRef>());
        }
        m_playerRefDicoKey[playerRef.GetPublicKey()].Add(playerRef);
        m_onNewRsaPlayer.Invoke(playerRef);
    }
    public  void RemovePlayerRef(MirrorRsaPlayerOnNetworkRef playerRef) {
        m_playerRefDicoId.Remove(playerRef.GetInstanceId());
        m_playerRefDicoKey.Remove(playerRef.GetPublicKey());
    }

    public Action<MirrorRsaPlayerOnNetworkRef> m_onNewRsaPlayer;
    public Action<int> m_onRemovedNetworkId;
    public Action<string> m_onRemovedRsaKey;

    public void RemovePlayerNotValide() {

        List<int> toRemoveId = new List<int>();
        List<string> toRemoveKey = new List<string>();
        List<int> ids = m_playerRefDicoId.Keys.ToList();
        List<string> rsa = m_playerRefDicoKey.Keys.ToList();
        foreach (var item in ids) {
            if (m_playerRefDicoId[item]==null ||
                m_playerRefDicoId[item].IsPlayerStillValide()==false) {
                m_playerRefDicoId.Remove(item);
                toRemoveId.Insert(0, item);
            }
        }
        foreach (var item in rsa) {
            if (m_playerRefDicoKey[item] == null ) {
                m_playerRefDicoKey.Remove(item);
                toRemoveKey.Insert(0, item);
            }
            
            if (m_playerRefDicoKey[item] != null &&
                m_playerRefDicoKey[item].Count >= 1) {
                List<MirrorRsaPlayerOnNetworkRef> rsas = m_playerRefDicoKey[item];
                for(int i = rsas.Count-1; i>=0; i--) {
                    if(rsas[i]==null || rsas[i].GetPlayer()==null || rsas[i].IsPlayerStillValide()==false) {
                        rsas.RemoveAt(i);
                    }
                }
            }
            if (m_playerRefDicoKey[item].Count == 0)
            {
                m_playerRefDicoKey.Remove(item);
                toRemoveKey.Insert(0, item);
            }
        }

        foreach (var item in toRemoveId) {
            m_onRemovedNetworkId?.Invoke(item);
        }
        foreach (var item in toRemoveKey) {
            m_onRemovedRsaKey?.Invoke(item);
        }
    }

    public static List<MirrorRsaPlayerOnNetworkRef> GetPlayersConnected()
    {
        return InstanceInScene.m_playerRefDicoId.Values.ToList();
    }

    public static List<string> GetListOfPublicKey()
    {
        return InstanceInScene.m_playerRefDicoKey.Keys.ToList();
    }

    static MirrorRsaPlayerOnNetworkRef m_currentPlayerRsa;
    public static void SetCurrentPlayer(MirrorRsaPlayerOnNetworkRef player)
    {
        m_currentPlayerRsa = player;
    }

    public static MirrorRsaPlayerOnNetworkRef GetCurrentPlayer()
    {
        return m_currentPlayerRsa;
    }
}
