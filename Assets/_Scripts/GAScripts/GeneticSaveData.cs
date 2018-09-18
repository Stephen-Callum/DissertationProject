using System.Collections.Generic;
using System;
using UnityEditor;
using System.Xml.Serialization;

[Serializable]
public class GeneticSaveData
{
    public List<Genes> Population { get; set; }
    public int NumOfGames { get; set; }
    public bool CanPopulate { get; set; }
    //public float Fitness;
    public int Generation { get; set; }
    public float BestFitness { get; set; }
    public int BestGenesIndex { get; set; }
}

