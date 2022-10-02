using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AchievementUI : MonoBehaviour
{
    // Start is called before the first frame update
    public AchievementSlot headCyborg;
    public AchievementSlot headSea;
    public AchievementSlot headTrogdor;

    public AchievementSlot armsCyborg;
    public AchievementSlot armsSea;
    public AchievementSlot armsTrogdor;

    public AchievementSlot upperBodyCyborg;
    public AchievementSlot upperBodySea;
    public AchievementSlot upperBodyTrogdor;

    public AchievementSlot lowerBodyCyborg;
    public AchievementSlot lowerBodySea;
    public AchievementSlot lowerBodyTrogdor;

    public AchievementSlot legsCyborg;
    public AchievementSlot legsSea;
    public AchievementSlot legsTrogdor;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {

    }

    public void AddAchievement(Genotype newGenotype, Trait phenotype, Trait.SlotType slotType)
    {

    }
}
