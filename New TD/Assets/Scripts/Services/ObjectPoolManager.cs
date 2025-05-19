/*
 * Looks like I don't need it beacuse I have ObjectPool class. May be deleted after tests
 * 
 * using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolManager : MonoBehaviour
{
    public static ObjectPoolManager Instance; 

    [System.Serializable]
    public class Pool
    {
        public string prefabName; 
        public GameObject prefab;
        public int size;
    }

    public List<Pool> pools;
    private Dictionary<string, Queue<GameObject>> poolDictionary;
    private Dictionary<string, GameObject> prefabDictionary;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        InitializePool();
    }

    private void InitializePool()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();
        prefabDictionary = new Dictionary<string, GameObject>();

        foreach (Pool pool in pools)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.prefabName, objectPool);
            prefabDictionary.Add(pool.prefabName, pool.prefab);
        }
    }

    public GameObject GetObject(string tag)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            return null;
        }

        if (poolDictionary[tag].Count == 0)
        {
            GameObject newObj = Instantiate(prefabDictionary[tag]);
            return newObj;
        }

        GameObject obj = poolDictionary[tag].Dequeue();
        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj, string tag)
    {
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
    }
}*/
