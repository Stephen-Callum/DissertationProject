using System.Collections.Generic;
using UnityEngine;

/// <summary>
/// Simple pooling for Unity.
///   Author: Martin "quill18" Glaude (quill18@quill18.com)
///   Latest Version: https://gist.github.com/quill18/5a7cfffae68892621267
///   License: CC0 (http://creativecommons.org/publicdomain/zero/1.0/)
/// </summary>

public static class SimplePool
{

    const int DEFAULT_POOL_SIZE = 10;

    // A class that represents a pool for a particular prefab.
    class Pool
    {
        // An ID that is appended to anything that is instantiated.
        int nextId = 1;

        // Structure containing inactive game objects.
        Stack<GameObject> inactive;

        // The prefab that will be pooled.
        GameObject prefab;

        // Constructor
        public Pool(GameObject prefab, int initialQty)
        {
            this.prefab = prefab;

            // Instantiate Stack (inactive) with an initial quantity.
            inactive = new Stack<GameObject>(initialQty);
        }

        // Return an object from pool.
        public GameObject Spawn(Transform tform)
        {
            GameObject obj;
            
            // If inactive Stack is empty instantiate a new object, else pop from the stack.
            if (inactive.Count == 0)
            {
                // Zero objects in pool so we instantiate a new object.
                obj = GameObject.Instantiate(prefab, tform);
                obj.name = prefab.name + " (" + (nextId++) + ")";
                
                // Add component to object to track which pool the object belongs to.
                obj.AddComponent<PoolMember>().myPool = this;
            }
            else
            {
                // Get last object inserted to inactive stack.
                obj = inactive.Pop();

                // If picked inactive object no longer exists then try the next object in stack.
                if (obj == null)
                {
                    return Spawn(tform);
                }
            }

            // Set object to active position and rotation of object
            obj.transform.position = tform.position;
            obj.transform.rotation = tform.rotation;
            obj.SetActive(true);
            return obj;

        }

        // Return an object to the inactive pool.
        public void Despawn(GameObject obj)
        {
            obj.SetActive(false);
            inactive.Push(obj);
        }

    }

    // Linked with instantiated objects to add them back to the correct pools when calling Despawn.
    class PoolMember : MonoBehaviour
    {
        public Pool myPool;
    }

    // A dictionary that stores separate queues of pooled game objects.
    static Dictionary<GameObject, Pool> pools;
    
    // Initialise dictionary.
    static void Init(GameObject prefab = null, int qty = DEFAULT_POOL_SIZE)
    {
        if (pools == null)
        {
            pools = new Dictionary<GameObject, Pool>();
        }
        if (prefab != null && pools.ContainsKey(prefab) == false)
        {
            pools[prefab] = new Pool(prefab, qty);
        }
    }
    
    // Preload/Spawn specified number of copies of an object.
    public static void Preload(GameObject prefab, int qty = 1)
    {
        Init(prefab, qty);

        // Make a GameObject array to to create objects for preloading.
        GameObject[] obs = new GameObject[qty];
        for (int i = 0; i < qty; i++)
        {
            obs[i] = Spawn(prefab);
        }
        
        // Despawn objects.
        for (int i = 0; i < qty; i++)
        {
            Despawn(obs[i]);
        }
    }
    
    // Spawn a copy of specified prefab.
    public static  GameObject Spawn(GameObject prefab, Transform transform)
    {
        Init(prefab);

        return pools[prefab].Spawn(transform);
    }
    public static GameObject Spawn(GameObject prefab)
    {
        return Spawn(prefab);
    }

    public static void Despawn(GameObject obj)
    {
        PoolMember pm = obj.GetComponent<PoolMember>();
        if (pm == null)
        {
            Debug.Log("Object '" + obj.name + "' wasn't spawned from a pool. Destroying it instead.");
            GameObject.Destroy(obj);
        }
        else
        {
            pm.myPool.Despawn(obj);
        }
    }
}
