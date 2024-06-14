using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_PublicRSAToDrones : MonoBehaviour
{

    public List<RsaToDrone> m_rsaToDronesBlueTeam= new List<RsaToDrone>();
    public List<RsaToDrone> m_rsaToDronesRedTeam= new List<RsaToDrone>();


    public List<MSoccerMono_AbstractGamepad> m_allGamepads = new List<MSoccerMono_AbstractGamepad>();


    [Serializable]
    public class RsaToDrone {
        public MSoccerMono_AbstractGamepad m_gamepadToAffect;
    }


    private void Awake()
    {
        RefreshCurrentListOfGamepad();
    }

    [ContextMenu("Refresh Gamepad List")]
    private void RefreshCurrentListOfGamepad()
    {
        m_allGamepads.Clear();
        for (int i = 0; i < m_rsaToDronesBlueTeam.Count; i++)
        {
            m_allGamepads.Add(m_rsaToDronesBlueTeam[i].m_gamepadToAffect);
        }
        for (int i = 0; i < m_rsaToDronesRedTeam.Count; i++)
        {
            m_allGamepads.Add(m_rsaToDronesRedTeam[i].m_gamepadToAffect);
        }
    }


   

    public void GetAllGamepad(out List<MSoccerMono_AbstractGamepad> pads)
    {
        pads= m_allGamepads;

    }
}

