
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SoccerStateSyncMono_PushPosition : MonoBehaviour
{

    public S_DroneSoccerPositions m_positions;
    public UnityEvent<S_DroneSoccerPositions> m_onPositionsChanged;

    public Transform m_centerOfArena;
    public Transform[] m_dronePositionRed = new Transform[6];
    public Transform[] m_dronePositionBlue = new Transform[6];
    public int m_framePerSeconds = 12;


    public long m_framePushed=0;
    
    public IEnumerator Start()
    {
        while (true)
        {
            m_positions.m_dateTimeUtcTick =(ulong) DateTime.UtcNow.Ticks;
            m_positions.m_framePushed =(ulong) m_framePushed++; 
            SetPosition(ref m_positions.m_redDrone0Stricker, m_dronePositionRed[0]);
            SetPosition(ref m_positions.m_redDrone1, m_dronePositionRed[1]);
            SetPosition(ref m_positions.m_redDrone2, m_dronePositionRed[2]);
            SetPosition(ref m_positions.m_redDrone3, m_dronePositionRed[3]);
            SetPosition(ref m_positions.m_redDrone4, m_dronePositionRed[4]);
            SetPosition(ref m_positions.m_redDrone5, m_dronePositionRed[5]);
            SetPosition(ref m_positions.m_blueDrone0Stricker, m_dronePositionBlue[0]);
            SetPosition(ref m_positions.m_blueDrone1, m_dronePositionBlue[1]);
            SetPosition(ref m_positions.m_blueDrone2, m_dronePositionBlue[2]);
            SetPosition(ref m_positions.m_blueDrone3, m_dronePositionBlue[3]);
            SetPosition(ref m_positions.m_blueDrone4, m_dronePositionBlue[4]);
            SetPosition(ref m_positions.m_blueDrone5, m_dronePositionBlue[5]);
            m_onPositionsChanged.Invoke(m_positions);
            yield return new WaitForSeconds(1f / m_framePerSeconds);
        }
        
    }

    private void SetPosition(ref S_DronePositionCompressed drone, Transform transform)
    {
        MSoccer_RelocationUtility.GetWorldToLocal_DirectionalPoint(
            transform.position,
            transform.rotation,
            m_centerOfArena,
            out Vector3 localPosition,
            out Quaternion localRotation
            );
        drone.SetPosition(localPosition);
        drone.SetRotation(localRotation);
    }

}
