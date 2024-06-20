using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_CameraViewPref : MonoBehaviour
{

    public GameObject m_waitingView;
    public GameObject m_view12Camera;
    public GameObject m_viewFollowDrone;
    public FollowDroneTransformLerpMono m_droneToFollow;
    public MSoccerMono_IsDroneExistingOnClient m_droneInClientScene;


    public int m_droneId;
    private int m_previousDroneID;


    public bool m_useAutoRefresh = true;
    public void Awake()
    {
       if(m_useAutoRefresh)
            InvokeRepeating("RefreshWithInspector", 0, 1);
    }

    private void OnValidate()
    {
        if (m_droneId != m_previousDroneID) {

            m_previousDroneID = m_droneId;
            SetCameraOnGivenDroneId(m_droneId);
            
           
        }
    }


    [ContextMenu("Refresh")]
    public void RefreshWithInspector() { 
    
        SetCameraOnGivenDroneId(m_droneId);
    }

    public string m_currentRsaDebug;
    public List<FixedSoccerId> m_ownedDroneDebug;
    public void SetCameraOnGivenDroneId(int droneId)
    {

        MirrorRsaPlayerOnNetworkRef player = MirrorRsaPlayerOnNetworkRefDico.GetCurrentPlayer();
        if (player == null || ! player.IsPlayerStillValide()) {
            SetWaitingViewMode();
            return;
        }

        m_currentRsaDebug = player.GetPublicKey();
        MSoccerMono_IsDroneExistingOnClient.GetOwnedSoccerDrone(m_currentRsaDebug, out m_ownedDroneDebug);


        if (droneId == 404 || droneId==-404) {
            SetAsNoViewMode();
        }
        else if (droneId < -20 || droneId > 20)
        {
            SetAsViewAllMode();
            return;
        }
        else {
            if (droneId > 0 && droneId <= 12)
            {
                FixedSoccerId id = (FixedSoccerId)droneId;
                SetWithFixeSoccerId(id);
            }
            else {
                if (droneId == 0) {
                    MSoccerMono_IsDroneExistingOnClient.GetFirstOwnedSoccerId(m_currentRsaDebug, out bool found, out FixedSoccerId id);
                    SetWithFixeSoccerId(id);

                }
                else {
                    int ownedIdIndex = (droneId+1) * -1;
                    if(ownedIdIndex < m_ownedDroneDebug.Count)
                    {
                        SetWithFixeSoccerId(m_ownedDroneDebug[ownedIdIndex]);
                    }
                    else {
                        SetWaitingViewMode();
                    }
                }

            }
        }
    }

    private void SetWithFixeSoccerId(FixedSoccerId id)
    {
        MSoccerMono_IsDroneExistingOnClient.GetDroneSoccerIdGamepad(id, out MSoccerMono_AbstractGamepad gamepad);
        if (gamepad != null)
        {
            MSoccerMono_FixedSoccerIdTag root = gamepad.GetComponentInParent<MSoccerMono_FixedSoccerIdTag>();
            if (root != null)
            {
                Rigidbody rb = root.GetComponentInChildren<Rigidbody>();
                if(rb != null)
                {
                    m_droneToFollow.m_target = rb.transform;
                    SetAsOneCameraMode();
                }
                else {
                    m_droneToFollow.m_target = gamepad.transform;
                    SetAsOneCameraMode();
                }
            }
        }
    }

    private void SetWaitingViewMode()
    {
        SetAsNoViewMode();
        m_waitingView.SetActive(true);
    }

    [ContextMenu("One Camera mode")]
    private void SetAsOneCameraMode()
    {
        SetAsNoViewMode();
        m_viewFollowDrone.SetActive(true);
    }

    [ContextMenu("View all mode")]
    private void SetAsViewAllMode()
    {
        SetAsNoViewMode();
        m_view12Camera.SetActive(true);
    }
    [ContextMenu("No view mode")]
    private void SetAsNoViewMode()
    {
        m_view12Camera.SetActive(false);
        m_viewFollowDrone.SetActive(false);
        m_waitingView.SetActive(false);
    }
}
