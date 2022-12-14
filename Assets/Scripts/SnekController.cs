using DG.Tweening;
using System;
using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SnekController : MonoBehaviour
{
    // Trait Slots
    public TraitSlotController headSlotController;
    public TraitSlotController armsSlotController;
    public TraitSlotController upperBodySlotController;
    public TraitSlotController lowerBodySlotController;
    public TraitSlotController legsSlotController;

    public List<TraitSlotController> TraitControllers
    {
        get
        {
            return new List<TraitSlotController>()
             {
                headSlotController,
                armsSlotController,
                upperBodySlotController,
                lowerBodySlotController,
                legsSlotController
            };
        }
    }

    // Editor Hookups
    public DeathAnimController deathAnim;
    public AudioClip deathScribble;
    public AudioClip eggTear;
    public AudioClip heartBreak;
    public AudioClip snekSnek;
    public GenotypeUIController genotypeUI;
    public GameObject HeartGroup;
    private List<SpriteRenderer> heartSprites;

    // Stats
    public float speed = 3.0f;
    private float speedFromTraits = 0f;
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
    private AudioSource audioSource;
    private Animator animator;
    private MateController currentMate;
    private AchievementUI achievementUI;
    private AudioManager audioManager;

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
        audioSource = GetComponents<AudioSource>()[0];
        gameManager = FindObjectOfType<GameManagerController>();
        achievementUI = FindObjectOfType<AchievementUI>();
        traitBank = FindObjectOfType<TraitsBankController>();
        heartSprites = HeartGroup.GetComponentsInChildren<SpriteRenderer>().ToList();
        audioManager = FindObjectOfType<AudioManager>();

        StartCoroutine(RotateJankyForever(0));
        StartCoroutine(FlipHeartsForever());

    }

    private IEnumerator RotateJankyForever(float initialZRotation)
    {
        var moveRight = true;
        var maxOffset = 3;
        var zOffset = maxOffset;
        transform.rotation = Quaternion.Euler(0, 0, initialZRotation);

        yield return null;

        while (true)
        {
            transform.rotation = Quaternion.Euler(0, 0, initialZRotation + zOffset);
            if (zOffset == maxOffset) moveRight = false;
            if (zOffset == -maxOffset) moveRight = true;
            zOffset = moveRight ? zOffset + maxOffset : zOffset - maxOffset;

            yield return new WaitForSeconds(0.5f);
        }
    }

    // Update is called once per frame
    void Update()
    {
        audioSource.volume = 0f;
        if (disableMovement) return;

        horizontal = Input.GetAxis("Horizontal");
        vertical = Input.GetAxis("Vertical");
        if (!audioManager.muteSfx && (horizontal != 0 || vertical != 0))
        {
            audioSource.volume = 0.75f;
        }
        didPressMate = Input.GetKey("space");

        UpdateHearts();
        UpdateSpriteFlip(horizontal);
    }

    void FixedUpdate()
    {
        if (disableMovement) return;
        Vector2 position = rigidbody2d.position;
        var finalSpeed = speed + speedFromTraits;
        position.x += finalSpeed * horizontal * Time.deltaTime;
        position.y += finalSpeed * vertical * Time.deltaTime;
        rigidbody2d.MovePosition(position);
    }

    public void SetTrait(TraitSlotController mateTraitController)
    {
        Tuple<Genotype, Trait> crossResult;
        TraitSlotController controller;
        switch (mateTraitController.slotType)
        {
            case Trait.SlotType.Head:
                crossResult = CrossTraits(headSlotController, mateTraitController);
                controller = headSlotController;
                break;
            case Trait.SlotType.Arms:
                crossResult = CrossTraits(armsSlotController, mateTraitController);
                controller = armsSlotController;
                break;
            case Trait.SlotType.UpperBody:
                crossResult = CrossTraits(upperBodySlotController, mateTraitController);
                controller = upperBodySlotController;
                break;
            case Trait.SlotType.LowerBody:
                crossResult = CrossTraits(lowerBodySlotController, mateTraitController);
                controller = lowerBodySlotController;
                break;
            case Trait.SlotType.Legs:
                crossResult = CrossTraits(legsSlotController, mateTraitController);
                controller = legsSlotController;
                break;
            default:
                return;
        }

        var genotype = crossResult.Item1;
        var phenotype = crossResult.Item2;
        AchievementsTracker.AddAchievement(mateTraitController.slotType, genotype, phenotype);
        controller.ApplyCrossResults(genotype, phenotype);
        genotypeUI.UpdateGenetics(mateTraitController.slotType, genotype, phenotype);

        var phenotypeColor = genotype.GetPhenotype();
        speedFromTraits += phenotypeColor != Phenotype.Orange ? 0.5f : 0f;

        // TODO: Update stats based on the new trait?
    }

    public void KillSnek()
    {
        animator.SetTrigger("DeathTrigger");
        disableMovement = true;
        speedFromTraits = 0;
    }

    public void DisplayGameOver()
    {
        bruh.Invoke();
    }

    public void SendToWinScreenBox()
    {
        transform.position = new Vector3(6.5f, 3f, 0f);
        StopAllCoroutines();
        StartCoroutine(RotateJankyForever(10));
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
            PlaySnekSnek();
        }

        didPressMate = false;
    }

    // Remove current mate when we leave so hearts update
    private void OnTriggerExit2D(Collider2D collision)
    {
        currentMate = null;
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

    private void UpdateHearts()
    {
        var showHearts = currentMate != null;
        foreach (var spriteRenderer in heartSprites)
        {
            spriteRenderer.enabled = showHearts;
        }
    }

    private IEnumerator FlipHeartsForever()
    {
        while (true)
        {
            var currentXScale = HeartGroup.transform.localScale.x;
            HeartGroup.transform.localScale = new Vector3(-currentXScale, 1, 1);
            yield return new WaitForSeconds(0.5f);
        }
    }

    private void Metaphase(MateController mate)
    {
        speedFromTraits = 0;
        foreach (var mateTraitController in mate.traitControllerList)
        {
            // seems like null controllers are making it in here somehow?
            // it soft locks the game when this happens so just skip for now. 
            if (mateTraitController == null) return;

            SetTrait(mateTraitController);
        }
    }

    private Tuple<Genotype, Trait> CrossTraits(TraitSlotController snekTraitController, TraitSlotController mateTraitController)
    {
        var snekGenotype = snekTraitController.genotype;
        var mateGenotype = mateTraitController.genotype;
        var newGenotype = snekGenotype.CrossedWith(mateGenotype);
        var newPhenotype = traitBank.GetTrait(snekTraitController.slotType, newGenotype);
        return Tuple.Create(newGenotype, newPhenotype);
    }

    #region Scene Resetting

    // Called by GameManagerController when the user finds a mate
    public void DisableMovement()
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
        achievementUI.UpdateAchievements();
        currentMate = null;
        disableMovement = false;
    }

    public bool CheckWinCondition()
    {
        // Check if any slots do not have traits
        if (TraitControllers.Any(slot => !slot.HasTrait()))
        {
            return false;
        }
        return true;
    }

    public Phenotype GetPrimaryPhenotype()
    {
        if (TraitControllers.All(slot => slot.genotype.GetPhenotype() == Phenotype.Blue))
        {
            return Phenotype.Blue;
        }
        else if (TraitControllers.All(slot => slot.genotype.GetPhenotype() == Phenotype.Purple))
        {
            return Phenotype.Purple;
        }
        else if (TraitControllers.All(slot => slot.genotype.GetPhenotype() == Phenotype.Green))
        {
            return Phenotype.Green;
        }
        else
        {
            return Phenotype.Orange;
        }
    }
    #endregion


    #region Sounds

    public void PlayDeathSound()
    {
        FindObjectOfType<AudioManager>().Play("Scribble");
    }

    public void PlayHeartbreakSound()
    {
        FindObjectOfType<AudioManager>().Play("Heartbreak");
    }

    public void PlaySnekSnek()
    {
        FindObjectOfType<AudioManager>().Play("SnekSnek");
    }

    public void PlayHatchSound()
    {
        FindObjectOfType<AudioManager>().Play("Egg Tear");
    }

    #endregion
}
