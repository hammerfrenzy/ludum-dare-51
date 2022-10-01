using DG.Tweening.Core.Easing;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class SnekController : MonoBehaviour
{
    Rigidbody2D rigidbody2d;
    float horizontal;
    float vertical;

    float disableMovementTimer = 0.2f;
    bool disableMovement = false;

    Animator animator;
    List<Trait> traits;
    
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

        Trait beefyArm = (Trait)AssetDatabase.LoadAssetAtPath("Assets/Sprites/Traits/Beefy Arm.asset", typeof(Trait));
        traits.Add(beefyArm);
        UnityEngine.Debug.Log(beefyArm.type);
    }

    // Update is called once per frame
    void Update()
    {
        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");

        animator.SetFloat("X Look", horizontal);

        // Stops snake from moving while potential mate animation plays???
        if (disableMovement)
        {
            disableMovementTimer -= Time.deltaTime;
            if (disableMovementTimer < 0)
            {
                disableMovement = false;
                disableMovementTimer = 0.2f;
            }
        }
    }

    void FixedUpdate()
    {
        if (disableMovement) return;
        Vector2 position = rigidbody2d.position;
        position.x = position.x + speed * horizontal * Time.deltaTime;
        position.y = position.y + speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void ResetSnek()
    {
        disableMovement = true;
        Vector2 position = new Vector2(0, 0);
        rigidbody2d.MovePosition(position);
    }

    public void RenderTraits()
    {
        foreach(var trait in traits)
        {

        }
    }
}
