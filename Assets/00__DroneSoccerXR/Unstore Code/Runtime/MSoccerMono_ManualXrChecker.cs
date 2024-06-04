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
    public UnityEvent m_onIsXR;
    public UnityEvent m_onIsNotXR;


    private void Start()
    {
        m_onXrProject.Invoke(m_isInXrProject);
        m_onDeviceModel.Invoke(m_deviceModel);
        if(m_isInXrProject)
            m_onIsXR.Invoke();
        else
            m_onIsNotXR.Invoke();
    }

    public static void SetAsXR(bool isXrProject)
    {
        m_isInXrProject = isXrProject;
    }

    public static void SetXrDeviceModel(string deviceModel)
    {
        m_deviceModel=deviceModel;
    }
}


