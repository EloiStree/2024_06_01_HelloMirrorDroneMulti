using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class MSoccerMono_LoopTick : MonoBehaviour
{

    public UnityEvent m_tick;
    public float m_timeBeforeStart=1f;
    public float m_timeBetweenTick=0.1f;
    public string m_lastTick;


    public Coroutine m_coroutine;

    private void OnEnable()
    {
        if(m_coroutine != null)
            StopCoroutine(m_coroutine);
        StartCoroutine (StartLoop());
    }
    private void OnDisable()
    {
        if(m_coroutine != null)
            StopCoroutine(m_coroutine); 
    }

    IEnumerator  StartLoop()
    {
        yield return new WaitForSeconds(m_timeBeforeStart);

        while (true)
        {
            Tick();
            yield return new WaitForSeconds(m_timeBetweenTick);
            yield return new WaitForEndOfFrame();
        }
    }

    [ContextMenu("Invoke Tick")]
    private void Tick()
    {
        m_tick.Invoke();
        m_lastTick = DateTime.Now.ToString();
    }
}
