using System.Collections.Generic;
using UnityEngine;

public class TraitsBankController : MonoBehaviour
{
    public List<Trait> headTraits;
    public List<Trait> armTraits;
    public List<Trait> upperBodyTraits;
    public List<Trait> lowerBodyTraits;
    public List<Trait> legsTraits;

    // TODO: Assign traits to phenotypes
    public Trait GetTrait(Trait.SlotType traitSlot, Genotype genotype)
    {
        int traitIndex = 3;

        var phenotype = genotype.GetPhenotype();
        switch (phenotype)
        {
            case Phenotype.Blue:
                traitIndex = 0; // Trogdor
                break;
            case Phenotype.Green:
                traitIndex = 1; // Underwater
                break;
            case Phenotype.Purple:
                traitIndex = 2; // Robot 
                break;
            case Phenotype.Orange:
                traitIndex = 3; // No Phenotype
                break;
        }

        switch (traitSlot)
        {
            case Trait.SlotType.Head:
                return headTraits[traitIndex];
            case Trait.SlotType.Arms:
                return armTraits[traitIndex];
            case Trait.SlotType.UpperBody:
                return upperBodyTraits[traitIndex];
            case Trait.SlotType.LowerBody:
                return lowerBodyTraits[traitIndex];
            case Trait.SlotType.Legs:
                return legsTraits[traitIndex];
            default:
                return null; // ruh roh raggy
        }
    }

    public Trait GetRandomHead()
    {
        return headTraits.PickRandom();
    }

    public Trait GetRandomArm()
    {
        return armTraits.PickRandom();
    }

    public Trait GetRandomUpperBody()
    {
        return upperBodyTraits.PickRandom();
    }

    public Trait GetRandomLowerBody()
    {
        return lowerBodyTraits.PickRandom();
    }

    public Trait GetRandomLegs()
    {
        return legsTraits.PickRandom();
    }
}
