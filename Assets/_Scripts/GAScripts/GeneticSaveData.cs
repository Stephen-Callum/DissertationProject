using System.Collections.Generic;
using System;

[Serializable]
public class GeneticSaveData<T>
{
    public List<T[]> PopulationGenes;
    //public float Fitness;
    public int Generation;
}
