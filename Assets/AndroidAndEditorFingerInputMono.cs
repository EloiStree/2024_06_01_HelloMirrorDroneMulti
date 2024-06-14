using UnityEngine;
using UnityEngine.Events;

public class AndroidAndEditorFingerInputMono : MonoBehaviour
{
    public bool m_useEditorDebug=true;
    public UnityEvent<int> m_onUnityCount;
    public UnityEvent<Vector2> m_onScreenPositionFingerOne;
    public UnityEvent<Vector2> m_onScreenPositionFingerTwo;

    private int m_touchCount;
    public bool m_mouseDown0;
    public bool m_mouseDown1;
    public void Update()
    {
        bool isInEditor = false;
#if UNITY_EDITOR
        isInEditor = true;
#endif
        if (isInEditor && m_useEditorDebug)
        {
            m_mouseDown0 = Input.GetMouseButton(0);
            m_mouseDown1 = Input.GetMouseButton(1);
            int c = (m_mouseDown0?1:0)+ (m_mouseDown1?1:0);
           
            if (m_touchCount == 1)
            {
                m_onScreenPositionFingerOne.Invoke(Input.mousePosition);
            }
            else if (m_touchCount == 2)
            {
                m_onScreenPositionFingerTwo.Invoke(Input.mousePosition);
            }
            else { 
            
                m_onScreenPositionFingerOne.Invoke(Vector2.zero);
                m_onScreenPositionFingerTwo.Invoke(Vector2.zero);
            }
            if (c != m_touchCount)
            {
                m_touchCount = c;
                m_onUnityCount.Invoke(m_touchCount);
            }
        }
        else {

            int touch = Input.touchCount;
            if(touch != m_touchCount)
            {
                m_onUnityCount.Invoke(m_touchCount);
                m_touchCount = touch;
            }

            if(m_touchCount==0)
            {
                m_onScreenPositionFingerOne.Invoke(Vector2.zero);
                m_onScreenPositionFingerTwo.Invoke(Vector2.zero);
            }
          
            else if(m_touchCount >= 2)
            {
                Vector3 left = Input.GetTouch(0).position;
                Vector3 right = Input.GetTouch(1).position;
                if (right.x < left.x)
                {
                    m_onScreenPositionFingerOne.Invoke(right);
                    m_onScreenPositionFingerTwo.Invoke(left);
                }
                else { 
                    m_onScreenPositionFingerOne.Invoke(left);
                    m_onScreenPositionFingerTwo.Invoke(right);
                }
            }
        }


    }

}