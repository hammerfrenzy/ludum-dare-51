using DG.Tweening;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MateController : MonoBehaviour
{
    // Trait Slots
    public TraitSlotController headSlotController;
    public TraitSlotController armsSlotController;
    public TraitSlotController upperBodySlotController;
    public TraitSlotController lowerBodySlotController;
    public TraitSlotController legsSlotController;

    private TraitsBankController traitsBank;
    private SpriteRenderer spriteRenderer;

    public List<TraitSlotController> traitSlotControllers;

    private Tween wanderTween;

    public void GetComponentsDuringSpawn()
    {
        traitsBank = FindObjectOfType<TraitsBankController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        WanderAround();

        // One-time setup for sprite flipping
        headSlotController.SetIsFlipped(true);
        armsSlotController.SetIsFlipped(true);
        upperBodySlotController.SetIsFlipped(true);
        lowerBodySlotController.SetIsFlipped(true);
        legsSlotController.SetIsFlipped(true);

        traitSlotControllers.Add(headSlotController);
        traitSlotControllers.Add(armsSlotController);
        traitSlotControllers.Add(upperBodySlotController);
        traitSlotControllers.Add(lowerBodySlotController);
        traitSlotControllers.Add(legsSlotController);

        StartCoroutine(RotateJankyForever());
    }

    public void AddTraitsPreferring(SnekController snekController)
    {
        Debug.Log("Assigning traits");
        // For now, choose a random trait from available options
        // (arm, leg, and upper body have an implement trait).
        // This will need to be improved once more traits are available.

        foreach (var controller in traitSlotControllers)
        {
            // TODO: use the genotype from snek sometimes
            // TODO: If they're far away, give something good?
            var genotype = Genotype.Randomized();
            var phenotype = traitsBank.GetTrait(controller.slotType, genotype);

            SetTrait(controller.slotType, phenotype, genotype);
        }
    }

    private void SetTrait(Trait.SlotType traitSlot, Trait phenotype, Genotype genotype)
    {
        switch (traitSlot)
        {
            case Trait.SlotType.Head:
                headSlotController.SetTrait(phenotype, Genotype.Randomized());
                UnityEngine.Debug.Log(headSlotController.currentTrait);
                break;
            case Trait.SlotType.Arms:
                armsSlotController.SetTrait(phenotype, Genotype.Randomized());
                UnityEngine.Debug.Log(armsSlotController.currentTrait);
                break;
            case Trait.SlotType.UpperBody:
                upperBodySlotController.SetTrait(phenotype, Genotype.Randomized());
                UnityEngine.Debug.Log(upperBodySlotController.currentTrait);
                break;
            case Trait.SlotType.LowerBody:
                lowerBodySlotController.SetTrait(phenotype, Genotype.Randomized());
                UnityEngine.Debug.Log(lowerBodySlotController.currentTrait);
                break;
            case Trait.SlotType.Legs:
                legsSlotController.SetTrait(phenotype, Genotype.Randomized());
                UnityEngine.Debug.Log(legsSlotController.currentTrait);
                break;
        }
    }

    // Moves the object to a random point up to 
    // one unit circle distance away, and then
    // starts toward a new target upon completion.  
    private void WanderAround()
    {
        wanderTween?.Kill();

        var targetPosition = transform.position + (Vector3)Random.insideUnitCircle;

        wanderTween = transform
            .DOMove(targetPosition, 2f)
            .SetDelay(Random.Range(0, 0.5f))
            .OnComplete(() =>
            {
                WanderAround();
            });
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

        // Mate frame is looking the opposite direction
        // of the Snek frame parts, so we need to invert
        // the way these are facing.
        headSlotController.SetIsFlipped(!newFlipState);
        armsSlotController.SetIsFlipped(!newFlipState);
        upperBodySlotController.SetIsFlipped(!newFlipState);
        lowerBodySlotController.SetIsFlipped(!newFlipState);
        legsSlotController.SetIsFlipped(!newFlipState);
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
}
