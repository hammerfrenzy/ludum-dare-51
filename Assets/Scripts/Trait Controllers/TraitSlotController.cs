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

    public void SetTrait(Trait trait, Genotype genotype)
    {
        currentTrait = trait;

        if (trait == null)
        {
            spriteRenderer.sprite = null;
            animator.runtimeAnimatorController = null;
            return;
        }

        spriteRenderer.sprite = trait.image;
        animator.runtimeAnimatorController = trait.animator;

        this.genotype = genotype;
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
