using System;
using System.Collections;
using System.Collections.Generic;

// Class that represents the individuals of a population
public class DNA<T>
{
    // Represents an array of characteristics for each individual
    public T[] Genes { get; private set; }

    // Represents the fitness of each individual
    public float Fitness { get; private set; }

    private Random random;
    private Func<T> getRandomGene;
    private Func<int, float> fitnessFunction;

    // Initialise each DNA object with gene pool size, random object, gene randomising function, fitness function and a boolean that checks if a DNA object can initialise Gene array
    public DNA(int size, Random random, Func<T> getRandomGene, Func<int, float> fitnessFunction, bool canInitGenes = true)
    {
        Genes = new T[size];
        this.random = random;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;

        if (canInitGenes)
        {
            for (int i = 0; i < Genes.Length; i++)
            {
                Genes[i] = getRandomGene();
            }
        }
    }

    // Returns a score for each individual to decide how likely said individual is to reproduce
    public float CalculateFitness(int index)
    {
        Fitness = fitnessFunction(index);
        return Fitness;
    }

    // Returns a new child after randomly picking genes from two parent DNA objects
    public DNA<T> CrossOver(DNA<T> otherParent)
    {
        DNA<T> child = new DNA<T>(Genes.Length, random, getRandomGene, fitnessFunction, canInitGenes : false);

        for (int i = 0; i < Genes.Length; i++)
        {
            child.Genes[i] = random.NextDouble() < 0.5 ? Genes[i] : otherParent.Genes[i];
        }

        return child;
    }

    // Randomise Genes of an individual depending on the mutationRate
    public void Mutate(float mutatitonRate)
    {
        for (int i = 0; i < Genes.Length; i++)
        {
            if (random.NextDouble() < mutatitonRate)
            {
                Genes[i] = getRandomGene();
            }
        }
    }
}
