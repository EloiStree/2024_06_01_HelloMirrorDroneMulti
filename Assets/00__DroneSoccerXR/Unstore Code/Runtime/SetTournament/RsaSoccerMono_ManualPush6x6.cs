using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RsaSoccerMono_ManualPush6x6 : MonoBehaviour
{
    public MSoccerMono_Set12DronesRsaEth m_setter;
    public Scriptable_RsaTeam6vs6 m_6vs6;
    

    [ContextMenu("Push to Setter from 6vs6")]
    public void PushToSetter()
    {

        if (m_setter != null)
        {
        }
    }
}
public class RsaSoccerMono_ManualPushTeamVsTeam : MonoBehaviour
{
    public MSoccerMono_Set12DronesRsaEth m_setter;
    public Scriptable_RsaEthTeam6 m_red;
    public Scriptable_RsaEthTeam6 m_blue;

    [ContextMenu("Push to Setter from 6vs6")]
    public void PushToSetter()
    {

        if (m_setter != null)
        {

            RsaEthPlayerDataUtility.GetListRsaOf(out List<string> rsaRed, out List<string> etherRed,
                m_red.m_data.m_drone0Stricker,
                m_red.m_data.m_drone1,
                m_red.m_data.m_drone2,
                m_red.m_data.m_drone3,
                m_red.m_data.m_drone4,
                m_red.m_data.m_drone5);
            m_setter.GetSetter().SetPlayerOneByOneEthRed(etherRed.ToArray());
            m_setter.GetSetter().SetPlayerOneByOneRsaRed(rsaRed.ToArray());
            RsaEthPlayerDataUtility.GetListRsaOf(out rsaRed, out  etherRed,
                 m_blue.m_data.m_drone0Stricker,
                 m_blue.m_data.m_drone1,
                 m_blue.m_data.m_drone2,
                 m_blue.m_data.m_drone3,
                 m_blue.m_data.m_drone4,
                 m_blue.m_data.m_drone5);
            m_setter.GetSetter().SetPlayerOneByOneEthBlue(etherRed.ToArray());
            m_setter.GetSetter().SetPlayerOneByOneRsaBlue(rsaRed.ToArray());
        }
    }
}
