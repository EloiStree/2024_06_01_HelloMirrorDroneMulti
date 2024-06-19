using UnityEngine;
using UnityEngine.Events;

public class NoEditorNoXrAndroidMono : MonoBehaviour {

    public UnityEvent<bool> m_isNoEditorNoXrAndroidBool;
    public UnityEvent<bool> m_isNoEditorXrAndroidBool;
    public UnityEvent m_isNoEditorNoXrAndroidTrue;
    public UnityEvent m_isNoEditorXrAndroidTrue;


    public bool m_autoCheckAtAwake=true;
    public bool m_isEditorDebug = false;
    public bool m_isAndroidDebug = false;
    public bool m_isXRDebug = false;
    public void Awake()
    {
        if (m_autoCheckAtAwake)
        {
            Check();
        }
    }
    [ContextMenu("Check")]
    public void Check() {

        bool isEditor = false;
#if UNITY_EDITOR
        isEditor = true;
        #endif
        m_isEditorDebug = isEditor; 
        m_isAndroidDebug = Application.platform == RuntimePlatform.Android;
        m_isXRDebug = MSoccerMono_ManualXrChecker.IsXrProject();
        if(m_isEditorDebug && m_isAndroidDebug && !m_isXRDebug)
        {
            m_isNoEditorNoXrAndroidBool.Invoke(true);
            m_isNoEditorNoXrAndroidTrue.Invoke();
        }
        else if (m_isEditorDebug && m_isAndroidDebug && m_isXRDebug)
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