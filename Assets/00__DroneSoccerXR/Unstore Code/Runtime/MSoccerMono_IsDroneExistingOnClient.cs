using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class MSoccerMono_IsDroneExistingOnClient : MonoBehaviour {

    static MSoccerMono_IsDroneExistingOnClient m_inScene;
    public static MSoccerMono_IsDroneExistingOnClient InstanceInScene
    {
        get { 
            if( m_inScene == null)
                m_inScene = GameObject.FindObjectOfType<MSoccerMono_IsDroneExistingOnClient>(false);
            if (m_inScene == null)
                m_inScene = new GameObject("MSoccerMono_IsDroneExistingOnClient").AddComponent<MSoccerMono_IsDroneExistingOnClient>();      
            return m_inScene; 
        }

        private set { m_inScene = value; }
    }
   

    public void Awake()
    {
        RefreshListofObjectInScene();

        InstanceInScene = this;
    }

    [ContextMenu("Refresh List of Object in Scene")]
    private void RefreshListofObjectInScene()
    {
        m_droneByAlias = GameObject.FindObjectsOfType<MirrorMono_CallableUniqueAlias>(false);
        m_droneByIndex = GameObject.FindObjectsOfType<MirrorMono_IndexIntegerOwner>(false);
        m_droneByRsaPublic = GameObject.FindObjectsOfType<MirrorMono_PublicRsaKeyOwner>(false);
        m_droneBySoccerId = GameObject.FindObjectsOfType<MSoccerMono_FixedSoccerIdTag>(false);
        m_trustParserInputIID = GameObject.FindObjectOfType<MSoccerMono_TrustParserInputInt>(false);

        m_soccerDronesToAlias = new SoccerIdtoRsaAlias[m_droneBySoccerId.Length];
        for(int i = 0; i < m_droneBySoccerId.Length; i++)
        {
            m_soccerDronesToAlias[i] = new SoccerIdtoRsaAlias();
            m_soccerDronesToAlias[i].m_fixedSoccerId = m_droneBySoccerId[i].m_fixedSoccerId;
            m_soccerDronesToAlias[i].m_rsaOwner = m_droneBySoccerId[i].GetComponent<MirrorMono_PublicRsaKeyOwner>();
            m_soccerDronesToAlias[i].m_aliasOwner = m_droneBySoccerId[i].GetComponent<MirrorMono_CallableUniqueAlias>();
            m_soccerDronesToAlias[i].m_integerOwner = m_droneBySoccerId[i].GetComponent<MirrorMono_IndexIntegerOwner>();
            m_soccerDronesToAlias[i].m_gamepad = m_droneBySoccerId[i].GetComponent<MSoccerMono_AbstractGamepad>();
        }
        m_soccerDronesToAlias= m_soccerDronesToAlias.OrderBy(x => x.GetFixedSoccerId()).ToArray();
        m_droneBySoccerId = m_droneBySoccerId.OrderBy(x => x.m_fixedSoccerId).ToArray();
        m_droneGamepadInFixedIdOrder = new MSoccerMono_AbstractGamepad[m_droneBySoccerId.Length];
        for (int i = 0; i < m_droneBySoccerId.Length; i++)
        {
            GetDroneSoccerIdGamepad(m_droneBySoccerId[i].m_fixedSoccerId, out m_droneGamepadInFixedIdOrder[i]);
        }
    }

    public MirrorMono_CallableUniqueAlias[] m_droneByAlias;
    public MirrorMono_IndexIntegerOwner[] m_droneByIndex;
    public MirrorMono_PublicRsaKeyOwner[] m_droneByRsaPublic;
    public MSoccerMono_FixedSoccerIdTag[] m_droneBySoccerId;
    public MSoccerMono_TrustParserInputInt m_trustParserInputIID;
    public MSoccerMono_AbstractGamepad[] m_droneGamepadInFixedIdOrder; 


    public SoccerIdtoRsaAlias[] m_soccerDronesToAlias; 

    [System.Serializable]
    public class SoccerIdtoRsaAlias { 
        public FixedSoccerId m_fixedSoccerId;
        public MirrorMono_PublicRsaKeyOwner m_rsaOwner;
        public MirrorMono_CallableUniqueAlias m_aliasOwner;
        public MirrorMono_IndexIntegerOwner m_integerOwner;
        public MSoccerMono_AbstractGamepad m_gamepad;

        public FixedSoccerId GetFixedSoccerId() { return m_fixedSoccerId; }
        public string GetRsa() { return m_rsaOwner.m_publicKeyOwner; }
        public int GetIndexIID() { return m_integerOwner.m_indexOfUser; }
        public string[] GetTextAlias() { return m_aliasOwner.m_textAlias; }
        public int[] GetIntegerAlias() { return m_aliasOwner.m_integerIdAlias; }


        public bool IsOwnedBy(int index)
        {
            if (m_integerOwner == null)
                return false;
            return m_integerOwner.m_indexOfUser == index;
        }
        public bool IsOwnedByExactly(string rsa)
        {
            if (m_rsaOwner == null)
                return false;
            return m_rsaOwner.m_publicKeyOwner == rsa;
        }


        public void IsInAlias(string alias, out bool isFound)
        {
            isFound = false;
            if (m_aliasOwner == null)
                return;
            for (int i = 0; i < m_aliasOwner.m_textAlias.Length; i++)
            {
                if (m_aliasOwner.m_textAlias[i] == alias)
                {
                    isFound = true;
                    return;
                }
            } 
            if(int.TryParse(alias, out int aliasAsInteger))
            {
                for (int j = 0; j < m_aliasOwner.m_integerIdAlias.Length; j++)
                {
                    if (m_aliasOwner.m_integerIdAlias[j] == aliasAsInteger)
                    {
                        isFound = true;
                        return;
                    }
                }
            }
          
        }


    }


    public static bool IsExistingAsAlias (string alias)
    {
        if(InstanceInScene == null)
            return false;
        if(InstanceInScene.m_droneByAlias == null)
            return false;
        foreach (var item in InstanceInScene.m_droneByAlias)
        {
            if (item.IsAliasIn(alias, true))
                return true;
        }
        return false;
    }
    public static bool IsExistingAsRsaOwner(string rsaPublicKey)
    {
        if (InstanceInScene == null)
            return false;
        if (InstanceInScene.m_droneByRsaPublic == null)
            return false;
        foreach (var item in InstanceInScene.m_droneByRsaPublic)
        {
            if (item.IsOwnedByExactly(rsaPublicKey))
                return true;
        }
        return false;

    }
    public static bool IsExistingIndexIIDOwner(int index)
    {
        if (InstanceInScene == null)
            return false;
        if (InstanceInScene.m_droneByIndex == null)
            return false;
        foreach (var item in InstanceInScene.m_droneByIndex)
        {
            if (item.IsOwnedBy(index))
                return true;
        }
        return false;
       

    }
    public static bool IsTrustedDroneInteger20To20(int index20to20) {
        if (InstanceInScene == null)
            return false;
        if (InstanceInScene.m_trustParserInputIID == null)
            return false;
        if (InstanceInScene.m_trustParserInputIID.m_dronesToAffect == null)
            return false;
        foreach (var item in InstanceInScene.m_trustParserInputIID.m_dronesToAffect)
        {
            if (item.m_integerUniqueId20to20 == index20to20)
                return true;
        }
        return false;
    }

    public static void GetOwnedSoccerDrone(string rsaKey, out List<FixedSoccerId> ownedDrone)
    {
        ownedDrone = new List<FixedSoccerId>();
        foreach (var item in InstanceInScene.m_soccerDronesToAlias)
        {
            if (item.IsOwnedByExactly(rsaKey))
            {
                ownedDrone.Add(item.GetFixedSoccerId());
            }
        }
       
    }

    public static bool IsOwnerOfFixedSoccerDrone(string rsaKey, FixedSoccerId fixedSoccerId)
    {
        foreach (var item in InstanceInScene.m_soccerDronesToAlias)
        {
            if (item.GetFixedSoccerId() == fixedSoccerId)
            {
                return item.IsOwnedByExactly(rsaKey);
            }
        }
        return false;
    }

    public static bool IsExistingAsAliasAndOwned(string targetDrone, string rsaKey)
    {
        foreach (var item in InstanceInScene.m_soccerDronesToAlias)
        {
            item.IsInAlias(targetDrone, out bool isFound);
            if (isFound)
            {
                return item.IsOwnedByExactly(rsaKey);
            }
        }
        return false;
    }

    public static void TryGetDroneSoccerIdFromAlias(string droneAlias, out bool found, out FixedSoccerId droneSoccer)
    {
        found = false;
        droneSoccer = FixedSoccerId.D1_Red0Stricker;
        foreach (var item in InstanceInScene.m_soccerDronesToAlias)
        {
            item.IsInAlias(droneAlias, out bool isFound);
            if (isFound)
            {
                droneSoccer = item.GetFixedSoccerId();
                found = true;
                return;
            }
        }
    }

    public static void GetDroneSoccerIdGamepad(FixedSoccerId id, out MSoccerMono_AbstractGamepad gamepad)
    {
        foreach (var item in InstanceInScene.m_soccerDronesToAlias)
        {
            if (item.GetFixedSoccerId() == id)
            {
                gamepad = item.m_gamepad;
                return;
            }
        }
        gamepad = null;
    }

   

    public static void GetFirstOwnedSoccerId(string rsa, out bool found, out FixedSoccerId id)
    {
        found = false;
        id = FixedSoccerId.D1_Red0Stricker;
        foreach (var item in InstanceInScene.m_soccerDronesToAlias)
        {
            if (item.IsOwnedByExactly(rsa))
            {
                id = item.GetFixedSoccerId();
                found = true;
                return;
            }
        }
    }

    public static bool IsOwningOneDroneIntegerPlayer(int index)
    {
        foreach (var d in InstanceInScene.m_droneByIndex)
            if (d.IsOwnedBy(index)) return true;
        return false;
    }

    public static void GetOwnedDroneIntegerPlayer(int index, out List<FixedSoccerId> fixedSoccerIds) { 
    
        fixedSoccerIds = new List<FixedSoccerId>();
        foreach (var d in InstanceInScene.m_droneByIndex) {
            if (d.IsOwnedBy(index)) {
                GetFixedSoccierIdFrom(d, out bool hasId, out FixedSoccerId id );
                if(hasId)
                    fixedSoccerIds.Add(id);
            }
        }

    }

    private static void GetFixedSoccierIdFrom(MirrorMono_IndexIntegerOwner d,out bool hasId,  out FixedSoccerId id)
    {
        MSoccerMono_FixedSoccerIdTag tag = d.GetComponent<MSoccerMono_FixedSoccerIdTag>();
        if (tag != null)
        {
            id= tag.GetFixedSoccerId();
            hasId= true;
            return;
        }
        id = FixedSoccerId.D1_Red0Stricker;
        hasId = false;
    }

    public  int[] GetDroneIndexClaim0to11()
    {
        return m_droneByIndex.Select(x =>x==null?0:x.m_indexOfUser).ToArray();
    }
    public FixedSoccerId[] GetDroneOwnedFromIndex(int indexOwner) { 
        List<FixedSoccerId> ids = new List<FixedSoccerId>();
        foreach (var d in m_droneByIndex)
        {
            if (d.m_indexOfUser == indexOwner)
            {
                GetFixedSoccierIdFrom(d, out bool hasId, out FixedSoccerId id);
                if (hasId)
                    ids.Add(id);
            }
        }
        return ids.ToArray();
    }

    public MSoccerMono_AbstractGamepad GetGamepadFromFixedSoccerId( FixedSoccerId m_ownedDrone)
    {
        switch (m_ownedDrone)
        {
            case FixedSoccerId.D1_Red0Stricker: return m_droneGamepadInFixedIdOrder[0];
            case FixedSoccerId.D2_Red1:return m_droneGamepadInFixedIdOrder[1];
            case FixedSoccerId.D3_Red2:
                return m_droneGamepadInFixedIdOrder[2];
            case FixedSoccerId.D4_Red3:
                return m_droneGamepadInFixedIdOrder[3];
            case FixedSoccerId.D5_Red4:
                return m_droneGamepadInFixedIdOrder[4];
            case FixedSoccerId.D6_Red5:
                return m_droneGamepadInFixedIdOrder[5];
            case FixedSoccerId.D7_Blue0Stricker:
                return m_droneGamepadInFixedIdOrder[6];
            case FixedSoccerId.D8_Blue1:
                return m_droneGamepadInFixedIdOrder[7];
            case FixedSoccerId.D9_Blue2:
                return m_droneGamepadInFixedIdOrder[8];
            case FixedSoccerId.D10_Blue3:
                return m_droneGamepadInFixedIdOrder[9];
            case FixedSoccerId.D11_Blue4:
                return m_droneGamepadInFixedIdOrder[10];
            case FixedSoccerId.D12_Blue5:
                return m_droneGamepadInFixedIdOrder[11];
            default:
                break;
        }
        return null;
    }

    public MSoccerMono_AbstractGamepad[] GetGamepadFromFixedSoccerId(FixedSoccerId[] m_ownedDrone)
    {
        return m_ownedDrone.Select(x => GetGamepadFromFixedSoccerId(x)).ToArray();
    }
}
