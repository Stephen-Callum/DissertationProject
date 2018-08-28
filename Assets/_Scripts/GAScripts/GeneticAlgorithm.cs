using System;
using System.Collections.Generic;

public class GeneticAlgorithm<T> {
    // List of individuals in population
	public List<DNA<T>> Population { get; private set; }

    // Refers to the Generation number
    public int Generation { get; private set; }

    // Fitness of best individual of previous generation
    public float BestFitness { get; private set; }

    // Genes of best individual of previous generation
    public T[] BestGenes { get; private set; }

    // Number of the best genes to select in a generation;
    public int Elitism;
    public float MutationRate;

    private List<DNA<T>> newPopulation;
    private Random random;
    private float fitnessSum;
    private int dnaSize;
    private Func<T> getRandomGene;
    private Func<int, float> fitnessFunction;

    // Initialise each GA object with the population size, random object, gene randomising function, fitness function, elitism variable and mutation rate
    public GeneticAlgorithm(int populationSize, int dnaSize, Random random, Func<T> getRandomGene, Func<int, float> fitnessFunction, int elitism, float mutationRate = 0.01f)
    {
        Generation = 1;
        MutationRate = mutationRate;
        Population = new List<DNA<T>>(populationSize);
        newPopulation = new List<DNA<T>>(populationSize);
        this.random = random;
        this.dnaSize = dnaSize;
        this.getRandomGene = getRandomGene;
        this.fitnessFunction = fitnessFunction;
        Elitism = elitism;

        BestGenes = new T[dnaSize];

        // Create elements of population
        for (int i = 0; i < populationSize; i++)
        {
            Population.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction, canInitGenes: true));
        }
    }

    // Create new generation with random genes
    public void NewGeneration(int numNewDNA = 0, bool crossoverNewDNA = false)
    {
        int finalCount = Population.Count + numNewDNA;
        if (finalCount <= 0)
        {
            return;
        }

        if(Population.Count > 0)
        {
            CalculateFitness();
            Population.Sort(CompareDNA);
            
        }
        newPopulation.Clear();

        for (int i = 0; i < finalCount; i++)
        {
            if (i < Elitism && i < Population.Count)
            {
                newPopulation.Add(Population[i]);
            }
            else if (i < Population.Count || crossoverNewDNA)
            {
                DNA<T> parent1 = ChooseParent();
                DNA<T> parent2 = ChooseParent();

                DNA<T> child = parent1.CrossOver(parent2);

                child.Mutate(MutationRate);

                newPopulation.Add(child);
            }
            else
            {
                newPopulation.Add(new DNA<T>(dnaSize, random, getRandomGene, fitnessFunction, canInitGenes: true));
            }
        }

        // To keep two lists of generations rather than creating a new list for each generation
        List<DNA<T>> tmpList = Population;
        Population = newPopulation;
        newPopulation = tmpList;

        // increment generation number
        Generation++;

    }

    // Compare fitness of first and second parameters. To pass into sorting method where most fit is at the beginning of the list
    public int CompareDNA(DNA<T> a, DNA<T> b)
    {
        if (a.Fitness > b.Fitness)
        {
            return -1;
        }
        else if (a.Fitness < b.Fitness)
        {
            return 1;
        }
        else
        {
            return 0;
        }
    }

    // Calculate the sum of fitnesses in the population. For each individual of the population, find the best performing individuals and copy them to BestGenes array
    public void CalculateFitness()
    {
        fitnessSum = 0;
        DNA<T> best = Population[0];

        for (int i = 0; i < Population.Count; i++)
        {
            fitnessSum += Population[i].CalculateFitness(i);

            if (Population[i].Fitness > best.Fitness)
            {
                best = Population[i];
            }
        }

        BestFitness = best.Fitness;
        best.Genes.CopyTo(BestGenes, 0);
    }

    // Choose parents based on fitness
    private DNA<T> ChooseParent()
    {
        double randomNumber = random.NextDouble() * fitnessSum;

        for (int i = 0; i < Population.Count; i++)
        {
            if(randomNumber < Population[i].Fitness)
            {
                return Population[i];
            }

            randomNumber -= Population[i].Fitness;
        }

        return null;
    }
}