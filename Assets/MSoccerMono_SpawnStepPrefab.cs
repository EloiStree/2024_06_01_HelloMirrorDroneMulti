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


    void ChangeModelOfPrefab(int _, int current)
    {
        DestroyAllChildren();
        if (m_prefabIdToSpawn >= 0 && m_prefabIdToSpawn < m_prefabToSpawn.Count) {
            GameObject o = GameObject.Instantiate(m_prefabToSpawn[m_prefabIdToSpawn]);
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

        RpcUpdateTransform(m_whereToCreate.position, m_whereToCreate.rotation, m_whereToCreate.localScale);

    }

    [ClientRpc]
    public void RefreshModelOfPrefab() {
        ChangeModelOfPrefab(-1, m_prefabIdToSpawn);
    }

    [ClientRpc]
    public void RpcUpdateTransform(Vector3 worldPosition, Quaternion worldRotation, Vector3 worldScale) {


        m_whereToCreate.position = worldPosition;
        m_whereToCreate.rotation = worldRotation;
        SetWorldScale(m_whereToCreate, worldScale);
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
