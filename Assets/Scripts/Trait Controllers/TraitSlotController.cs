using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
[RequireComponent(typeof(Animator))]
public class TraitSlotController : MonoBehaviour
{
    // Start is called before the first frame update
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    public Trait.SlotType slotType;
    public Trait currentTrait;
    public Genotype genotype;

    void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void ApplyCrossResults(Genotype genotype, Trait phenotype)
    {
        this.genotype = genotype;
        currentTrait = phenotype;

        // DO NOT CHANGE THIS ORDER OR THE ANIMATOR WILL
        // OVERWRITE THE SPRITE RENDERER WITH ITS FINAL FRAME?
        // WHICH ISN'T A BIG DEAL EXCEPT WHEN GOING FROM SOME
        // TRAIT TO NO TRAIT AND THEN YOU GET STUCK WITH THE
        // FINAL FRAME OF THE PREVIOUS TRAIT WOW COOL.
        animator.runtimeAnimatorController = phenotype.animator;
        spriteRenderer.sprite = phenotype.image;
    }

    public void SetIsFlipped(bool isFlipped)
    {
        spriteRenderer.flipX = isFlipped;
    }

    public bool HasTrait()
    {
        var phenotype = genotype.GetPhenotype();
        return phenotype != Phenotype.Orange;
    }
}
