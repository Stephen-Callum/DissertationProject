using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingSystem : MonoBehaviour {

    public GameObject bulletPrefab;
    public Transform enemyTurretEnd;

    [SerializeField]
    private GameObject prefab;

    private Queue<GameObject> inPoolObjects = new Queue<GameObject>();
   
    public static PoolingSystem Instance { get; private set; }

    private void Awake()
    {
        Instance = this;
        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < 10; i++)
        {
            var addingInstance = Instantiate(bulletPrefab, enemyTurretEnd.position, enemyTurretEnd.rotation);
            AddToPool(addingInstance);
        }
    }

    public void AddToPool (GameObject instance)
    {
        instance.SetActive(false);
        inPoolObjects.Enqueue(instance);
    }

    public GameObject GetFromPool()
    {
        if (inPoolObjects.Count == 0)
        {
            GrowPool();
        }

        var instance = inPoolObjects.Dequeue();
        instance.SetActive(true);
        return instance;

    }

    // Use this for initialization
     void Start () {
        
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
