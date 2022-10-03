using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AchievementsTracker
{
    public static List<Trait> traits;
    public static HashSet<Tuple<Trait.SlotType, Phenotype>> tupleList = new HashSet<Tuple<Trait.SlotType, Phenotype>>();
    public static void AddAchievement(Trait.SlotType slot, Genotype newGenotype, Trait phenotype)
    {
        if (newGenotype.GetPhenotype() == Phenotype.Orange) return;
        var potentialTuple = Tuple.Create(slot, newGenotype.GetPhenotype());
        if (!tupleList.Contains(potentialTuple))
        {
            tupleList.Add(potentialTuple);
        }
    }
}
