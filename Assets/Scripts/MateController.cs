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
    public List<TraitSlotController> traitControllerList
    {
        get
        {
            return new List<TraitSlotController>()
            {
                headSlotController,
                armsSlotController,
                upperBodySlotController,
                lowerBodySlotController,
                legsSlotController,
            };
        }
    }

    private TraitsBankController traitsBank;
    private SpriteRenderer spriteRenderer;


    private Tween wanderTween;

    public void GetComponentsDuringSpawn()
    {
        traitsBank = FindObjectOfType<TraitsBankController>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        // One-time setup for sprite flipping
        headSlotController.SetIsFlipped(true);
        armsSlotController.SetIsFlipped(true);
        upperBodySlotController.SetIsFlipped(true);
        lowerBodySlotController.SetIsFlipped(true);
        legsSlotController.SetIsFlipped(true);

        StartCoroutine(RotateJankyForever());
        StartCoroutine(WanderForever());
    }

    void OnDestroy()
    {
        wanderTween?.Kill();
    }

    public void AddTraitsPreferring(SnekController snekController, int ringNumber)
    {
        var traitCount = 0;
        var randomizedTraits = traitControllerList;
        randomizedTraits.Shuffle();

        var maxTraits = 2;
        switch (ringNumber)
        {
            case 3:
                maxTraits = 3;
                break;
            case 4:
            case 5:
            case 6:
                maxTraits = 4;
                break;
        }

        foreach (var controller in randomizedTraits)
        {
            var traitChance = 0.55f;
            var giveTrait = Random.Range(0f, 1f) < traitChance;
            if (!giveTrait) return;

            var genotype = ChooseGenotypeV2(snekController, controller.slotType, ringNumber);
            var phenotype = traitsBank.GetTrait(controller.slotType, genotype);

            SetTrait(controller.slotType, phenotype, genotype);

            if (genotype.GetPhenotype() != Phenotype.Orange)
            {
                traitCount++;
            }

            // farther rings have more traits
            var minTraits = Mathf.Min(2, ringNumber);
            var canGainMoreTraits = traitCount < minTraits || traitCount < maxTraits;
            if (!canGainMoreTraits) break;
        }
    }

    private Genotype ChooseGenotypeV1()
    {
        return Genotype.Randomized();
    }

    private Genotype ChooseGenotypeV2(SnekController snekController, Trait.SlotType slot, int ringNumber)
    {
        // Start with a 50% chance to share a trait,
        // reduced by 10% per ring level 
        float useSnekGenotypePercent = 0.5f - (ringNumber * 0.1f);
        var roll = Random.Range(0f, 1f);
        if (roll > useSnekGenotypePercent) return ChooseGenotypeV1();

        switch (slot)
        {
            case Trait.SlotType.Head:
                return SameUnlessRecessive(snekController.headSlotController.genotype);
            case Trait.SlotType.Arms:
                return SameUnlessRecessive(snekController.armsSlotController.genotype);
            case Trait.SlotType.UpperBody:
                return SameUnlessRecessive(snekController.upperBodySlotController.genotype);
            case Trait.SlotType.LowerBody:
                return SameUnlessRecessive(snekController.lowerBodySlotController.genotype);
            case Trait.SlotType.Legs:
                return SameUnlessRecessive(snekController.legsSlotController.genotype);
            default:
                return ChooseGenotypeV1();
        }
    }

    // It feels bad to be surrounded by a bunch of partless werums,
    // so if our trait is fully recessive let's just ignore it.
    private Genotype SameUnlessRecessive(Genotype genotype)
    {
        var phenotypeColor = genotype.GetPhenotype();
        return phenotypeColor != Phenotype.Orange ? genotype : ChooseGenotypeV1();
    }

    private void SetTrait(Trait.SlotType traitSlot, Trait phenotype, Genotype genotype)
    {
        switch (traitSlot)
        {
            case Trait.SlotType.Head:
                headSlotController.ApplyCrossResults(genotype, phenotype);
                break;
            case Trait.SlotType.Arms:
                armsSlotController.ApplyCrossResults(genotype, phenotype);
                break;
            case Trait.SlotType.UpperBody:
                upperBodySlotController.ApplyCrossResults(genotype, phenotype);
                break;
            case Trait.SlotType.LowerBody:
                lowerBodySlotController.ApplyCrossResults(genotype, phenotype);
                break;
            case Trait.SlotType.Legs:
                legsSlotController.ApplyCrossResults(genotype, phenotype);
                break;
        }
    }

    // Moves the object to a random point up to 
    // one unit circle distance away, and then
    // starts toward a new target upon completion.  
    private IEnumerator WanderForever()
    {
        var targetPosition = transform.position + (Vector3)Random.insideUnitCircle;
        var finishedWander = false;
        wanderTween = transform
            .DOMove(targetPosition, 2f)
            .SetDelay(Random.Range(0, 0.5f))
            .OnComplete(() =>
            {
                finishedWander = true;
            });

        while (!finishedWander)
        {
            yield return null;
        }

        wanderTween.Kill();
        StartCoroutine(WanderForever());
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
