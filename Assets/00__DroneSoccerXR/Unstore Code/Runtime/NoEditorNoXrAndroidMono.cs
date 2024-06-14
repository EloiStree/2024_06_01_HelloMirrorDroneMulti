using UnityEngine;
using UnityEngine.Events;

public class NoEditorNoXrAndroidMono : MonoBehaviour {

    public UnityEvent<bool> m_isNoEditorNoXrAndroidBool;
    public UnityEvent<bool> m_isNoEditorXrAndroidBool;
    public UnityEvent m_isNoEditorNoXrAndroidTrue;
    public UnityEvent m_isNoEditorXrAndroidTrue;

    public bool m_autoCheckAtAwake=true;
    public void Awake()
    {
        if (m_autoCheckAtAwake)
        {
            Check();
        }
    }
    [ContextMenu("Check")]
    public void Check() { 
    
        if(Application.isEditor==false && Application.platform==RuntimePlatform.Android && !MSoccerMono_ManualXrChecker.IsXrProject())
        {
            m_isNoEditorNoXrAndroidBool.Invoke(true);
            m_isNoEditorNoXrAndroidTrue.Invoke();
        }
        else if (Application.isEditor == false && Application.platform == RuntimePlatform.Android && MSoccerMono_ManualXrChecker.IsXrProject())
        {
            m_isNoEditorXrAndroidBool.Invoke(true);
            m_isNoEditorXrAndroidTrue.Invoke();
        }
        else
        {
            m_isNoEditorNoXrAndroidBool.Invoke(false);
        }
    }
}