using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using static Trait;

public class SnekController : MonoBehaviour
{
    // Trait Slots
    public TraitSlotController headSlotController;
    public TraitSlotController armsSlotController;
    public TraitSlotController upperBodySlotController;
    public TraitSlotController lowerBodySlotController;
    public TraitSlotController legsSlotController;

    // Stats
    public float speed = 3.0f;
    public float attractiveness = 1.0f;
    public float intimidation = 1.0f;
    public int maxHealth = 10;
    public int currentHealth;

    // Private members
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;
    private GameManagerController gameManager;
    private float horizontal;
    private float vertical;
    private bool didPressMate = false;
    private bool disableMovement = false;

    // Start is called before the first frame update
    void Start()
    {
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManagerController>();

        StartCoroutine(RotateJankyForever());
    }

    private IEnumerator RotateJankyForever()
    {
        var moveRight = true;
        var maxRotation = 3;
        var zRotation = maxRotation;
        while (true)
        {
            transform.rotation = Quaternion.Euler(0, 0, zRotation);
            if (zRotation == maxRotation) moveRight = false;
            if (zRotation == -maxRotation) moveRight = true;
            zRotation = moveRight ? zRotation + maxRotation : zRotation - maxRotation;

            yield return new WaitForSeconds(0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (disableMovement) return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        didPressMate = Input.GetKey("space");

        UpdateSpriteFlip(horizontal);
    }

    void FixedUpdate()
    {
        if (disableMovement) return;
        Vector2 position = rigidbody2d.position;
        position.x += speed * horizontal * Time.deltaTime;
        position.y += speed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void SetTrait(TraitSlotController slot)
    {
        Trait potentialTrait = slot.currentTrait;
        UnityEngine.Debug.Log(slot.slotType);
        UnityEngine.Debug.Log(potentialTrait);
        switch (slot.slotType)
        {
            case SlotType.Head:
                DecideTrait(headSlotController, potentialTrait);
                break;
            case SlotType.Arms:
                DecideTrait(armsSlotController, potentialTrait);
                break;
            case SlotType.UpperBody:
                DecideTrait(upperBodySlotController, potentialTrait);
                break;
            case SlotType.LowerBody:
                DecideTrait(lowerBodySlotController, potentialTrait);
                break;
            case SlotType.Legs:
                DecideTrait(legsSlotController, potentialTrait);
                break;
        }
        // TODO: Update stats based on the new trait?
    }

    // Note: This is working because didPressMate is
    // using GetKey over GetKeyDown. GetKeyDown will
    // only be active for the frame it is pressed,
    // and seems to be running after this callback.
    private void OnTriggerStay2D(Collider2D collision)
    {
        var mate = collision.GetComponent<MateController>();

        if (mate != null && didPressMate)
        {
            Metaphase(mate);
            gameManager.MateReset();
        }

        didPressMate = false;
    }

    private void UpdateSpriteFlip(float horizontalInput)
    {
        var isFlipped = spriteRenderer.flipX;

        bool newFlipState;
        if (isFlipped && horizontalInput > 0)
        {
            newFlipState = false;
        }
        else if (!isFlipped && horizontalInput < 0)
        {
            newFlipState = true;
        }
        else
        {
            return; // No update to flip state
        }

        spriteRenderer.flipX = newFlipState;
        headSlotController.SetIsFlipped(newFlipState);
        armsSlotController.SetIsFlipped(newFlipState);
        upperBodySlotController.SetIsFlipped(newFlipState);
        lowerBodySlotController.SetIsFlipped(newFlipState);
        legsSlotController.SetIsFlipped(newFlipState);
    }

    private void Metaphase(MateController mate)
    {
        foreach(var slot in mate.traitSlotControllers)
        {
            SetTrait(slot);
        }
    }

    private void DecideTrait(TraitSlotController slotController, Trait potentialTrait)
    {
        // TODO: punnet square / more complicated random decision thing here in the future.
        // If you need to access the trait currently in the slotController: slotController.currentTrait.
        var coinFlip = Random.Range(0, 2);
        if (coinFlip == 1)
        {
            slotController.SetTrait(potentialTrait);
        }
    }
    #region Scene Resetting

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

    #endregion
}
