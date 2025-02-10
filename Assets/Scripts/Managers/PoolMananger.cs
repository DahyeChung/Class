using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;


///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
///////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////

[Serializable]
public class PoolItemData
{
    public GameObject prefab;
    public int initCount = 5;
    public List<GameObject> listItem;
}
public class PoolMananger : Singleton<PoolMananger>
{
    public List<PoolItemData> listPool = new List<PoolItemData>();

    void Awake()
    {
        AddObjectsToPool();
    }

    void AddObjectsToPool()
    {
        foreach (var pool in listPool)
        {
            for (int i = 0; i < pool.initCount; i++)
            {
                var ob = Instantiate(pool.prefab, transform);
                pool.listItem.Add(ob);
                ob.transform.localPosition = Vector3.zero;
                ob.SetActive(false);
            }
        }
    }

    public GameObject GetSpawn(string prefabName, Vector3 pos, Quaternion rot)
    {
        foreach (var pool in listPool)
        {
            if (pool.prefab.name.Equals(prefabName))
            {
                if (pool.listItem != null)
                {
                    foreach (var item in pool.listItem)
                    {
                        if (!item.activeSelf)
                        {
                            item.SetActive(true);

                            item.transform.position = pos;
                            item.transform.rotation = rot;
                            return item;
                        }
                    }
                    var ob = Instantiate(pool.prefab, pos, rot);
                    pool.listItem.Add(ob);
                    ob.SetActive(true);
                }
            }
        }
        return null;
    }

    public void TakeSpawn(GameObject prefab)
    {
        prefab.SetActive(false);
    }
}