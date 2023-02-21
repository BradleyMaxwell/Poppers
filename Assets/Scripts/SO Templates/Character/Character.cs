using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Character : ScriptableObject
{
    public new string name;

    [Header("Combat")]
    public int damage;
    public int health;
    public float attackSpeed;
    public float range;

    [Header("Movement")]
    public float movementSpeed;
}
