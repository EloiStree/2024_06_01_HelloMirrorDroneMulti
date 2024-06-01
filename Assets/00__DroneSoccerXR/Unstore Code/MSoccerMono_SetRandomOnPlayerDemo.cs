using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror.Examples.Basic;
using Mirror;

public class MSoccerMono_SetRandomOnPlayerDemo : NetworkBehaviour
{
    public MyMirrorPlayer m_playerToAffect;

    IEnumerator Start()
    {

         if (this.isServer) { 
            while (true) {

                yield return new WaitForSeconds(1);
                yield return new WaitForEndOfFrame();
                m_playerToAffect.playerColor = new Color(Random.value, Random.value, Random.value, 1);
            }
         }
        yield return new WaitForEndOfFrame();
    }

    private void Reset()
    {
        m_playerToAffect = GetComponent<MyMirrorPlayer>();
    }

}
