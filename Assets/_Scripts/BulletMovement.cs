using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour, IPooledObject {

    public float bulletSpeed;

    public Rigidbody2D bulletRB;
	// Use this for initialization
	public void OnObjectSpawn () {
        bulletRB.velocity = (transform.up * bulletSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
