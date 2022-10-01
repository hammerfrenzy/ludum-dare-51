using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeathAnimController : MonoBehaviour
{
    // Start is called before the first frame update
    public SpriteRenderer spriteRenderer;
    void Start()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }
}
