using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class DroneRaceNetworkBuilderMono : MonoBehaviour
{

    [Tooltip("Must have a MSoccerMono_SpawnStepPrefab script on it")]
    public GameObject m_prefabToSpawn;

    [Tooltip("List of prefab that can be replace in the game.")]
    public List<GameObject> m_replacablePrefab;

    public Transform m_whereToReplace;

    public List<Transform> found;


    


    [ContextMenu("Refresh list of prefab")]
    public void RefreshListOfPrefab() {

        MSoccerMono_SpawnStepPrefab script = m_prefabToSpawn.GetComponent<MSoccerMono_SpawnStepPrefab>();
        m_replacablePrefab = script.m_prefabToSpawn;
    }

    [ContextMenu("Parse Single player map to multiplayer map on server")]
    public void LoadSinglePlayerMapToMultiplayer()
    {
        List<Transform>  o = GetAllGameObjectInLevel();
        foreach (var item in m_replacablePrefab)
        {
            string name = item.name;
            foreach (var objectInLevel in o)
            {
                if (objectInLevel.name.IndexOf(name) ==0) {

                    GameObject created = GameObject.Instantiate(m_prefabToSpawn);
                    created.SetActive(true);
                    MSoccerMono_SpawnStepPrefab script = created.GetComponent<MSoccerMono_SpawnStepPrefab>();
                    script.SetModelFromName(name);
                    created.transform.position = objectInLevel.position;
                    created.transform.rotation = objectInLevel.rotation;
                    created.transform.localScale = objectInLevel.localScale;
                    script.UpdateOnClientsCurrentTransform();
                }
            }
        }
    }

   
    List<Transform>  GetAllGameObjectInLevel()
    {
        found = new List<Transform>();
        m_whereToReplace.gameObject.GetComponentsInChildren(found);
        return found;
    }
}
