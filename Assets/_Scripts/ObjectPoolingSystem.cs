using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingSystem : MonoBehaviour {

    [System.Serializable]
    public class Pool
    {
        public string bulletType;
        public int size;
        public GameObject projectilePrefab;
    }

    #region Singleton

    public static ObjectPoolingSystem SharedInstance;

    private void Awake()
    {
        SharedInstance = this;
    }
    #endregion

    //public Transform turretEnd;
    public List<Pool> pools;

    // A dictionary that stores separate queues of pooled game objects
    public Dictionary<string, Queue<GameObject>> poolDictionary;

	// Use this for initialization
	void Start () {
        // Allocate poolDictionary to memory
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // For every pool in List<pools> create a queue of inactive game objects and enqueue them, then add them to the dictionary.
        foreach (Pool pool in pools)
        {
            Queue<GameObject> projectilePool = new Queue<GameObject>();

            for (int i = 0; i < pool.size; i++)
            {
                GameObject obj = Instantiate(pool.projectilePrefab);
                obj.SetActive(false);
                projectilePool.Enqueue(obj);
            }

            poolDictionary.Add(pool.bulletType, projectilePool);
        }
	}
    
    public GameObject GetFromPool (string tag, Transform turretEnd)
    {
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with key" + tag + "does not exist.");
            return null;
        }
        GameObject objectFromPool = poolDictionary[tag].Dequeue();
        objectFromPool.SetActive(true);
        objectFromPool.transform.position = turretEnd.transform.position;
        objectFromPool.transform.rotation = turretEnd.transform.rotation;

        poolDictionary[tag].Enqueue(objectFromPool);

        return objectFromPool;
    }
	
	// Update is called once per frame
	void Update () {
		
	}
}
