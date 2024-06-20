using UnityEngine;

public class MSoccerMono_ManualXrSet_Resources : MonoBehaviour
{
    public string m_resourcePath = "IsXrProject";

    public bool m_debugFoundXrResources = false;
    public void Awake()
    {
        bool found = Resources.Load(m_resourcePath);
        if (found)
        {

            MSoccerMono_ManualXrChecker.SetAsXR(true);
            MSoccerMono_ManualXrChecker.SetXrDeviceModel("Undefined XR device");
        }
        else
        {
            MSoccerMono_ManualXrChecker.SetAsXR(false);
            MSoccerMono_ManualXrChecker.SetXrDeviceModel("None XR device");
        }
    }
}

