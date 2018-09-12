using System.Collections;
using System;
using System.Collections.Generic;
using Unity;
using UnityEngine;

public class AIController : MonoBehaviour {

    public int Generation { get; private set; }
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
    private Genes parent1;
    private Genes parent2;
    [SerializeField] public GeneticSaveData Save = new GeneticSaveData();

    // Save a Population of genes
    public void SaveGeneration(string filePath)
    {
        

        FileReadAndWrite.WriteToXMLFile(filePath, Save);
    }

    // Load a population of genes
    public bool LoadGeneration(string filePath)
    {
        if (!System.IO.File.Exists(filePath))
        {
            return false;
        }

        Save = FileReadAndWrite.ReadFromBinaryFile(filePath);
        
        return true;
    }

    // Pull a gene from the population depending on the number of games played
    public void GetAI(int numGamesPlayed)
    {
        CurrentGenes = Save.Population[numGamesPlayed];
    }

    public void FitnessFunction(int generationNumber)
    {
        float score = 0.0f;
        print("generation number: " + generationNumber);

        score += enemyHealth.HealthRemainingScore() + playerHealth.HealthRemainingScore();
        
        //if (score == 0.0f)
        //{
        //    geneToCalculate.Fitness = 0.0f;
        //    return;
        //}
        
        Save.Population[generationNumber].Fitness = score;
        if (generationNumber == 0)
        {
            Save.BestFitness = score;
            Save.BestGenesIndex = generationNumber;
        }
        else
        {
            if (score < Save.BestFitness)
            {
                Save.BestFitness = score;
                Save.BestGenesIndex = generationNumber;
            }
        }
        print("Fitness = " + score);

        if (canIncrementGames)
        {
            Save.NumOfGames++;
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
        Save.Population = new List<Genes>(populationSize);
        //createRandGenes = new Genes();
        Debug.Log("num of variables = " + numOfEnemyVariables);
        FullPath = Application.persistentDataPath + "/" + "Genetic Save";
        PopulateList();
    }

    private void Start()
    {
        Save.NumOfGames = 0;
        canIncrementGames = true;
        LoadGeneration(FullPath);
        if (Save.NumOfGames >= 5)
        {
            BreedNewGenes();
        }
        GetAI(Save.NumOfGames);
        print("population count: " + Save.Population.Count);
    }

    // Crossover and mutate the best genes to produce child gene and add them to the population
    private void BreedNewGenes()
    {
        
            if (Save.NumOfGames < Save.Population.Count)
            {
                // Create two parents and assign the elite genes to them. Should look into breeding some of the lower performing genes.
                parent1 = Save.Population[Save.BestGenesIndex]; // Best gene
                Debug.Log("parent 1 fitness=" + parent1.Fitness);
                parent2 = Save.Population[UnityEngine.Random.Range(0, Save.Population.Count)]; // random gene

                Genes child = parent1.Crossover(parent2, numOfEnemyVariables, random);

                child.Mutate(mutationRate, random);
                Debug.Log("child gene 0"+child.GeneArray[0]);

                Save.Population[Save.NumOfGames] = child;
            }
        
    }

    //private Genes ChooseParent()
    //{
    //    double? randomNumber = random.NextDouble() * fitnessSum;

    //    return null;
    //}

    // Calculates the sum of fitnesses in the population (used in ChooseParent). For each individual of the population, find the best performing individuals and copy them to BestGenes array

    // what if there are null values?
   
    
    // Initisalises the Population list with new random genes
    private void PopulateList()
    {
        if (canPopulate)
        {
            Generation = 0;
            for (int i = 0; i < populationSize; i++)
            {
                Save.Population.Add(new Genes(numOfEnemyVariables));
                Save.Population[i].RandomiseGenes();
                Save.Population[i].Generation = Generation;
                Generation++;
            }
            canPopulate = false;
        }
    }
}
