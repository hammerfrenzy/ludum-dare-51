using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trait", menuName = "Trait", order = 1)]
public class Trait : ScriptableObject
{
    public enum SlotType
    {
        Head,
        Arms,
        UpperBody,
        LowerBody,
        Legs
    }

    public new string name;
    public Sprite image;
    public Sprite phenotypeImage;
    public RuntimeAnimatorController animator;
    public Vector2 offset;
    public int speedModifier;
    public int attractivenessModifier;
    public int intimidationModifier;
    public int healthModifier;
    public float probability;
    public SlotType type;
}
