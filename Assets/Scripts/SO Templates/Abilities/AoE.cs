using UnityEngine;
using System.Collections;

public abstract class AoE : Ability
{
    [Header("AoE Variables")]
    public float radius;

    public abstract void Use(Transform origin, float radius);
}
