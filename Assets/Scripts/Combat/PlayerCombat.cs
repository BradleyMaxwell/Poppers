using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(PlayerInput))]
public class PlayerCombat : CharacterCombat
{
    [HideInInspector] public Player player;
    public int attacksRemaining;
    [HideInInspector] public bool reloading = false;
    private PlayerInput playerInput;
    private PlayerCamera playerCamera;
    private InputAction basicAttackAction;
    private InputAction reloadAction;
    [SerializeField] private ParticleSystem basicAttackAnimation;

    protected override void Awake()
    {
        base.Awake();

        player = (Player)character; // re-use the character scriptable object for descendant scripts
        attacksRemaining = player.attacksPerReload;

        // getting player inputs
        playerInput = GetComponent<PlayerInput>();
        playerCamera = GetComponent<PlayerCamera>();
        basicAttackAction = playerInput.actions["BasicAttack"];
        reloadAction = playerInput.actions["Reload"];
    }

    // Update is called once per frame
    void Update()
    {
        if (reloading) // this stops the reload coroutine to be called more than once per reload
        {
            return;
        }

        if (attacksRemaining <= 0 || (reloadAction.triggered && attacksRemaining != player.attacksPerReload)) // if the player has run out of basic attacks or presses reload button, then reload
        {
            StartCoroutine(Reload()); // coroutine allow simple time delays opposed to regular functions
            return; // returning so that it does not carry on to allow basic attacks whilst reloading
        }

        if (basicAttackAction.IsPressed()) // if player presses basic attack button then check if they can basic attack in that frame before casting
        {
            if (BasicAttackLockOutPassed())
            {
                BasicAttack();
            }
        }
    }

    protected override void BasicAttack ()
    {
        base.BasicAttack();
        basicAttackAnimation.Play();
        attacksRemaining = attacksRemaining - 1;

        // if the player is aiming at an enemy that is within its attack range when the basic attack is cast, then make that enemy take damage  
        EnemyCombat enemy = playerCamera.enemyTarget;
        if (enemy)
        {
            enemy.TakeDamage(player.damage);
        }
    }

    IEnumerator Reload () // reload weapon after a certain amount of time specified by the player's reload time
    {
        reloading = true;
        yield return new WaitForSeconds(player.reloadTime); // wait for the given reload time before reloading
        attacksRemaining = player.attacksPerReload;
        reloading = false;
    }

    protected override void Die()
    {
    }
}
