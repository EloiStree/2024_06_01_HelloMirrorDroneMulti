using System;
using System.Collections.Generic;
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
        m_trustParserInputIID = GameObject.FindObjectOfType<MSoccerMono_TrustParserInputInt>(false);
    }

    public MirrorMono_CallableUniqueAlias[] m_droneByAlias;
    public MirrorMono_IndexIntegerOwner[] m_droneByIndex;
    public MirrorMono_PublicRsaKeyOwner[] m_droneByRsaPublic;
    public MSoccerMono_TrustParserInputInt m_trustParserInputIID;


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

    internal static void GetOwnedSoccerDrone(out List<FixedSoccerId> ownedDrone)
    {
        throw new NotImplementedException();
    }

    internal static void GetOwnedSoccerDrone(string rsaKey, out List<FixedSoccerId> ownedDrone)
    {
        throw new NotImplementedException();
    }

    internal static bool IsOwnerOfFixedSoccerDrone(string rsaKey, FixedSoccerId fixedSoccerId)
    {
        throw new NotImplementedException();
    }
}