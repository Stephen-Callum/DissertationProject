using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPoolingSystem : MonoBehaviour {

    [System.Serializable]
    public class Pool
    {
        public string bulletType;
        public int poolSize;
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
	void Start ()
    {
        InstantiatePool();
    }

    public void InstantiatePool()
    {
        // Allocate poolDictionary to memory
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // For every pool in List<pools> create a queue of inactive game objects and enqueue them, then add them to the dictionary.
        foreach (Pool pool in pools)
        {
            Queue<GameObject> projectilePool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.projectilePrefab);
                obj.SetActive(false);
                projectilePool.Enqueue(obj);
            }

            poolDictionary.Add(pool.bulletType, projectilePool);
        }
	}
    

    public void ReturnToPool (GameObject obj)
    {
        obj.SetActive(false);
        poolDictionary[tag].Enqueue(obj);
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

        IPooledObject pooledObj = objectFromPool.GetComponent<IPooledObject>();

        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }

        poolDictionary[tag].Enqueue(objectFromPool);

        return objectFromPool;
    }

    public void ReturnToPool(string tag, GameObject objToPool)
    {
        objToPool.SetActive(false);
        poolDictionary[tag].Enqueue(objToPool);
    }
    
    //// return bullet damage of specific bullet type.
    //public int GetBulletDamage(string tag)
    //{
        
    //}
	
	// Update is called once per frame
	void Update () {
		
	}
}
