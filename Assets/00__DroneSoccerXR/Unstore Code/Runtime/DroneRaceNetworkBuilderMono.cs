using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.Events;

public class DroneRaceNetworkBuilderMono : MonoBehaviour
{

    [Tooltip("Must have a MSoccerMono_SpawnStepPrefab script on it")]
    public GameObject m_prefabToSpawn;

    [Tooltip("List of prefab that can be replace in the game.")]
    public List<GameObject> m_replacablePrefab;

    public Transform m_whereToReplace;

    public List<DFlagMono_RootTag> found;

    public UnityEvent m_beforeLoading;
    public UnityEvent m_whenLoaded;



    [ContextMenu("Refresh list of prefab")]
    public void RefreshListOfPrefab() {

        MSoccerMono_SpawnStepPrefab script = m_prefabToSpawn.GetComponent<MSoccerMono_SpawnStepPrefab>();
        m_replacablePrefab = script.m_prefabToSpawn;
    }

    [ContextMenu("Parse Single player map to multiplayer map on server")]
    public void LoadSinglePlayerMapToMultiplayer()
    {
        m_beforeLoading.Invoke();
        List<DFlagMono_RootTag>  o = GetAllGameObjectInLevel();
        foreach (var item in m_replacablePrefab)
        {
            DFlagMono_RootTag tag = item.GetComponent<DFlagMono_RootTag>();
            if (tag == null)
                continue;

            string guid = tag.GetGuid();
            foreach (var objectInLevel in o)
            {
                if (objectInLevel.GetGuid()== guid) {

                    GameObject created = GameObject.Instantiate(m_prefabToSpawn);
                    created.SetActive(true);
                    MSoccerMono_SpawnStepPrefab script = created.GetComponent<MSoccerMono_SpawnStepPrefab>();
                    script.SetModelFromName(item.name);
                    script .m_worldPosition= objectInLevel.transform.position;
                    script.m_worldRotation = objectInLevel.transform.rotation;
                    script .m_worldScale= objectInLevel.transform.localScale;
                    //script.UpdateOnClientsCurrentTransform();
                }
            }
        }
        m_whenLoaded.Invoke();
    }

   
    List<DFlagMono_RootTag>  GetAllGameObjectInLevel()
    {
        found = new List<DFlagMono_RootTag>();
        m_whereToReplace.gameObject.GetComponentsInChildren(found);
        return found;
    }
}
