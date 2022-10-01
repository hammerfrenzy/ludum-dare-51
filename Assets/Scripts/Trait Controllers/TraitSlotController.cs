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
    public Trait currentTrait;

    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void SetTrait(Trait trait)
    {
        currentTrait = trait;
        spriteRenderer.sprite = trait.image;
        animator.runtimeAnimatorController = trait.animator;
    }

    public void SetIsFlipped(bool isFlipped)
    {
        spriteRenderer.flipX = isFlipped;
    }
}
