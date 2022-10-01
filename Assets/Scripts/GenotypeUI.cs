using UnityEngine;
using UnityEngine.UI;

public class GenotypeUI : MonoBehaviour
{
    public Sprite DominantA;
    public Sprite RecessiveA;
    public Sprite DominantB;
    public Sprite RecessiveB;

    public Image AlleleA1Image;
    public Image AlleleA2Image;
    public Image AlleleB1Image;
    public Image AlleleB2Image;

    void Start()
    {
        AlleleA1Image.sprite = RecessiveA;
        AlleleA2Image.sprite = RecessiveA;
        AlleleB1Image.sprite = RecessiveB;
        AlleleB2Image.sprite = RecessiveB;
    }

    public void SetGenotype(Genotype genotype)
    {
        AlleleA1Image.sprite = genotype.alleleA1 ? DominantA : RecessiveA;
        AlleleA2Image.sprite = genotype.alleleA2 ? DominantA : RecessiveA;
        AlleleB1Image.sprite = genotype.alleleB1 ? DominantB : RecessiveB;
        AlleleB2Image.sprite = genotype.alleleB2 ? DominantB : RecessiveB;
    }
}
