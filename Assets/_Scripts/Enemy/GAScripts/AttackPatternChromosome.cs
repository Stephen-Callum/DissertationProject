using System.Collections;
using System.Collections.Generic;
using GeneticSharp.Domain.Chromosomes;
using GeneticSharp.Domain.Randomizations;
using UnityEngine;

public class AttackPatternChromosome : ChromosomeBase {

    private readonly int _attackPatterns;

    public AttackPatternChromosome(int attackPatterns) : base(attackPatterns)
    {
        _attackPatterns = attackPatterns;
        var attackPatternIndexes = RandomizationProvider.Current.GetUniqueInts(attackPatterns, 0, attackPatterns);

       for(int i = 0; i < attackPatterns; i++)
        {
            ReplaceGene(i, new Gene(attackPatternIndexes[i]));
        }

    }

    //Used by the fitness function to evaluate the AttackPatternChromosome's fitness based on the enemy's strength compared to the player
    public double Strength { get; internal set; }

    public override Gene GenerateGene(int geneIndex)
    {
        return new Gene(RandomizationProvider.Current.GetInt(0, _attackPatterns));
    }

    public override IChromosome CreateNew()
    {
        return new AttackPatternChromosome(_attackPatterns);
    }

    public override IChromosome Clone()
    {
        var clone = base.Clone() as AttackPatternChromosome;
        clone.Strength = Strength;

        return clone;
    }
}
