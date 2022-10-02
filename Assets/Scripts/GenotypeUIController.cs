using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GenotypeUIController : MonoBehaviour
{
    public GenotypeUI HeadGenotypeUI;
    public GenotypeUI ArmsGenotypeUI;
    public GenotypeUI UpperBodyGenotypeUI;
    public GenotypeUI LowerBodyGenotypeUI;
    public GenotypeUI LegsGenotypeUI;

    public Image HeadPhenotypeImage;
    public Image ArmsPhenotypeImage;
    public Image UpperBodyPhenotypeImage;
    public Image LowerBodyPhenotypeImage;
    public Image LegsPhenotypeImage;

    public void SetGenotype(Trait.SlotType slot, Genotype genotype)
    {
        var phenotype = genotype.GetPhenotype();

        switch (slot)
        {
            case Trait.SlotType.Head:
                HeadGenotypeUI.SetGenotype(genotype);
                break;
            case Trait.SlotType.Arms:
                ArmsGenotypeUI.SetGenotype(genotype);
                break;
            case Trait.SlotType.UpperBody:
                UpperBodyGenotypeUI.SetGenotype(genotype);
                break;
            case Trait.SlotType.LowerBody:
                LowerBodyGenotypeUI.SetGenotype(genotype);
                break;
            case Trait.SlotType.Legs:
                LegsGenotypeUI.SetGenotype(genotype);
                break;
        }
    }
}
