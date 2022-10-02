using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using static Trait;

public class SnekController : MonoBehaviour
{
    // Trait Slots
    public TraitSlotController headSlotController;
    public TraitSlotController armsSlotController;
    public TraitSlotController upperBodySlotController;
    public TraitSlotController lowerBodySlotController;
    public TraitSlotController legsSlotController;

    // Editor Hookups
    public DeathAnimController deathAnim;
    public GenotypeUIController genotypeUI;

    // Stats
    public float speed = 3.0f;
    public float attractiveness = 1.0f;
    public float intimidation = 1.0f;
    public int maxHealth = 10;
    public int currentHealth;

    public UnityEvent bruh;

    // Private members
    private Rigidbody2D rigidbody2d;
    private SpriteRenderer spriteRenderer;
    private GameManagerController gameManager;
    private TraitsBankController traitBank;

    private Animator animator;
    private MateController currentMate;

    private float horizontal;
    private float vertical;
    private bool didPressMate = false;
    private bool disableMovement = false;


    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rigidbody2d = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        gameManager = FindObjectOfType<GameManagerController>();
        traitBank = FindObjectOfType<TraitsBankController>();
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

    public void SetTrait(TraitSlotController mateTraitController)
    {
        Trait potentialTrait = mateTraitController.currentTrait;
        Genotype? genotype = null;
        switch (mateTraitController.slotType)
        {
            case SlotType.Head:
                genotype = CrossTraits(headSlotController, mateTraitController);
                break;
            case SlotType.Arms:
                genotype = CrossTraits(armsSlotController, mateTraitController);
                break;
            case SlotType.UpperBody:
                genotype = CrossTraits(upperBodySlotController, mateTraitController);
                break;
            case SlotType.LowerBody:
                genotype = CrossTraits(lowerBodySlotController, mateTraitController);
                break;
            case SlotType.Legs:
                genotype = CrossTraits(legsSlotController, mateTraitController);
                break;
        }

        genotypeUI.SetGenotype(mateTraitController.slotType, genotype.Value);

        // TODO: Update stats based on the new trait?
    }

    public void KillSnek()
    {
        animator.SetTrigger("DeathTrigger");
        disableMovement = true;
    }

    public void DisplayGameOver()
    {
        bruh.Invoke();
    }

    // Note: This is working because didPressMate is
    // using GetKey over GetKeyDown. GetKeyDown will
    // only be active for the frame it is pressed,
    // and seems to be running after this callback.
    private void OnTriggerStay2D(Collider2D collision)
    {
        currentMate = collision.GetComponent<MateController>();

        if (currentMate != null && didPressMate)
        {
            animator.SetTrigger("MateTrigger");
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
        deathAnim.spriteRenderer.flipX = newFlipState;
    }

    private void Metaphase(MateController mate)
    {
        foreach (var mateTraitController in mate.traitSlotControllers)
        {
            SetTrait(mateTraitController);
        }
    }

    private Genotype CrossTraits(TraitSlotController snekTraitController, TraitSlotController mateTraitController)
    {
        var snekGenotype = snekTraitController.genotype;
        var mateGenotype = mateTraitController.genotype;
        var newGenotype = snekGenotype.CrossedWith(mateGenotype);
        var newPhenotype = traitBank.GetTrait(snekTraitController.slotType, newGenotype);
        snekTraitController.SetTrait(newPhenotype, newGenotype);

        return newGenotype;
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
        Metaphase(currentMate);
    }

    // Called by GameManagerController when the reset process is completed
    public void EndMating()
    {
        currentMate = null;
        disableMovement = false;
    }

    #endregion
}
