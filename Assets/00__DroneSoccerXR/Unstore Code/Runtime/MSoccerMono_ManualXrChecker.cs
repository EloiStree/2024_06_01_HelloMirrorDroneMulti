using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_ManualXrChecker : MonoBehaviour
{
    public static bool m_isInXrProject=false;
    public static string m_deviceModel="";

    public UnityEvent<bool> m_onXrProject;
    public UnityEvent<string> m_onDeviceModel;
    public UnityEvent<bool> m_onIsXR;
    public UnityEvent<bool> m_onIsNotXR;


    private void Start()
    {
        m_onXrProject.Invoke(m_isInXrProject);
        m_onDeviceModel.Invoke(m_deviceModel);
        if (m_isInXrProject)
        {
            m_onIsXR.Invoke(true);
            m_onIsNotXR.Invoke(false);
        }

        else
        {
            m_onIsXR.Invoke(false);
            m_onIsNotXR.Invoke(true);
        }
    }

    public static void SetAsXR(bool isXrProject)
    {
        m_isInXrProject = isXrProject;
    }

    public static void SetXrDeviceModel(string deviceModel)
    {
        m_deviceModel=deviceModel;
    }

    public static bool IsXrProject() { 
    return m_isInXrProject; 
    }
}


