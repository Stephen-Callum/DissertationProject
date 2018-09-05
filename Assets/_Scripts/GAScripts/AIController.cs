using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    public List<Genes> Population { get; private set; }
    public List<Genes> EliteGenes { get; private set; }
    public int Generation { get; private set; }
    public float? BestFitness { get; private set; }
    public float? NextBestFitness { get; private set; }
    public Genes CurrentGenes;
    [NonSerialized]
    public string fullPath;
    public int numOfGames;
    
    [SerializeField] private int populationSize;
    //[SerializeField] private int elitism = 5;
    [SerializeField] private float mutationRate = 0.01f;
    private IRandomiseGenes createRandGenes;
    private int numOfEnemyVariables;
    private float? fitnessSum;
    private bool canPopulate;
    private Genes genes;
    private GameObject enemy;
    private GameObject player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private System.Random random;
    

    // Save a Population of genes
    public void SaveGeneration(string filePath)
    {
        GeneticSaveData save = new GeneticSaveData
        {
            Generation = Generation,
            PopulationGenes = Population,
        };

        FileReadAndWrite.WriteToBinaryFile(filePath, save);
    }

    // Load a population of genes
    public bool LoadGeneration(string filePath)
    {
        if (!System.IO.File.Exists(filePath))
        {
            return false;
        }

        GeneticSaveData save = FileReadAndWrite.ReadFromBinaryFile<GeneticSaveData>(filePath);
        Generation = save.Generation;
        Population = save.PopulationGenes;
        
        return true;
    }

    // Pull a gene from the population depending on the number of games played
    public void GetAI(int numGamesPlayed)
    {
        CurrentGenes = Population[numGamesPlayed];
    }

    public float FitnessFunction(int generationNumber)
    {
        float score = 10.0f;
        Genes genes = Population[generationNumber];
        // must relate score to one population index, dont run through array
        for (int i = 0; i < genes.GeneArray.Length; i++)
        {
            score *= enemyHealth.HealthRemainingScore() + playerHealth.HealthRemainingScore();
        }
        if (score == 0.0f)
        {
            return 0.0f;
        }
        return 10.0f / score;
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
        genes = new Genes();
        random = new System.Random();
        Population = new List<Genes>(populationSize);
        EliteGenes = new List<Genes>(2);
        createRandGenes = new Genes();
        Debug.Log(numOfEnemyVariables);
        fullPath = Application.persistentDataPath + "/" + "Genetic Save";
        PopulateList();
    }

    private void Start()
    {
        LoadGeneration(fullPath);
        if (numOfGames >= 5)
        {
            BreedNewGenes();
        }
        GetAI(numOfGames);
    }

    private void Update()
    {
        
    }

    // Crossover and mutate the best genes to produce child gene and add them to the population
    private void BreedNewGenes()
    {
        if (Population.Count > 0)
        {
            CalculateBestFitness();
        }

        for (int i = numOfGames; i < Population.Count; i++)
        {
            if (i < Population.Count)
            {
                Genes parent1 = genes.EliteGenes[0];
                Genes parent2 = genes.EliteGenes[1];

                Genes child = parent1.Crossover(parent2);

                child.Mutate(mutationRate);

                Population[i] = child;
            }
        }
    }

    // Calculates the sum of fitnesses in the population (used in ChooseParent). For each individual of the population, find the best performing individuals and copy them to BestGenes array
    private void CalculateBestFitness()
    {
        //fitnessSum = 0;
        Population.Sort(CompareGenes);
        Genes best = Population[0];
        Genes nextBest = Population[1];

        BestFitness = best.Fitness;
        NextBestFitness = nextBest.Fitness;
        EliteGenes.Add(best);
        EliteGenes.Add(nextBest);
    }

    // what if there are null values?
    private int CompareGenes(Genes a, Genes b)
    {
        if (a.Fitness < b.Fitness)
        {
            return -1;
        }
        else if (a.Fitness > b.Fitness)
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
                Population.Add(new Genes(numOfEnemyVariables, createRandGenes, FitnessFunction, random));
                Population[i].Generation = Generation;
                Generation++;
            }
            canPopulate = false;
        }
    }
}
