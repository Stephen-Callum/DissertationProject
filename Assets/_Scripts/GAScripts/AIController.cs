using System.Collections;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AIController : MonoBehaviour {

    [SerializeField] private int populationSize = 200;
    [SerializeField] private int elitism = 5;
    [SerializeField] private float mutationRate = 0.01f;
    [SerializeField] private float bulletMinFR;
    [SerializeField] private float bulletMaxFR;
    [SerializeField] private float empMinFR;
    [SerializeField] private float empMaxFR;
    private float bulletFireRate;
    private float empFireRate;
    private int numOfEnemyVariables;
    private EnemyProperties targetProperties;
    private GenerateAI generate;
    private GameObject enemy;
    private GameObject player;
    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;
    private GeneticAlgorithm<float> ga;
    private System.Random random;

    private void Awake()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerHealth = player.GetComponent<PlayerHealth>();
        enemy = GameObject.FindGameObjectWithTag("Enemy");
        enemyHealth = enemy.GetComponent<EnemyHealth>();
        targetProperties = enemy.GetComponent<EnemyProperties>();
        numOfEnemyVariables = targetProperties.GetType().GetFields().Length;
        //generate = enemy.GetComponent<GenerateAI>();
        Debug.Log(numOfEnemyVariables);
    }

    private void Start()
    {
        ga = new GeneticAlgorithm<float>(populationSize, numOfEnemyVariables, random, GetRandomProperties, FitnessFunction, elitism, mutationRate);
        //generate.GetAI();
    }

    private void Update()
    {
        ga.NewGeneration();
        if(ga.BestFitness == 150)
        {
            this.enabled = false;
        }
    }

    private float GetRandomProperties()
    {
        float fireRate = UnityEngine.Random.Range(bulletMinFR, bulletMaxFR);

        return fireRate;
    }

    private float FitnessFunction(int index)
    {
        float score = 10.0f;
        DNA<float> dna = ga.Population[index];

        for (int i = 0; i < dna.Genes.Length; i++)
        {
            //if(enemyHealth.currentHealth == 0)
            //{
            //    score *= 5;
            //}
            //if (enemyHealth.currentHealth == 1)
            //{
            //    score *= 4;
            //}
            //if (enemyHealth.currentHealth == 2)
            //{
            //    score *= 3;
            //}
            //if (enemyHealth.currentHealth == 3)
            //{
            //    score *= 2;
            //}
            //if (enemyHealth.currentHealth == 4)
            //{
            //    score *= 1;
            //}
            //if (enemyHealth.currentHealth == 5)
            //{
            //    score *= 0.1f;
            //}
            //if (playerHealth.currentHealth == 0)
            //{
            //    score *= 0.1f;
            //}
            //if (playerHealth.currentHealth == 1)
            //{
            //    score *= 3.0f;
            //}
            //if (playerHealth.currentHealth == 2)
            //{
            //    score *= 2.0f;
            //}
            //if (playerHealth.currentHealth == 3)
            //{
            //    score *= 1.0f;
            //}
            score *= enemyHealth.HealthRemainingScore() + playerHealth.HealthRemainingScore();
        }

        return score;
    }
}
