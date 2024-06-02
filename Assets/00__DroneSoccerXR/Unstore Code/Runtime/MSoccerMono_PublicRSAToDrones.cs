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

        public string m_publicRsakey;
        public int m_integerUniqueID;
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

    [ContextMenu("Reset Integer as default for guest")]
    public void ResetAsIntegerDefaultUniqueId() 
    {
        for (int i = 0; i < m_rsaToDronesBlueTeam.Count; i++)
        {
            m_rsaToDronesBlueTeam[i].m_integerUniqueID = -1 * (i+1);
        }
        for (int i = 0; i < m_rsaToDronesRedTeam.Count; i++)
        {
            m_rsaToDronesRedTeam[i].m_integerUniqueID =-1*( i+1+6);
        }
    }



    public void SetTeamBlueRsaKeys(params string[] publicKey)
    {
        for (int i = 0; i < publicKey.Length; i++)
        {
            if (i < m_rsaToDronesBlueTeam.Count)
            {
                m_rsaToDronesBlueTeam[i].m_publicRsakey = publicKey[i];
            }
        }
    }
    public void SetTeamRedRsaKeys(params string[] publicKey)
    {
        for (int i = 0; i < publicKey.Length; i++)
        {
            if (i < m_rsaToDronesRedTeam.Count)
            {
                m_rsaToDronesRedTeam[i].m_publicRsakey = publicKey[i];
            }
        }
    }
    public void SetTeamRedIntegerUniqueIdKeys(params int[] publicKey)
    {
        for (int i = 0; i < publicKey.Length; i++)
        {
            if (i < m_rsaToDronesBlueTeam.Count)
            {
                m_rsaToDronesBlueTeam[i].m_integerUniqueID = publicKey[i];
            }
        }
    }
    public void SetTeamBlueIntegerUniqueIdKeys(params int[] publicKey)
    {
        for (int i = 0; i < publicKey.Length; i++)
        {
            if (i < m_rsaToDronesRedTeam.Count)
            {
                m_rsaToDronesRedTeam[i].m_integerUniqueID = publicKey[i];
            }
        }
    }

    public MSoccerMono_AbstractGamepad GetGamepadByUniqueID(int integerUniqueIDTarget)
    {
        for (int i = 0; i < m_rsaToDronesBlueTeam.Count; i++)
        {
            if (m_rsaToDronesBlueTeam[i].m_integerUniqueID == integerUniqueIDTarget)
            {
                return m_rsaToDronesBlueTeam[i].m_gamepadToAffect;
            }
        }
        for (int i = 0; i < m_rsaToDronesRedTeam.Count; i++)
        {
            if (m_rsaToDronesRedTeam[i].m_integerUniqueID == integerUniqueIDTarget)
            {
                return m_rsaToDronesRedTeam[i].m_gamepadToAffect;
            }
        }
        return null;
    }

    public void GetAllGamepad(out List<MSoccerMono_AbstractGamepad> pads)
    {
        pads= m_allGamepads;

    }
}

