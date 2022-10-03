using DG.Tweening;
using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenuAchievements : MonoBehaviour
{
    #region Bruh
    // Start is called before the first frame update
    public AchievementSlot headCyborg;
    public AchievementSlot headSea;
    public AchievementSlot headTrogdor;

    private List<AchievementSlot> headSlots
    {
        get
        {
            return new List<AchievementSlot>()
             {
                headCyborg,
                headSea,
                headTrogdor
            };
        }
    }

    public AchievementSlot armsCyborg;
    public AchievementSlot armsSea;
    public AchievementSlot armsTrogdor;

    private List<AchievementSlot> armSlots
    {
        get
        {
            return new List<AchievementSlot>()
             {
                armsCyborg,
                armsSea,
                armsTrogdor
            };
        }
    }

    public AchievementSlot upperBodyCyborg;
    public AchievementSlot upperBodySea;
    public AchievementSlot upperBodyTrogdor;

    private List<AchievementSlot> upperBodySlots
    {
        get
        {
            return new List<AchievementSlot>()
             {
                upperBodyCyborg,
                upperBodySea,
                upperBodyTrogdor
            };
        }
    }

    public AchievementSlot lowerBodyCyborg;
    public AchievementSlot lowerBodySea;
    public AchievementSlot lowerBodyTrogdor;

    private List<AchievementSlot> lowerBodySlots
    {
        get
        {
            return new List<AchievementSlot>()
             {
                lowerBodyCyborg,
                lowerBodySea,
                lowerBodyTrogdor
            };
        }
    }

    public AchievementSlot legsCyborg;
    public AchievementSlot legsSea;
    public AchievementSlot legsTrogdor;

    private List<AchievementSlot> legsBodySlots
    {
        get
        {
            return new List<AchievementSlot>()
             {
                legsCyborg,
                legsSea,
                legsTrogdor
            };
        }
    }
    #endregion

    public AchievementStar goldStar;
    public AchievementStar silverStar;
    public AchievementStar blueStar;
    public AchievementStar purpleStar;
    public AchievementStar greenStar;
    public AchievementStar redStar;
    private List<AchievementStar> achievemntStars
    {
        get
        {
            return new List<AchievementStar>()
            {
                silverStar,
                blueStar,
                purpleStar,
                greenStar,
                redStar
            };
        }
    }

    public bool visible = false;
    private RectTransform rectTransform;
    private Tween displayTween;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        SetAchievementStars();
    }

    void Update()
    {
        if (visible)
        {
            UpdateAchievements();
            displayTween?.Kill();
            displayTween = rectTransform.DOAnchorPosX(-1, 0.5f);
        }
        else
        {
            displayTween?.Kill();
            displayTween = rectTransform.DOAnchorPosX(-1144, 0.5f);
        }
    }

    // this is a called by the UI,
    // don't be fooled into thinking 
    // it's an unused function.
    public void ToggleVisibility()
    {
        visible = !visible;
    }

    void OnDestroy()
    {
        displayTween?.Kill();
    }

    public void UpdateAchievements()
    {
        foreach (var tupleThing in AchievementsTracker.tupleList)
        {
            switch (tupleThing.Item1)
            {
                case Trait.SlotType.Head:
                    SetAchievementIcon(headSlots, tupleThing.Item2);
                    break;
                case Trait.SlotType.Arms:
                    SetAchievementIcon(armSlots, tupleThing.Item2);
                    break;
                case Trait.SlotType.UpperBody:
                    SetAchievementIcon(upperBodySlots, tupleThing.Item2);
                    break;
                case Trait.SlotType.LowerBody:
                    SetAchievementIcon(lowerBodySlots, tupleThing.Item2);
                    break;
                case Trait.SlotType.Legs:
                    SetAchievementIcon(legsBodySlots, tupleThing.Item2);
                    break;
            }
        }
    }

    public void SetAchievementStars()
    {
        if(AchievementsTracker.tupleList.Count >= 15)
        {
            silverStar.starEnabled = true;
        }
        if(AchievementsTracker.trogEnd)
        {
            blueStar.starEnabled = true;
        }
        if (AchievementsTracker.seaEnd)
        {
            greenStar.starEnabled = true;
        }
        if (AchievementsTracker.techEnd)
        {
            purpleStar.starEnabled = true;
        }
        if (AchievementsTracker.miscEnd)
        {
            redStar.starEnabled = true;
        }
        

        if (achievemntStars.All(star => star.starEnabled))
        {
            goldStar.starEnabled = true;
        }
    }

    private void SetAchievementIcon(List<AchievementSlot> slots, Phenotype phenotype)
    {
        switch (phenotype)
        {
            case Phenotype.Purple:
                slots[0].Test();
                break;
            case Phenotype.Green:
                slots[1].Test();
                break;
            case Phenotype.Blue:
                slots[2].Test();
                break;
            case Phenotype.Orange:
                break;

        }
    }
}
