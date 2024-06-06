using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoccerStateSyncMono_PushClaimChanged : MonoBehaviour
{
    public DroneSoccerPublicRsaKeyClaim m_publicRsaClaim;
    public DroneSoccerIndexIntegerClaim m_indexIntegerClaim;
    public UnityEvent<DroneSoccerPublicRsaKeyClaim> m_onPublicRsaClaimChanged;
    public UnityEvent<DroneSoccerIndexIntegerClaim> m_onIndexIntegerClaimChanged;

    public MirrorMono_PublicRsaKeyOwner[] m_publicRsaKeyDroneRedOwner = new MirrorMono_PublicRsaKeyOwner[6];
    public MirrorMono_PublicRsaKeyOwner[] m_publicRsaKeyDroneBlueOwner = new MirrorMono_PublicRsaKeyOwner[6];
    public MirrorMono_IndexIntegerOwner[] m_indexIntegerDroneRedOwner = new MirrorMono_IndexIntegerOwner[6];
    public MirrorMono_IndexIntegerOwner[] m_indexIntegerDroneBlueOwner = new MirrorMono_IndexIntegerOwner[6];


    public void Awake()
    {
        Refresh();
        Invoke("Refresh", 5);
    
    }

    [ContextMenu("Refresh")]
    public void Refresh() { 
    

        m_publicRsaClaim.m_blueDrone0Stricker = m_publicRsaKeyDroneBlueOwner[0].m_publicKeyOwner;
        m_publicRsaClaim.m_blueDrone1 = m_publicRsaKeyDroneBlueOwner[1].m_publicKeyOwner;
        m_publicRsaClaim.m_blueDrone2 = m_publicRsaKeyDroneBlueOwner[2].m_publicKeyOwner;
        m_publicRsaClaim.m_blueDrone3 = m_publicRsaKeyDroneBlueOwner[3].m_publicKeyOwner;
        m_publicRsaClaim.m_blueDrone4 = m_publicRsaKeyDroneBlueOwner[4].m_publicKeyOwner;
        m_publicRsaClaim.m_blueDrone5 = m_publicRsaKeyDroneBlueOwner[5].m_publicKeyOwner;

        m_publicRsaClaim.m_redDrone0Stricker = m_publicRsaKeyDroneRedOwner[0].m_publicKeyOwner;
        m_publicRsaClaim.m_redDrone1 = m_publicRsaKeyDroneRedOwner[1].m_publicKeyOwner;
        m_publicRsaClaim.m_redDrone2 = m_publicRsaKeyDroneRedOwner[2].m_publicKeyOwner;
        m_publicRsaClaim.m_redDrone3 = m_publicRsaKeyDroneRedOwner[3].m_publicKeyOwner;
        m_publicRsaClaim.m_redDrone4 = m_publicRsaKeyDroneRedOwner[4].m_publicKeyOwner;
        m_publicRsaClaim.m_redDrone5 = m_publicRsaKeyDroneRedOwner[5].m_publicKeyOwner;


        m_indexIntegerClaim.m_blueDrone0Stricker = m_indexIntegerDroneBlueOwner[0].m_indexOfUser;
        m_indexIntegerClaim.m_blueDrone1 = m_indexIntegerDroneBlueOwner[1].m_indexOfUser;
        m_indexIntegerClaim.m_blueDrone2 = m_indexIntegerDroneBlueOwner[2].m_indexOfUser;
        m_indexIntegerClaim.m_blueDrone3 = m_indexIntegerDroneBlueOwner[3].m_indexOfUser;
        m_indexIntegerClaim.m_blueDrone4 = m_indexIntegerDroneBlueOwner[4].m_indexOfUser;
        m_indexIntegerClaim.m_blueDrone5 = m_indexIntegerDroneBlueOwner[5].m_indexOfUser;

        m_indexIntegerClaim.m_redDrone0Stricker = m_indexIntegerDroneRedOwner[0].m_indexOfUser;
        m_indexIntegerClaim.m_redDrone1 = m_indexIntegerDroneRedOwner[1].m_indexOfUser;
        m_indexIntegerClaim.m_redDrone2 = m_indexIntegerDroneRedOwner[2].m_indexOfUser;
        m_indexIntegerClaim.m_redDrone3 = m_indexIntegerDroneRedOwner[3].m_indexOfUser;
        m_indexIntegerClaim.m_redDrone4 = m_indexIntegerDroneRedOwner[4].m_indexOfUser;
        m_indexIntegerClaim.m_redDrone5 = m_indexIntegerDroneRedOwner[5].m_indexOfUser;





        m_onPublicRsaClaimChanged.Invoke(m_publicRsaClaim);
        m_onIndexIntegerClaimChanged.Invoke(m_indexIntegerClaim);

    }
    
}
