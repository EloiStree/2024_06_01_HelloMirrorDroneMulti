using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MirrorRsaPlayerRefDicoRefreshMono: MonoBehaviour
{

    public List<MirrorRsaPlayerOnNetworkRef> m_playerRefList = new List<MirrorRsaPlayerOnNetworkRef>();
    public List<string> m_publicRsaKey = new List<string>();

    public void Start()
    {
        StartCoroutine(Refresh());
    }

    public IEnumerator Refresh() {
        while (true) {
            MirrorRsaPlayerOnNetworkRefDico.InstanceInScene.RemovePlayerNotValide();
            m_playerRefList = MirrorRsaPlayerOnNetworkRefDico.GetPlayersConnected();
            m_publicRsaKey = MirrorRsaPlayerOnNetworkRefDico.GetListOfPublicKey();
            yield return new WaitForSeconds(1);
        }
    }
}

