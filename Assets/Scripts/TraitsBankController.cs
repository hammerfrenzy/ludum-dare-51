using System.Collections.Generic;
using UnityEngine;

public class TraitsBankController : MonoBehaviour
{
    public List<Trait> headTraits;
    public List<Trait> armTraits;
    public List<Trait> upperBodyTraits;
    public List<Trait> lowerBodyTraits;
    public List<Trait> legsTraits;

    public Trait GetTrait(Trait.SlotType traitSlot, Genotype genotype)
    {
        switch (traitSlot)
        {
            case Trait.SlotType.Head:
                return GetHeadTrait(genotype);
            case Trait.SlotType.Arms:
                return GetArmsTrait(genotype);
            case Trait.SlotType.UpperBody:
                return GetUpperBodyTrait(genotype);
            case Trait.SlotType.LowerBody:
                return GetLowerBodyTrait(genotype);
            case Trait.SlotType.Legs:
                return GetLegsTrait(genotype);
            default:
                return null; // ruh roh raggy
        }
    }

    private Trait GetHeadTrait(Genotype genotype)
    {
        var phenotype = genotype.GetPhenotype();
        switch (phenotype)
        {
            case Phenotype.Blue:
                return null;
            case Phenotype.Green:
                return null;
            case Phenotype.Purple:
                return null;
            case Phenotype.Orange:
                return null;
            default:
                return null;
        }
    }

    private Trait GetArmsTrait(Genotype genotype)
    {
        var phenotype = genotype.GetPhenotype();
        switch (phenotype)
        {
            case Phenotype.Blue:
                return null;
            case Phenotype.Green:
                return null;
            case Phenotype.Purple:
                return null;
            case Phenotype.Orange:
                return null;
            default:
                return null;
        }
    }

    private Trait GetUpperBodyTrait(Genotype genotype)
    {
        var phenotype = genotype.GetPhenotype();
        switch (phenotype)
        {
            case Phenotype.Blue:
                return null;
            case Phenotype.Green:
                return null;
            case Phenotype.Purple:
                return null;
            case Phenotype.Orange:
                return null;
            default:
                return null;
        }
    }

    private Trait GetLowerBodyTrait(Genotype genotype)
    {
        var phenotype = genotype.GetPhenotype();
        switch (phenotype)
        {
            case Phenotype.Blue:
                return null;
            case Phenotype.Green:
                return null;
            case Phenotype.Purple:
                return null;
            case Phenotype.Orange:
                return null;
            default:
                return null;
        }
    }

    private Trait GetLegsTrait(Genotype genotype)
    {
        var phenotype = genotype.GetPhenotype();
        switch (phenotype)
        {
            case Phenotype.Blue:
                return null;
            case Phenotype.Green:
                return null;
            case Phenotype.Purple:
                return null;
            case Phenotype.Orange:
                return null;
            default:
                return null;
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
