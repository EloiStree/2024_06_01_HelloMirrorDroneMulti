using UnityEngine;

public class MSoccerMono_ManualXrChecker_SetXR : MonoBehaviour {

    public bool m_isXrProject = true;
    public string m_deviceModel = "Undefined XR Device";

    public void Awake()
    {
        MSoccerMono_ManualXrChecker.SetAsXR(m_isXrProject);
        MSoccerMono_ManualXrChecker.SetXrDeviceModel(m_deviceModel);
    }
}


