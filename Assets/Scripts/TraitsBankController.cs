using System.Collections.Generic;
using UnityEngine;

public class TraitsBankController : MonoBehaviour
{
    public List<Trait> headTraits;
    public List<Trait> armTraits;
    public List<Trait> upperBodyTraits;
    public List<Trait> lowerBodyTraits;
    public List<Trait> legsTraits;

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
