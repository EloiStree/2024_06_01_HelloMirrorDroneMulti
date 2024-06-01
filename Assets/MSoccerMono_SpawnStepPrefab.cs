using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;

public class MSoccerMono_SpawnStepPrefab : NetworkBehaviour
{
    [SyncVar(hook = nameof(ChangeModelOfPrefab))]
    public int m_prefabIdToSpawn=0;
    public Transform m_whereToCreate;
    public List<GameObject> m_prefabToSpawn;


    [SyncVar(hook = nameof(SomethingChange))]
    public Vector3 m_worldPosition;
    [SyncVar(hook = nameof(SomethingChange))]
    public Quaternion m_worldRotation;
    [SyncVar(hook = nameof(SomethingChange))]
    public Vector3 m_worldScale;


    public string m_lastGivenNameToLoad;
    public void SetModelFromName(string name)
    {
        m_lastGivenNameToLoad = name;
        for (int i = 0; i < m_prefabToSpawn.Count; i++)
        {
            if (m_prefabToSpawn[i].name.Length==(name).Length && m_prefabToSpawn[i].name.IndexOf(name)==0)
            {
                m_prefabIdToSpawn = -1;
                m_prefabIdToSpawn = i;
                return;
            }
        }
    }

    void ChangeModelOfPrefab(int _, int current)
    {
        DestroyAllChildren();
        if (current >= 0 && current < m_prefabToSpawn.Count) {
            GameObject o = GameObject.Instantiate(m_prefabToSpawn[current]);
            o.transform.parent = m_whereToCreate;
            o.transform.localPosition = Vector3.zero;
            o.transform.localRotation = Quaternion.identity;
            o.transform.localScale = Vector3.one;
        }
    }
    public void DestroyAllChildren()
    {
        foreach (Transform child in m_whereToCreate)
        {
            Destroy(child.gameObject);
        }
    }

    public void Awake()
    {
        if (isServer) { 
            NetworkServer.Spawn(this.gameObject);
            UpdateOnClientsCurrentTransform();
            RefreshModelOfPrefab();
        }
    }

    public void OnDestroy()
    {
        if (isServer)
        {
            NetworkServer.UnSpawn(this.gameObject);
        }
    }

    [ServerCallback]
    [ContextMenu("Random")]
    public void RandomTest() {

        transform.position += new Vector3(Random.value, Random.value, Random.value);
        transform.rotation *= Quaternion.Euler(Random.value*360, Random.value * 360, Random.value * 360);
        transform.localScale =new Vector3(Random.value + 0.5f, Random.value + 0.5f, Random.value + 0.5f);
        m_prefabIdToSpawn = Random.Range(0, m_prefabToSpawn.Count);
        UpdateOnClientsCurrentTransform();


    }

    [ServerCallback]
    [ContextMenu("Update Transform from server")]
    public void UpdateOnClientsCurrentTransform() {

        m_worldPosition= transform.position;
        m_worldRotation = transform.rotation;
        m_worldScale= transform.localScale;
    }   

    [ClientRpc]
    public void RefreshModelOfPrefab() {
        ChangeModelOfPrefab(-1, m_prefabIdToSpawn);
    }



    public void SomethingChange(Vector3 previous, Vector3 current) { HookTransformChangedObserved(); }
    public void SomethingChange(Quaternion previous, Quaternion current) { HookTransformChangedObserved(); }

    public void HookTransformChangedObserved() {

        //if (!isServer) { 
            transform.position = m_worldPosition;
            transform.rotation = m_worldRotation;
            transform.localScale = m_worldScale;
        //}
    }

    private void Reset()
    {
        m_whereToCreate = transform;
    }


    public  void SetWorldScale( Transform transform, Vector3 worldScale)
    {
        Transform parent = transform.parent;
        if (parent == null)
        {
            // If there's no parent, setting local scale is equivalent to setting world scale
            transform.localScale = worldScale;
        }
        else
        {
            // Calculate the required local scale
            Vector3 parentScale = parent.lossyScale;
            transform.localScale = new Vector3(
                worldScale.x / parentScale.x,
                worldScale.y / parentScale.y,
                worldScale.z / parentScale.z
            );
        }
    }

}
