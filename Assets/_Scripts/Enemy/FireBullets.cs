using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour {

    public Transform frontTurretEnd;
    public Transform leftTurretEnd;
    public Transform rightTurretEnd;
    public float fireRate;

    private float nextShot;
    private bool canFire;

    ObjectPoolingSystem objectPoolingSystem;

    private void Awake()
    {
        canFire = true;
    }

    // Use this for initialization
    private void Start () {
        objectPoolingSystem = ObjectPoolingSystem.SharedInstance;
    }
	
	// Update is called once per frame
	private void Update ()
    {
        BulletFire();
    }

    private void BulletFire()
    {
        if (Time.time > nextShot && canFire)
        {
            nextShot = Time.time + fireRate;
            objectPoolingSystem.GetFromPool("DamagingBullet", frontTurretEnd);
            objectPoolingSystem.GetFromPool("DamagingBullet", leftTurretEnd);
            objectPoolingSystem.GetFromPool("DamagingBullet", rightTurretEnd);
        }
    }

    public void CeaseFire(bool gameOver)
    {
        if(gameOver == true)
        {
            canFire = false;
        }
    }
}
