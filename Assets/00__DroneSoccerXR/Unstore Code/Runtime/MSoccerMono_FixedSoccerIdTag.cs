using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MSoccerMono_FixedSoccerIdTag : MonoBehaviour
{

    public FixedSoccerId m_fixedSoccerId ;


    public FixedSoccerId GetFixedSoccerId() {
        return m_fixedSoccerId;
    }
    public void GetFixedSoccerId(out FixedSoccerId fixedSoccerId) {
        fixedSoccerId = m_fixedSoccerId;
    }
}
