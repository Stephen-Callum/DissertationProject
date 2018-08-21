using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour {

    public float fireRate;
    public GameObject bulletPrefab;
    public GameObject EMPCharge;

    private Transform turretEnd;
    private float nextShot;

    //public Transform frontTurretEnd;
    //public Transform leftTurretEnd;
    //public Transform rightTurretEnd;

    private void Awake()
    {
        SimplePool.Preload(bulletPrefab, 10);
    }

    // Use this for initialization
    void Start () {
        turretEnd = GetComponent<GameObject>().transform;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;
            SimplePool.Spawn(bulletPrefab, turretEnd);
        }
        
	}
}
