using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour {
    public float bulletSpeed;

    public Rigidbody2D bulletRB;
	
    // Use this for initialization
	void Start () {
        bulletRB = GetComponent<Rigidbody2D>();
        
	}
	
	// Update is called once per frame
	void Update () {
		bulletRB.velocity = -transform.up * bulletSpeed * Time.deltaTime;
	}
}
