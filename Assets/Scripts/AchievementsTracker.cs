using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AchievementsTracker
{
    public static List<Trait> traits;
    public static List<Tuple<Trait.SlotType, Phenotype>> tupleList = new List<Tuple<Trait.SlotType, Phenotype>>();
    public static void AddAchievement(Trait.SlotType slot, Genotype newGenotype, Trait phenotype)
    {
        var potentialTuple = Tuple.Create(slot, newGenotype.GetPhenotype());
        if(!tupleList.Contains(potentialTuple))
        {
            tupleList.Add(potentialTuple);
        }
    }
}
