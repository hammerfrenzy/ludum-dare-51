using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsSlotController : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    Animator animator;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
    }

    public void SetTrait(Trait trait)
    {
        spriteRenderer.sprite = trait.image;
        animator.runtimeAnimatorController = trait.animator;
    }
}
