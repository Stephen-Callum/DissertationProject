using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour {

    public float fireRate;

    private float nextShot;

    //public Rigidbody2D bulletPrefab;
    public Transform frontTurretEnd;
    public Transform leftTurretEnd;
    public Transform rightTurretEnd;

    ObjectPoolingSystem objectPoolingSystem;

	// Use this for initialization
	void Start () {
        objectPoolingSystem = ObjectPoolingSystem.SharedInstance;
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;
            objectPoolingSystem.GetFromPool("Bullet", frontTurretEnd);
            objectPoolingSystem.GetFromPool("Bullet", leftTurretEnd);
            objectPoolingSystem.GetFromPool("Bullet", rightTurretEnd);
        }
        
	}
}
