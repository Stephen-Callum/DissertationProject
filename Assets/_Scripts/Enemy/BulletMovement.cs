using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletMovement : MonoBehaviour, IPooledObject
{
    public float bulletSpeed;

    public Rigidbody2D bulletRB;
	// Use this for initialisation of bullets
	public void OnObjectSpawn ()
    {
        bulletRB.velocity = (transform.up * bulletSpeed);
	}
}
