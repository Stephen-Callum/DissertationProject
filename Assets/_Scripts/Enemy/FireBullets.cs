using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour {

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
    private GameObject GAManager;
    private AIController aIController;

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
        GAManager = GameObject.FindGameObjectWithTag("GAManager");
        aIController = GAManager.GetComponent<AIController>();
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
            rightTurretEnd,
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
            nextBulletShot = Time.time + aIController.CurrentGenes.BulletFireRate;
            objectPoolingSystem.GetFromPool("DamagingBullet", frontTurretEnd);
            objectPoolingSystem.GetFromPool("DamagingBullet", leftTurretEnd);
            objectPoolingSystem.GetFromPool("DamagingBullet", rightTurretEnd);
        }
    }

    private void EMPFire()
    {
        randomIndex = randomTransform.Next(possiblePositions.Count);
        if (Time.time > nextEMPShot && canFire)
        {
            nextEMPShot = Time.time + aIController.CurrentGenes.EMPFireRate;
            objectPoolingSystem.GetFromPool("EMPCharge", possiblePositions[randomIndex]);
        }
    }
}
