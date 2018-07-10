using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour {

    public float fireRate;

    private float nextShot;

    public Rigidbody2D bulletPrefab;
    public Transform enemyTurretEnd;

	// Use this for initialization
	void Start () {
    }
	
	// Update is called once per frame
	void Update () {
        if (Time.time > nextShot)
        {
            nextShot = Time.time + fireRate;
            Instantiate(bulletPrefab, enemyTurretEnd.position, enemyTurretEnd.rotation);
        }
        
	}
}
