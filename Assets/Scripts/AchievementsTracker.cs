using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class AchievementsTracker
{
    public static List<Trait> traits;
    public static void AddAchievement(Trait.SlotType slot, Genotype newGenotype, Trait phenotype)
    {
        switch (slot)
        {
            case Trait.SlotType.Head:
                break;
            case Trait.SlotType.Arms:
                break;
            case Trait.SlotType.UpperBody:
                break;
            case Trait.SlotType.LowerBody:
                break;
            case Trait.SlotType.Legs:
                break;
        }
    }
}
