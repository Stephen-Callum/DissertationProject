using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyProperties : MonoBehaviour {

    public EnemyProperties()
    {
        
    }

    public float RotationSpeed
    {
        get
        {
            return rotationSpeed;
        }

        set
        {
            if ((value > 2) && (value < 6))
            {
                rotationSpeed = value;
            }
        }

    }
    public float BulletFireRate
    {
        get
        {
            return bulletFireRate;
        }

        set
        {
            if ((value > 0) && (value <= 3))
            {
                bulletFireRate = value;
            }
        }
    }
    public float EMPFireRate
    {
        get
        {
            return empFireRate;
        }

        set
        {
            if ((value > 0) && (value <= 10))
            {
                empFireRate = value;
            }
        }
    }

    public static float rotationSpeed;
    public static float bulletFireRate;
    public static float empFireRate;
}
