using UnityEngine;

public class HealAbility : PlayerAbility // this script controls the use of the heal ability on the player
{
    // private variables
    private Heal heal;
    private PlayerCombat playerCombat;

    protected override void Awake()
    {
        playerCombat = GetComponent<PlayerCombat>();
        heal = (Heal)ability; // converting ability to the heal so the script can call the specific heal Use function
        base.Awake();
    }

    protected override void Update()
    {
        base.Update();
    }

    protected override bool UseConditionMet()
    {
        return playerCombat.currentHealth < playerCombat.maxHealth; // make sure the ability cannot go off if the player has full health
    }

    protected override void UseAbility()
    {
        heal.Use(gameObject); // heal the holder of this script for the heal % stored in the scriptable object
    }
}
