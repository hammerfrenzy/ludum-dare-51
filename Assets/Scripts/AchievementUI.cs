using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;

public class AchievementUI : MonoBehaviour
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

    public bool visible;
    private RectTransform rectTransform;
    public GameObject uiCanvas;
    private Canvas canvas;
    private void Start()
    {
        canvas = uiCanvas.GetComponent<Canvas>();
        rectTransform = GetComponent<RectTransform>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            UpdateAchievements();
            rectTransform.DOAnchorPosX(32, 0.5f);
            canvas.sortingLayerName = "UI Over Player";
        }
        if (Input.GetKeyUp(KeyCode.Tab) || Input.GetKeyDown(KeyCode.LeftShift))
        {
            canvas.sortingLayerName = "UI";
            rectTransform.DOAnchorPosX(-600, 0.5f);
        }
    }

    public void UpdateAchievements()
    {
        foreach(var tupleThing in AchievementsTracker.tupleList)
        {
            switch(tupleThing.Item1)
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

    private void SetAchievementIcon(List<AchievementSlot> slots, Phenotype phenotype)
    {
        switch(phenotype)
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
