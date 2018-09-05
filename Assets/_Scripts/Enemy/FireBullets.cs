using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour {
    
    //public float fireRate;
    //public float EMPFireRate;

    private int randomIndex;
    private float nextBulletShot;
    private float nextEMPShot;
    private bool canFire;
    private Transform frontTurretEnd;
    private Transform leftTurretEnd;
    private Transform rightTurretEnd;
    private List<Transform> possiblePositions;
    private System.Random randomTransform;
    private ObjectPoolingSystem objectPoolingSystem;
    private AIController<GameObject> aIController;

    public void CeaseFire(bool gameOver)
    {
        if(gameOver == true)
        {
            canFire = false;
        }
    }

    private void Awake()
    {
        canFire = true;
        frontTurretEnd = GameObject.FindGameObjectWithTag("FrontTurret").transform;
        leftTurretEnd = GameObject.FindGameObjectWithTag("LeftTurret").transform;
        rightTurretEnd = GameObject.FindGameObjectWithTag("RightTurret").transform;
        aIController = GetComponent<AIController<Genes>>();
    }

    // Use this for initialization
    private void Start ()
    {
        objectPoolingSystem = ObjectPoolingSystem.SharedInstance;
        randomTransform = new System.Random();
        possiblePositions = new List<Transform>
        {
            frontTurretEnd,
            leftTurretEnd,
            rightTurretEnd
        };
    }

    // Update is called once per frame
    private void Update ()
    {
        BulletFire();
        EMPFire();
    }

    private void BulletFire()
    {
        if (Time.time > nextBulletShot && canFire)
        {
            nextBulletShot = Time.time + aIController.GetAI(aIController.numOfGames).BulletFireRate;
            objectPoolingSystem.GetFromPool("DamagingBullet", frontTurretEnd);
            objectPoolingSystem.GetFromPool("DamagingBullet", leftTurretEnd);
            objectPoolingSystem.GetFromPool("DamagingBullet", rightTurretEnd);
            Debug.Log("Bullet firerate = " + aIController.GetAI(aIController.numOfGames).BulletFireRate);
        }
    }

    private void EMPFire()
    {
        randomIndex = randomTransform.Next(possiblePositions.Count);
        if (Time.time > nextEMPShot && canFire)
        {
            nextEMPShot = Time.time + aIController.GetAI(aIController.numOfGames).EMPFireRate;
            objectPoolingSystem.GetFromPool("EMPCharge", possiblePositions[randomIndex]);
            Debug.Log("EMP firerate = " + aIController.GetAI(aIController.numOfGames).EMPFireRate.ToString());
        }
    }
}
