using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class AsteroidToByteEventMono : MonoBehaviour
{
    public byte m_createdStartByte;
    public byte m_destroyedStartByte;

    public UnityEvent<byte[]> m_onByteToPushed;

    public void PushAsteroidCreated(AsteroidCreationEvent created)
    {
        byte[] bytes = new byte[1+ 9*4 + 2*4 + 8];
        bytes[0] = m_createdStartByte;
        BitConverter.GetBytes(created.m_startPosition.x).CopyTo(bytes, 1);
        BitConverter.GetBytes(created.m_startPosition.y).CopyTo(bytes, 5);
        BitConverter.GetBytes(created.m_startPosition.z).CopyTo(bytes, 9);
        BitConverter.GetBytes(created.m_startRotationEuler.x).CopyTo(bytes, 13);
        BitConverter.GetBytes(created.m_startRotationEuler.y).CopyTo(bytes, 17);
        BitConverter.GetBytes(created.m_startRotationEuler.z).CopyTo(bytes, 21);
        BitConverter.GetBytes(created.m_startDirection.x).CopyTo(bytes, 25);
        BitConverter.GetBytes(created.m_startDirection.y).CopyTo(bytes, 29);
        BitConverter.GetBytes(created.m_startDirection.z).CopyTo(bytes, 33);
        BitConverter.GetBytes(created.m_speedInMetersPerSecond).CopyTo(bytes, 37);
        BitConverter.GetBytes(created.m_colliderRadius).CopyTo(bytes, 41);
        BitConverter.GetBytes(created.m_serverUtcNowTicks).CopyTo(bytes, 45);
        m_onByteToPushed.Invoke(bytes);
    }

    public void PushAsteroidDestroy(AsteroidDestructionEvent destroyed) { 
        byte[] bytes = new byte[1+2*4+8];
        bytes[0] = m_destroyedStartByte;
        BitConverter.GetBytes(destroyed.m_poolId).CopyTo(bytes, 1);
        BitConverter.GetBytes(destroyed.m_poolItemIndex).CopyTo(bytes, 5);
        BitConverter.GetBytes(destroyed.m_serverUtcNowTicks).CopyTo(bytes, 9);
        m_onByteToPushed.Invoke(bytes);
    }
}
