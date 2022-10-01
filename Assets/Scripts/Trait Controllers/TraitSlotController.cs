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

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void SetTrait(Trait trait)
    {
        currentTrait = trait;

        if(trait == null)
        {
            spriteRenderer.sprite = null;
            animator.runtimeAnimatorController = null;
            return;
        }

        spriteRenderer.sprite = trait.image;
        animator.runtimeAnimatorController = trait.animator;
    }

    public void SetIsFlipped(bool isFlipped)
    {
        spriteRenderer.flipX = isFlipped;
    }
}
