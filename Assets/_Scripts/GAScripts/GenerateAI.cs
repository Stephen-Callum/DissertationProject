using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateAI : MonoBehaviour {

    private float bulletFireRate;
    private float empFireRate;
    private float bulletMinFR;
    private float bulletMaxFR;
    private float empMinFR;
    private float empMaxFR;
    private int numOfFields;
    private EnemyProperties enemyProperties;

    private void Awake()
    {
        enemyProperties = GetComponent<EnemyProperties>();
        bulletMinFR = 0.1f;
        bulletMaxFR = 1.5f;
        empMinFR = 3.0f;
        empMaxFR = 10;
        numOfFields = enemyProperties.GetType().GetFields().Length;

    }
    private void Start()
    {
        Debug.Log("numbner of fields ios: " + numOfFields);
    }

    public void GetAI()
    {
        RandomiseAI(bulletMinFR, bulletMaxFR, empMinFR, empMaxFR);
        enemyProperties.BulletFireRate = bulletFireRate;
        enemyProperties.EMPFireRate = empFireRate;
    }

    private void RandomiseAI(float bulletMin, float bulletMax, float empMin, float empMax)
    {
        //randOne = Random.value;
        //randTwo = Random.value;
        bulletFireRate = Random.Range(bulletMin, bulletMax);
        empFireRate = Random.Range(empMin, empMax);
        print("bullet firerate = " + bulletFireRate);
        print("emp firerate = " + empFireRate);
    }
}
