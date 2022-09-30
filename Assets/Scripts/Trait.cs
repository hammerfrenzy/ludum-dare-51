using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Trait", menuName = "Trait", order = 1)]
public class Trait : ScriptableObject
{
    public new string name;
    public Sprite image;
    public Vector2 position;
    public int speedModifier;
    public int attractivenessModifier;
    public int intimidationModifier;
    public int healthModifier;
    public float probability;
}
