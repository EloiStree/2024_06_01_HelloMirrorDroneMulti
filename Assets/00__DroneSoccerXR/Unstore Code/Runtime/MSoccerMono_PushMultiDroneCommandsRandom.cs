using System.Collections;
using UnityEngine;

public class MSoccerMono_PushMultiDroneCommandsRandom : MonoBehaviour{

    public MSoccerMono_PushMultiDroneCommands m_pushMultiDroneCommands;
    public string [] m_targetAlias;
    public int [] m_targetIntAlias;
    public int m_interval = 1;

    public void Start()
    {
        StartCoroutine(StartRandom());
    }

    private IEnumerator StartRandom()
    {
        while (true)
        {

            if(m_pushMultiDroneCommands==null)
            {
                m_pushMultiDroneCommands = MSoccerMono_PushMultiDroneCommands.OwnedPusherInScene;
            }
            if(m_pushMultiDroneCommands != null)
            {
                foreach (var item in m_targetAlias)
                {
                    m_pushMultiDroneCommands.PushDroneCommandsToDroneAlias(item, UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
                }
                //foreach(var item in m_targetIntAlias)
                //{
                //    m_pushMultiDroneCommands.PushDroneCommandsToIntAlias(item, UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f), UnityEngine.Random.Range(-1f, 1f));
                //}
            }
            yield return new WaitForSeconds(m_interval);
            yield return new WaitForEndOfFrame();
        }
    }
}

