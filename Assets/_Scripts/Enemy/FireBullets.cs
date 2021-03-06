﻿using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FireBullets : MonoBehaviour
{

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
    private AudioSource[] fireSounds;

    public void CeaseFire(bool gameOver)
    {
        if(gameOver == true)
        {
            canFire = false;
        }
    }

    // One of the first functions to be called
    private void Awake()
    {
        canFire = true;
        frontTurretEnd = GameObject.FindGameObjectWithTag("FrontTurret").transform;
        leftTurretEnd = GameObject.FindGameObjectWithTag("LeftTurret").transform;
        rightTurretEnd = GameObject.FindGameObjectWithTag("RightTurret").transform;
        GAManager = GameObject.FindGameObjectWithTag("GAManager");
        aIController = GAManager.GetComponent<AIController>();
        fireSounds = GetComponents<AudioSource>();
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
    // Fire bullets
    private void BulletFire()
    {
        if (Time.time > nextBulletShot && canFire)
        {
            fireSounds[0].Play();
            nextBulletShot = Time.time + aIController.CurrentGenes.BulletFireRate;
            objectPoolingSystem.GetFromPool("DamagingBullet", frontTurretEnd);
            objectPoolingSystem.GetFromPool("DamagingBullet", leftTurretEnd);
            objectPoolingSystem.GetFromPool("DamagingBullet", rightTurretEnd);
        }
    }
    // Fire EMP charges
    private void EMPFire()
    {
        randomIndex = randomTransform.Next(possiblePositions.Count);
        if (Time.time > nextEMPShot && canFire)
        {
            fireSounds[1].Play();
            nextEMPShot = Time.time + aIController.CurrentGenes.EMPFireRate;
            objectPoolingSystem.GetFromPool("EMPCharge", possiblePositions[randomIndex]);
        }
    }
}
