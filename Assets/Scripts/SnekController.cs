using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Trait;

public class SnekController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;
    bool disableMovement = false;

    Animator animator;

    // Trait Slots
    public ArmsSlotController armsSlotController;

    // Stats
    public float speed = 3.0f;
    public float attractiveness = 1.0f;
    public float intimidation = 1.0f;
    public int maxHealth = 10;
    public int currentHealth;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (disableMovement) return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("X Look", horizontal);
    }

    void FixedUpdate()
    {
        if (disableMovement) return;
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void SetTrait(Trait trait)
    {
        switch (trait.type)
        {
            case SlotType.Arms:
                {
                    armsSlotController.SetTrait(trait);
                    break;
                }
        }
    }

    // Called by GameManagerController when the user finds a mate
    public void StartMating()
    {
        disableMovement = true;
    }

    // Called by GameManagerController partway through the scene reset process
    public void Reset()
    {
        transform.position = Vector3.zero;
    }

    // Called by GameManagerController when the reset process is completed
    public void EndMating()
    {
        disableMovement = false;
    }
}
