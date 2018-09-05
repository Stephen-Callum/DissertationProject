using UnityEngine;
using System;
using System.Collections.Generic;

public class Genes : IRandomiseGenes{

    // Represents an array of characteristics for each individual
    public float[] GeneArray { get; private set; }

    // A list of the best performing genes
    public List<Genes> EliteGenes { get; private set; }

    // Denotes the generation number of a gene
    public int Generation { get; set; }
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

    // Represents the fitness of each individual where 0 is best fitness.
    public float? Fitness { get; private set; }

    public static float BulletMinFR = 0.2f;
    public static float BulletMaxFR = 2.0f;
    public static float EmpMinFR = 4.0f;
    public static float EmpMaxFR = 10.0f;
    private float bulletFireRate;
    private float empFireRate;
    private IRandomiseGenes createRandGeneInterface;
    private System.Random random;
    private Func<int, float> fitnessFunction;
    [NonSerialized]
    public Genes genes;

    // Empty contructor
    public Genes()
    {
           
    }
    
    // Each gene object is initialised with a gene array that contains random genes (each pertaining to enemy attack behaviours.)
    public Genes(int numOfProperties, IRandomiseGenes createRandGeneInterface, Func<int, float> fitnessFunction, System.Random random)
    {
        this.fitnessFunction = fitnessFunction;
        this.createRandGeneInterface = createRandGeneInterface;
        this.random = random;

        // Set length of Gene to the number of properties
        GeneArray = new float[numOfProperties];

        // Set fitness to null rather than 0 in order to avoid corrupting the fitness calculation.
        Fitness = null;

        // for each element in the gene array, set it to a property of the enemy. 0 = Bullet Fire rate, 1 = EMP Fire Rate
        RandomiseGenes();
    }
    
    // Randomise Genes on initialisation
    public void RandomiseGenes()
    {
        GeneArray[0] = UnityEngine.Random.Range(BulletMinFR, BulletMaxFR);
        GeneArray[1] = UnityEngine.Random.Range(EmpMinFR, EmpMaxFR);
        // For each element, randomise depending on that element's min and max.
    }

    // Randomise specific genes in gene array. Passed into Mutation method
    public void RandomiseGenes(int geneArrayIndex)
    {
        if (geneArrayIndex == 0)
        {
            GeneArray[0] = UnityEngine.Random.Range(BulletMinFR, BulletMaxFR);
            return;
        }
        else if (geneArrayIndex == 1)
        {
            GeneArray[1] = UnityEngine.Random.Range(EmpMinFR, EmpMaxFR);
            return;
        }
        return;
        
        // For each element, randomise depending on that element's min and max.
    }

    // Run through gene array random chance to mutate genes of a population.
    public void Mutate(float mutationRate)
    {
        for (int i = 0; i < GeneArray.Length; i++)
        {
            if (random.NextDouble() < mutationRate)
            {
                RandomiseGenes(i);
            }

        }
    }

    // Select genes from parent gene arrays (50% to pick from two parents)
    public Genes Crossover(Genes otherParent)
    {
        Genes child = new Genes(GeneArray.Length, createRandGeneInterface, fitnessFunction, random);

        for (int i = 0; i < GeneArray.Length; i++)
        {
            child.GeneArray[i] = random.NextDouble() < 0.5 ? GeneArray[i] : otherParent.GeneArray[i];
        }

        return child;
    }
    
    // Sets the fitness of an element in the Population List
    private float? CalculateFitness(int index)
    {
        Fitness = fitnessFunction(index);
        return Fitness;
    }

    
}
