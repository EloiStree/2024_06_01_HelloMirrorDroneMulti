
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoccerStateSyncMono_PushDimension : MonoBehaviour, MSoccerMono_RefreshablePush
{


    public S_DroneSoccerMatchStaticInformation m_arenaInformation;
    public UnityEvent<S_DroneSoccerMatchStaticInformation> m_onArenaInformationChanged;

    public Transform m_arenaCenter;
    public Transform m_areneTopRightFrontCorner;

    public Transform m_goalCenter;
    public Transform m_innerAnchor;
    public Transform m_outerAnchor;
    public float m_sphereRadius = 0.4f;


    public MSoccerMono_MaxTimeForMatch m_maxTimeMatch;
    public MSoccerMono_WinSetByPoint m_winSetByPoint;
    public MSoccerMono_WinSetByTime m_winSetByTime;



    public Vector3 m_localPosition = Vector3.zero;

    public void Start()
    {
        RefreshAndPush();
        Invoke("RefreshAndPush", 5);
    }


    [ContextMenu("Refresh Push")]
    public void RefreshAndPush()
    {

        m_arenaInformation.m_numberOfPointsToForceWinSet = m_winSetByPoint.m_scoreToWinSet;
        m_arenaInformation.m_numberOfSetsToWinMatch = m_winSetByPoint.m_setToWinMatch;
        m_arenaInformation.m_maxTimingOfSetInSeconds = m_winSetByTime.m_timeToWinSetInSeconds;
        m_arenaInformation.m_maxTimingOfMatchInSeconds = m_maxTimeMatch.m_maxTimeForMatchInSeconds;
        MSoccer_RelocationUtility.GetWorldToLocal_Point(
            m_areneTopRightFrontCorner.position,
            m_arenaCenter,
            out m_localPosition
            );
        MSoccer_RelocationUtility.GetWorldToLocal_Point(
            m_goalCenter.position,
            m_arenaCenter,
            out Vector3 goalLocalPosition
            );
        MSoccer_RelocationUtility.GetWorldToLocal_Point(
            m_innerAnchor.position,
            m_goalCenter,
            out Vector3 goalInnerAnchor
            );
        MSoccer_RelocationUtility.GetWorldToLocal_Point(
            m_outerAnchor.position,
            m_goalCenter,
            out Vector3 goalOutterAnchor
            );

        m_arenaInformation.m_arenaWidthMeter = m_localPosition.x;
        m_arenaInformation.m_arenaHeightMeter = m_localPosition.y;
        m_arenaInformation.m_arenaDepthMeter = m_localPosition.z;
        m_arenaInformation.m_droneSphereRadiusMeter = m_sphereRadius;

        m_arenaInformation.m_goalDistanceOfCenterMeter = goalLocalPosition.x;
        m_arenaInformation.m_goalCenterHeightMeter = goalLocalPosition.y;
        m_arenaInformation.m_goalDepthMeter = Math.Abs(goalInnerAnchor.z);
        m_arenaInformation.m_goalInnerRadiusMeter = Math.Abs(goalInnerAnchor.y);
        m_arenaInformation.m_goalOuterRadiusMeter = Math.Abs(goalOutterAnchor.y);


        m_onArenaInformationChanged.Invoke(m_arenaInformation);

    }
}

