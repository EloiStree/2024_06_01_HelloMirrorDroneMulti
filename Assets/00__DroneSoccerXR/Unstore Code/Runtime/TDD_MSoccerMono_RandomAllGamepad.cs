using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TDD_MSoccerMono_RandomAllGamepad : MonoBehaviour
{

    public MSoccerMono_AbstractGamepad[] m_gamepads;

    [ContextMenu("Fetch Gamepad in scene")]
    public void RefreshWithGamepadInScene() {

        m_gamepads = GameObject.FindObjectsByType<MSoccerMono_AbstractGamepad>(FindObjectsSortMode.None);


    }
    public float m_timeBetweenChange = 0.1f;

    IEnumerator Start()
    {
        while (true)
        {


            RefreshWithGamepadInScene();

            yield return new WaitForEndOfFrame();
            yield return new WaitForSeconds(m_timeBetweenChange);
            foreach (var m in m_gamepads)
            {
                SetRandomInput(m);

            }
        }
    }

    private void SetRandomInput(MSoccerMono_AbstractGamepad m)
    {
        if (m == null)
            return;

        m.SetHorizontalRotation(UnityEngine.Random.Range(-1.0f, 1.0f));
        m.SetHorizontaMove(UnityEngine.Random.Range(-1.0f, 1.0f));
        m.SetVerticalMove(UnityEngine.Random.Range(-1.0f, 1.0f));
        m.SetFrontalMove(UnityEngine.Random.Range(-1.0f, 1.0f));
       
    }
}
