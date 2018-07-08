using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
    public float bulletSpeed;

    public Rigidbody2D bulletRB;
	
    // Use this for initialization
	void Start () {
        bulletRB = GetComponent<Rigidbody2D>();
        bulletRB.AddForce(transform.up * bulletSpeed);
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
