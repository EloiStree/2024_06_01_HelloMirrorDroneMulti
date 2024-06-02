using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;

public class MSoccerMono_XRModeChecker : MonoBehaviour
{

    public UnityEvent m_onNotXr;
    public UnityEvent m_onXr;
    public UnityEvent<bool> m_onDeviceXrFound;
    public UnityEvent<bool> m_onDeviceXrNotFound;


    void Start()
    {
        CheckXRMode();
    }

    void CheckXRMode()
    {
        if (XRSettings.isDeviceActive)
        {
            m_onXr.Invoke();
            m_onDeviceXrFound.Invoke(true);
            m_onDeviceXrNotFound.Invoke(false);
            Debug.Log("XR device is active.");
            Debug.Log("Loaded XR device: " + XRSettings.loadedDeviceName);
        }
        else
        {
            m_onNotXr.Invoke();
            m_onDeviceXrFound.Invoke(false);
            m_onDeviceXrNotFound.Invoke(true);
            Debug.Log("No XR device found.");
        }
    }

}
