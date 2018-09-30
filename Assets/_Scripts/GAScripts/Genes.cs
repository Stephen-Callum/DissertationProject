using UnityEngine;
using Unity;
using System;
using System.Collections.Generic;
using System.Xml.Serialization;

[Serializable]
public class Genes
{
    // Denotes the generation number of a gene
    public int Generation { get; set; }

    // Property that stores and sets bullet fire rate
    public float BulletFireRate
    {
        get
        {
            return bulletFireRate;
        }

        set
        {
            if ((value > BulletMinFR) && (value <= BulletMaxFR))
            {
                bulletFireRate = value;
            }
        }
    }

    // Property that stores and sets EMP fire rate
    public float EMPFireRate
    {
        get
        {
            return empFireRate;
        }

        set
        {
            if ((value > EmpMinFR) && (value <= EmpMaxFR))
            {
                empFireRate = value;
            }
        }
    }

    public float Fitness { get; set; }
    public float PlayTime { get; set; }
    public static float BulletMinFR = 0.2f;
    public static float BulletMaxFR = 2.0f;
    public static float EmpMinFR = 4.0f;
    public static float EmpMaxFR = 10.0f;
    private float bulletFireRate;
    private float empFireRate;

    // Each gene object is initialised with a fitness of 10 that is later overridden.
    public Genes()
    {
        Fitness = 10;
    }

    // Randomise Genes on initialisation
    public void RandomiseGenes()
    {
        // For each element, randomise depending on that element's min and max
        bulletFireRate = UnityEngine.Random.Range(BulletMinFR, BulletMaxFR);
        empFireRate = UnityEngine.Random.Range(EmpMinFR, EmpMaxFR);
        
    }

    // Randomise specific genes in gene array. Passed into Mutation method
    public void RandomiseGenes(int fireRateIterator)
    {
        if (fireRateIterator == 0)
        {
            bulletFireRate = UnityEngine.Random.Range(BulletMinFR, BulletMaxFR);
            return;
        }
        else if (fireRateIterator == 1)
        {
            empFireRate = UnityEngine.Random.Range(EmpMinFR, EmpMaxFR);
            return;
        }
        return;
        
        // For each element, randomise depending on that element's min and max.
    }

    // Run through gene array random chance to mutate genes of a population.
    public void Mutate(float mutationRate, System.Random random, int numOfProperties)
    {
        for (int i = 0; i < numOfProperties; i++)
        {
            if (random.NextDouble() < mutationRate)
            {
                RandomiseGenes(i);
            }
        }
    }

    // Select genes from parent gene arrays (50% to pick from two parents)
    public Genes Crossover(Genes otherParent, int numOfProperties, System.Random random)
    {
        Genes child = new Genes();

        child.BulletFireRate = random.NextDouble() < 0.5 ? BulletFireRate : otherParent.BulletFireRate;
        child.EMPFireRate = random.NextDouble() < 0.5 ? EMPFireRate : otherParent.EMPFireRate;

        return child;
    }
}
