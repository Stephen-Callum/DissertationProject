using System.Collections.Generic;
using System;
using UnityEditor;

public class GeneticSaveData
{
    public List<Genes> PopulationGenes { get; set; }
    public int NumberOfGamesPlayed { get; set; }
    public bool CanPopulate { get; set; }
    //public float Fitness;
    public int Generation { get; set; }
}
