using UnityEngine;
using System.Collections;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public abstract class PlayerAbility : MonoBehaviour
{
    public enum AbilityState // all possible states a player's ability can be in
    {
        ready,
        active,
        cooldown
    }

    // variables
    public Ability ability; // the data for the ability
    private AbilityState state; // the current state of the ability at a given frame
    private float cooldownRemaining; // how long until the ability is ready again
    private float activeTimeRemaining; // how long until the ability is no longer active
    [SerializeField] private string action;
    private PlayerInput playerInput; // the player's controls
    [SerializeField] private Player player; // information about the player
    [SerializeField] private AbilityDisplay abilityDisplay; // the UI element that information about this ability will be stored

    protected virtual void Awake() // need to pass this to children aswell
    {
        state = AbilityState.ready; // initialise the ability as ready for use
        playerInput = GetComponent<PlayerInput>();
        abilityDisplay.SetUpAbilityDisplay(this); // pass this player ability script to the UI to set up the display
    }

    // Update is called once per frame
    protected virtual void Update() // this will be inherited by every child player ability
    {
        switch (state) // what to do based on what state the ability is in at a given frame
        {
            case AbilityState.ready:
                abilityDisplay.DisplayReady();
                if (playerInput.actions[action].IsPressed() && UseConditionMet())
                {
                    UseAbility();
                    state = AbilityState.active;
                    activeTimeRemaining = ability.activeTime;
                }
                break;
            case AbilityState.active:
                if (activeTimeRemaining > 0) // ability is still active
                {
                    activeTimeRemaining -= Time.deltaTime;
                } else // ability's active period has passed
                {
                    state = AbilityState.cooldown;
                    cooldownRemaining = GetAbilityCooldown();
                }
                break;
            case AbilityState.cooldown:
                abilityDisplay.DisplayCooldown(cooldownRemaining, GetAbilityCooldown());
                if (cooldownRemaining > 0) // ability is still on cooldown
                {
                    cooldownRemaining -= Time.deltaTime;
                } else // ability is now off cooldown
                {
                    state = AbilityState.ready;
                }
                break;
        }
    }

    protected virtual bool UseConditionMet () // overridable in case the child player abilities have a specific condition that needs to be met before using ability
    {
        return true;
    }

    protected abstract void UseAbility(); // needed so that the child player abilities will implement their own way of calling the ability's use function

    public float GetAbilityCooldown ()
    {
        return ability.cooldown - (ability.cooldown * player.cooldownReduction);
    }
}
