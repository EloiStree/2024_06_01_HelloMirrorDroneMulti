using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_LoopTick : MonoBehaviour
{

    public UnityEvent m_tick;
    public float m_timeBeforeStart=1f;
    public float m_timeBetweenTick=0.1f;

    IEnumerator Start()
    {
        yield return new WaitForSeconds(m_timeBeforeStart);

        while (true) {
            m_tick.Invoke();
            yield return new WaitForSeconds(m_timeBetweenTick);
            yield return new WaitForEndOfFrame();
        }
    }
}
