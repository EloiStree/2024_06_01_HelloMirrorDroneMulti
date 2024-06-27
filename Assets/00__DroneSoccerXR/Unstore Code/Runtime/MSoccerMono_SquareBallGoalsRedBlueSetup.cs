using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_SquareBallGoalsRedBlueSetup : MonoBehaviour
{
    public Transform m_rootCenterField;
    public Transform m_goalRed;
    public Transform m_goalBlue;
    public float m_goalDistanceOfCenterMeter=8;
    public float m_goalGroundHeightMeter=4;
    public float m_goalWidthRadiusMeter=12;
    public float m_goalDepthMeter=10;

    public bool m_useAwake = true;

    private void Awake()
    {
        RefreshSetup();
    }

    [ContextMenu("Refresh Setup")]
    public void RefreshSetup()
    {
        if (m_rootCenterField == null)
            return;
        if (m_goalRed == null)
            return;
        if (m_goalBlue == null)
            return;

        m_goalRed.position = m_rootCenterField.position + m_rootCenterField.right * m_goalDistanceOfCenterMeter;
        m_goalBlue.position = m_rootCenterField.position + m_rootCenterField.right * -m_goalDistanceOfCenterMeter;
        m_goalRed.position += m_rootCenterField.up * m_goalGroundHeightMeter / 2f;
        m_goalBlue.position += m_rootCenterField.up * m_goalGroundHeightMeter / 2f;

        m_goalRed.rotation = m_rootCenterField.rotation * Quaternion.Euler(0, -90, 0);
        m_goalBlue.rotation = m_rootCenterField.rotation * Quaternion.Euler(0, 90, 0);

        m_goalRed.localScale = new Vector3(m_goalWidthRadiusMeter, m_goalGroundHeightMeter, m_goalDepthMeter);
        m_goalBlue.localScale = new Vector3(m_goalWidthRadiusMeter, m_goalGroundHeightMeter, m_goalDepthMeter);
    }

    private void OnValidate()
    {
      RefreshSetup();
    }
    void Update()
    {
        
    }
}
