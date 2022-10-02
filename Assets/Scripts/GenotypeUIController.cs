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

    private TraitsBankController traitBank;

    public void Start()
    {
        traitBank = FindObjectOfType<TraitsBankController>();
    }

    public void UpdateGenetics(Trait.SlotType slot, Genotype genotype, Trait phenotype)
    {
        switch (slot)
        {
            case Trait.SlotType.Head:
                UpdateUI(genotype, phenotype, HeadGenotypeUI, HeadPhenotypeImage);
                break;
            case Trait.SlotType.Arms:
                UpdateUI(genotype, phenotype, ArmsGenotypeUI, ArmsPhenotypeImage);
                break;
            case Trait.SlotType.UpperBody:
                UpdateUI(genotype, phenotype, UpperBodyGenotypeUI, UpperBodyPhenotypeImage);
                break;
            case Trait.SlotType.LowerBody:
                UpdateUI(genotype, phenotype, LowerBodyGenotypeUI, LowerBodyPhenotypeImage);
                break;
            case Trait.SlotType.Legs:
                UpdateUI(genotype, phenotype, LegsGenotypeUI, LegsPhenotypeImage);
                break;
        }
    }

    private void UpdateUI(Genotype genotype, Trait phenotype, GenotypeUI genotypeUI, Image phenotypeUI)
    {
        genotypeUI.SetGenotype(genotype);
        phenotypeUI.sprite = phenotype.phenotypeImage;
        phenotypeUI.color = phenotype.phenotypeImage == null ? Color.clear : Color.white;
    }
}
