using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Collections;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Fitnesses;
using GeneticSharp.Domain.Randomizations;

using UnityEngine;

public class APFitness : IFitness {

    private PlayerHealth playerHealth;
    private EnemyHealth enemyHealth;

    public APFitness(int attackPatterns)
    {
        Apatterns = new List<BattleResult>(attackPatterns);

        for (int i = 0; i < attackPatterns; i++)
        {
            var aPattern = new BattleResult { PlayerHealthRemaining = GetPlayerHealth(), EnemyHealthRemaining = GetEnemyHealth() };
            Apatterns.Add(aPattern);
        }
    }

    public IList<BattleResult> Apatterns { get; private set; }

    public double Evaluate(IChromosome chromosome)
    {
        var genes = chromosome.GetGenes();

        foreach(var g in genes)
        {
            var currentGeneIndex = Convert.ToInt32(g.Value, CultureInfo.InvariantCulture);
        }

        var fitness = 1.0 - GetPlayerHealth()
    }

    private int GetPlayerHealth()
    {
        return playerHealth.currentHealth;
    }

    private int GetEnemyHealth()
    {
        return enemyHealth.currentHealth;
    }

    private static double CalcBattleIntensity(BattleResult pHealth, BattleResult eHealth)
    {
        double fitnessValue = pHealth + eHealth;

        return fitnessValue;
    }
}
