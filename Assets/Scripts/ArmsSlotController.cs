using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmsSlotController : MonoBehaviour
{
    // Start is called before the first frame update
    SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    public void SetTrait(Trait trait)
    {
        spriteRenderer.sprite = trait.image;
    }
}
