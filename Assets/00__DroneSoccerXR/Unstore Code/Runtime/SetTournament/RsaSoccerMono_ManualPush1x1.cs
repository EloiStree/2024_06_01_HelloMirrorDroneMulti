using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RsaSoccerMono_ManualPush1x1 : MonoBehaviour
{
    public MSoccerMono_Set12DronesRsaEth m_setter;

    public string m_rsaRed;
    public string m_etherAddressRed;
    public string m_rsaBlue;
    public string m_etherAddressBlue;
    public Scriptable_RsaEthDuel1x1 m_duel1x1;
    private void OnValidate()
    {
        
    }

    [ContextMenu("Push to Setter")]
    public void PushToSetter() {

        if (m_duel1x1 != null)
        {
            m_rsaRed = m_duel1x1.m_data.m_red.m_rsaPublicKey;
            m_rsaBlue = m_duel1x1.m_data.m_blue.m_rsaPublicKey;
            m_etherAddressRed = m_duel1x1.m_data.m_red.m_etherAddress;
            m_etherAddressBlue = m_duel1x1.m_data.m_blue.m_etherAddress;
        }
        if (m_setter != null)
        {
            m_setter.GetSetter().SetTeamRedRSAWithOneKey(m_rsaRed);
            m_setter.GetSetter().SetTeamBlueRSAWithOneKey(m_rsaBlue);
            m_setter.GetSetter().SetTeamRedEthWithOneKey(m_etherAddressRed);
            m_setter.GetSetter().SetTeamBlueEthWithOneKey(m_etherAddressBlue);
        }
    }
}
