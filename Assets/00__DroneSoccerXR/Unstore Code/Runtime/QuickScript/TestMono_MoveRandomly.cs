using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestMono_MoveRandomly : MonoBehaviour
{
    public bool m_useRandomMove;

    public Transform m_whatToMove;
    public Vector3 m_moveDirection;
    public float m_moveSpeed=1;
    public float m_interval = 1;


    void Start()
    {
        InvokeRepeating("ChangeDirection", m_interval, m_interval);    
    }

    public void ChangeDirection() {

        m_moveDirection = new Vector3(UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
    }

    void Update()
    {
        if (m_whatToMove) {

            m_whatToMove.position += m_moveDirection * m_moveSpeed * Time.deltaTime;
        }
    }

    private void Reset()
    {
        ChangeDirection();
        m_whatToMove = this.transform;
    }
}
