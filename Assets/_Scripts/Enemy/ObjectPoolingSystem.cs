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
        // Allocate memory for poolDictionary
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        // For every pool in List<pools> create a queue of inactive game objects and enqueue them.
        foreach (Pool pool in pools)
        {
            Queue<GameObject> projectilePool = new Queue<GameObject>();

            for (int i = 0; i < pool.poolSize; i++)
            {
                GameObject obj = Instantiate(pool.projectilePrefab);
                obj.SetActive(false);
                projectilePool.Enqueue(obj);
            }
            // Add object to queue in dictionary
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
        // check if dictionary contains key of object to be retrieved
        if (!poolDictionary.ContainsKey(tag))
        {
            Debug.LogWarning("Pool with key" + tag + "does not exist.");
            return null;
        }
        // Dequeue object to be taken from pool and set to active
        GameObject objectFromPool = poolDictionary[tag].Dequeue();
        objectFromPool.SetActive(true);
        // Set position and rotation to the rotation and position of the turrets
        objectFromPool.transform.position = turretEnd.transform.position;
        objectFromPool.transform.rotation = turretEnd.transform.rotation;
        IPooledObject pooledObj = objectFromPool.GetComponent<IPooledObject>();
        // Apply bullet movement physics to projectile on spawn
        if (pooledObj != null)
        {
            pooledObj.OnObjectSpawn();
        }
        // Enqueue the projectile
        poolDictionary[tag].Enqueue(objectFromPool);
        // Return the project from pool
        return objectFromPool;
    }
    
    // Set object as inactive and return to queue
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
