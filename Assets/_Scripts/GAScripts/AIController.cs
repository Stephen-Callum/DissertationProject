using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public List<Genes> Population { get; private set; }
    public List<Genes> SortedPopulation { get; private set; }
    public List<Genes> EliteGenes { get; private set; }
    public int Generation { get; private set; }
    public float? BestFitness { get; private set; }
    public float? NextBestFitness { get; private set; }
    public int NumOfGames { get; private set; }
    public Genes CurrentGenes;
    [NonSerialized] public string FullPath;
    [SerializeField] private int populationSize;
    //[SerializeField] private int elitism = 5;
    [SerializeField] private float mutationRate = 0.01f;
    //private IRandomiseGenes createRandGenes;
    private int numOfEnemyVariables;
    //private float? fitnessSum;
    private bool canPopulate;
    //private Genes genes;
    private GameObject enemy;
    private GameObject player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private System.Random random;
    private bool canIncrementGames;

    // Save a Population of genes
    public void SaveGeneration(string filePath)
    {
        GeneticSaveData save = new GeneticSaveData
        {
            Generation = Generation,
            PopulationGenes = Population,
            NumberOfGamesPlayed = NumOfGames,
            CanPopulate = canPopulate,
        };

        FileReadAndWrite.WriteToXMLFile(filePath, save);
    }

    // Load a population of genes
    public bool LoadGeneration(string filePath)
    {
        if (!System.IO.File.Exists(filePath))
        {
            return false;
        }

        GeneticSaveData save = FileReadAndWrite.ReadFromBinaryFile(filePath);
        Generation = save.Generation;
        Population = save.PopulationGenes;
        NumOfGames = save.NumberOfGamesPlayed;
        canPopulate = save.CanPopulate;
        
        return true;
    }

    // Pull a gene from the population depending on the number of games played
    public void GetAI(int numGamesPlayed)
    {
        CurrentGenes = Population[numGamesPlayed];
    }

    public void FitnessFunction(int generationNumber)
    {
        float score = 0.0f;
        print("generation number: " + generationNumber);
        Genes geneToCalculate = Population[generationNumber];

        score += enemyHealth.HealthRemainingScore() + playerHealth.HealthRemainingScore();
        
        //if (score == 0.0f)
        //{
        //    geneToCalculate.Fitness = 0.0f;
        //    return;
        //}
        
        geneToCalculate.Fitness = score;

        if (canIncrementGames)
        {
            NumOfGames++;
            canIncrementGames = false;
        }
    }

    // will it run more than once?
    private void Awake()
    {
        canPopulate = true;
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        numOfEnemyVariables = 2;
        //genes = new Genes();
        random = new System.Random();
        Population = new List<Genes>(populationSize);
        SortedPopulation = new List<Genes>(populationSize);
        EliteGenes = new List<Genes>(2);
        //createRandGenes = new Genes();
        Debug.Log("num of variables = " + numOfEnemyVariables);
        FullPath = Application.persistentDataPath + "/" + "Genetic Save";
        PopulateList();
    }

    private void Start()
    {
        canIncrementGames = true;
        LoadGeneration(FullPath);
        if (NumOfGames >= 5)
        {
            BreedNewGenes();
        }
        GetAI(NumOfGames);
        print("population count: " + Population.Count);
    }

    // Crossover and mutate the best genes to produce child gene and add them to the population
    private void BreedNewGenes()
    {
        if (Population.Count > 0)
        {
            CalculateBestFitness();
        }

        for (int i = NumOfGames; i < Population.Count; i++)
        {
            if (i < Population.Count)
            {
                // Create two parents and assign the elite genes to them. Should look into breeding some of the lower performing genes.
                Genes parent1, parent2 = new Genes(numOfEnemyVariables);
                parent1 = EliteGenes[0];
                parent2 = EliteGenes[1];

                Genes child = parent1.Crossover(parent2, numOfEnemyVariables, random);

                child.Mutate(mutationRate, random);

                Population[i] = child;
            }
        }
    }

    //private Genes ChooseParent()
    //{
    //    double? randomNumber = random.NextDouble() * fitnessSum;

    //    return null;
    //}

    // Calculates the sum of fitnesses in the population (used in ChooseParent). For each individual of the population, find the best performing individuals and copy them to BestGenes array
    private void CalculateBestFitness()
    {
        //fitnessSum = 0;
        // Store population in a list sorted by best fitness first
        SortedPopulation = Population;
        SortedPopulation.Sort(CompareGenes);
        Genes best = SortedPopulation[0];
        Genes nextBest = SortedPopulation[1];

        BestFitness = best.Fitness;
        NextBestFitness = nextBest.Fitness;
        EliteGenes.Add(best);
        EliteGenes.Add(nextBest);
    }

    // what if there are null values?
    private int CompareGenes(Genes a, Genes b)
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
    
    // Initisalises the Population list with new random genes
    private void PopulateList()
    {
        if (canPopulate)
        {
            Generation = 0;
            for (int i = 0; i < populationSize; i++)
            {
                Population.Add(new Genes(numOfEnemyVariables, random));
                Population[i].RandomiseGenes();
                Population[i].Generation = Generation;
                Generation++;
            }
            canPopulate = false;
        }
    }
}
