using UnityEngine;

public struct Genotype
{
    public bool alleleA1;
    public bool alleleA2;
    public bool alleleB1;
    public bool alleleB2;

    public Genotype(bool alleleA1, bool alleleA2, bool alleleB1, bool alleleB2)
    {
        this.alleleA1 = alleleA1;
        this.alleleA2 = alleleA2;
        this.alleleB1 = alleleB1;
        this.alleleB2 = alleleB2;
    }

    public static Genotype Randomized()
    {
        return new Genotype(
            Random.Range(0, 2) == 0,
            Random.Range(0, 2) == 0,
            Random.Range(0, 2) == 0,
            Random.Range(0, 2) == 0
        );
    }

    public Genotype CrossedWith(Genotype mateGenotype)
    {
        return new Genotype(
            this.ChooseAlleleA(),
            mateGenotype.ChooseAlleleA(),
            this.ChooseAlleleB(),
            mateGenotype.ChooseAlleleB()
        );
    }

    public override string ToString()
    {
        var a1 = alleleA1 ? "A" : "a";
        var a2 = alleleA2 ? "A" : "a";
        var b1 = alleleB1 ? "B" : "b";
        var b2 = alleleB2 ? "B" : "b";
        return $"{a1}{a2}{b1}{b2}";
    }

    public Phenotype GetPhenotype()
    {
        if (!(alleleA1 || alleleA2 || alleleB1 || alleleB2))
        {
            return Phenotype.Orange;
        }

        if (!(alleleA1 || alleleA2) && (alleleB1 || alleleB2))
        {
            return Phenotype.Green;
        }

        if (!(alleleB1 || alleleB2) && (alleleA1 || alleleA2))
        {
            return Phenotype.Purple;
        }

        return Phenotype.Blue;
    }

    private bool ChooseAlleleA()
    {
        return Random.Range(0, 2) == 0 ? alleleA1 : alleleA2;
    }

    private bool ChooseAlleleB()
    {
        return Random.Range(0, 2) == 0 ? alleleB1 : alleleB2;
    }
}

public enum Phenotype
{
    Blue,   // 10 (no double recessive)
    Purple, // 3 (any A, recessive B)
    Green,  // 3 (recessive A, any B)
    Orange  // 1 (all recessive)
}
