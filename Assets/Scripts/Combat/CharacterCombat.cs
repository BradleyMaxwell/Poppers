using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CharacterCombat : MonoBehaviour // the base class for all characters in the game
{
    [SerializeField] protected Character character;
    public int maxHealth;
    public int currentHealth;

    protected float lastBasicAttack; // the time the last basic attack was cast

    protected virtual void Awake() // made this protected override so subclasses also do this
    {
        maxHealth = character.health;
        currentHealth = maxHealth; // initialise the current health to be 100% of the max health
    }

    public virtual void TakeDamage(int damage) // can be called by other objects to inflict damage on the character object
    {
        currentHealth = currentHealth - damage;
        if (currentHealth <= 0)
        {
            Die();
        }
    }

    protected bool BasicAttackLockOutPassed () { // characters can call this function to check if the time of their attack lock out has passed or not
        if (Time.time >= lastBasicAttack + (1 / character.attackSpeed))
        {
            return true;
        } else
        {
            return false;
        }
    }

    protected virtual void BasicAttack() // the character needs to have a method to cast a basic attack
    {
        lastBasicAttack = Time.time; // change the time of the next basic attack
    }

    protected virtual void Die() // die function for character, will probably vary based off type of character
    {
    }

    public void IncreaseCurrentHealth (int amount) // increase the character's current health
    {
        if (currentHealth + amount > maxHealth)
        {
            currentHealth = maxHealth;
        } else
        {
            currentHealth += amount;
        }
    }
}