using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AsteroidInCubeMono : MonoBehaviour
{

    public class AsteroidStartPoint { 
    

    }

    public class AsteroidCapsule { 
    
    }
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}




public class CapsuleLinearProjectileMono : MonoBehaviour{ 


}


public struct CapsuleCurrentState {
    public bool m_isAlive;

}


public struct CLP_FixedInfoAtCreate
{
    public int m_projectilePoolId;
    public int m_projectilePoolType;
    
}

public struct CapsuleLinearProjectile {

    public long m_serverTimeTick;
    public float m_radiusSize;
    public Vector3 m_localPosition;
    public Quaternion m_startDirection;
    public float m_movingSpeedPerSecond;
}

public struct ProjectileCurrentState {

    public Vector3 m_previousPosition;
    public Vector3 m_currentPosition;
}









