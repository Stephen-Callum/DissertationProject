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
    [SerializeField] private float mutationRate = 0.01f;
    [SerializeField] public GeneticSaveData Save = new GeneticSaveData();
    private int numOfEnemyVariables;
    private float playTime;
    private float scaledPlayTime;
    private float healthScore;
    private bool canPopulate;
    private GameObject enemy;
    private GameObject player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private System.Random random;
    private bool canIncrementGames;
    private Genes parent1;
    private Genes parent2;
    

    // Save a Population of genes
    public void SaveGeneration(string filePath)
    {
        FileReadAndWrite.WriteToXMLFile(filePath, Save);
        //WriteDataToXML.WriteToXMLFile(dataFilePath, DataSave);
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
        Debug.Log("--------------------Number of Games------------------------ = " + Save.NumOfGames);
        Debug.Log("Bullet firerate = " + CurrentGenes.BulletFireRate);
        Debug.Log("EMP firerate = " + CurrentGenes.EMPFireRate);
    }

    //public void HealthFitnessFunction(int generationNumber)
    //{
    //    float score = 0.0f;
    //    Debug.Log("Health fitness says hi");
    //    score += enemyHealth.HealthRemainingScore() + playerHealth.HealthRemainingScore();

    //    Save.Population[generationNumber].Fitness = score;
    //    if (generationNumber == 0)
    //    {
    //        Save.BestFitness = score;
    //        Save.BestGenesIndex = generationNumber;
    //    }
    //    else
    //    {
    //        if (score < Save.BestFitness)
    //        {
    //            Save.BestFitness = score;
    //            Save.BestGenesIndex = generationNumber;
    //        }
    //    }
    //    Debug.Log("Health Fitness = " + score);

    //    if (canIncrementGames)
    //    {
    //        Save.NumOfGames++;
    //        canIncrementGames = false;
    //    }
    //}

    public void TimeFitnessFuntion(int generationNumber)
    {
        float score = 0.0f;
        playTime = Time.timeSinceLevelLoad;
        scaledPlayTime = Mathf.Pow((playTime - 30.0f) / 30.0f, 2);
        score += scaledPlayTime;

        Save.Population[generationNumber].PlayTime = playTime;
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
        if (canIncrementGames)
        {
            Save.NumOfGames++;
            canIncrementGames = false;
        }

        Debug.Log("Playtime = " + playTime);
        Debug.Log("Time Fitness = " + score);
    }

    // needs to show playtime also
    //public void HealthAndTimeFitnessFunction(int generationNumber)
    //{
    //    float score = 0.0f;
    //    float playTime = Time.timeSinceLevelLoad;
    //    healthScore = enemyHealth.HealthRemainingScore() + playerHealth.HealthRemainingScore();
    //    scaledPlayTime = Mathf.Pow((playTime - 30.0f) / 30.0f, 2);
    //    score += healthScore + scaledPlayTime;

    //    Save.Population[generationNumber].PlayTime = playTime;
    //    Save.Population[generationNumber].Fitness = score;
    //    if (generationNumber == 0)
    //    {
    //        Save.BestFitness = score;
    //        Save.BestGenesIndex = generationNumber;
    //    }
    //    else
    //    {
    //        if (score < Save.BestFitness)
    //        {
    //            Save.BestFitness = score;
    //            Save.BestGenesIndex = generationNumber;
    //        }
    //    }
    //    if (canIncrementGames)
    //    {
    //        Save.NumOfGames++;
    //        canIncrementGames = false;
    //    }
    //    // for debugging purposes

    //    Debug.Log("Playtime = " + playTime);
    //    Debug.Log("Time Fitness = " + scaledPlayTime);
    //    Debug.Log("Health Fitness = " + healthScore);
    //    Debug.Log("Sum Fitness = " + score);
    //}

    // will it run more than once?
    private void Awake()
    {
        canPopulate = true;
        //player = GameObject.FindGameObjectWithTag("Player");
        //playerHealth = player.GetComponent<PlayerHealth>();
        //enemy = GameObject.FindGameObjectWithTag("Enemy");
        //enemyHealth = enemy.GetComponent<EnemyHealth>();
        numOfEnemyVariables = 2;
        random = new System.Random();
        Save.Population = new List<Genes>(populationSize);
        FullPath = Application.persistentDataPath + "/" + "GeneticSaveHealth.xml";
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
    }

    // Crossover and mutate the best genes to produce child gene and add them to the population
    private void BreedNewGenes()
    {
        if (Save.NumOfGames < Save.Population.Count)
        {
            // Create two parents and assign the elite genes to them. Should look into breeding some of the lower performing genes.
            parent1 = Save.Population[Save.BestGenesIndex]; // Best gene
            Debug.Log("parent 1 fitness=" + parent1.Fitness);
            parent2 = Save.Population[UnityEngine.Random.Range(0, Save.Population.Count)]; // random gene from population of 100

            Genes child = parent1.Crossover(parent2, numOfEnemyVariables, random);

            child.Mutate(mutationRate, random, numOfEnemyVariables);
            child.Generation = Save.NumOfGames;
            Debug.Log("child gene: " + child.BulletFireRate);

            Save.Population[Save.NumOfGames] = child;
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
                Save.Population.Add(new Genes());
                Save.Population[i].RandomiseGenes();
                Save.Population[i].Generation = Generation;
                Generation++;
            }
            canPopulate = false;
        }
    }
}
