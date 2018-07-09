using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour {

    public float fireRate;
    public Rigidbody2D bulletPrefab;
    public Transform enemyTurret;
    

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
        Instantiate(bulletPrefab, enemyTurret.position, enemyTurret.rotation);
	}
}
