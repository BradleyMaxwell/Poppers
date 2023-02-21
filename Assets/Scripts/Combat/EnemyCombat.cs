using UnityEngine;
using System.Collections;

[RequireComponent(typeof(EnemyMovement))]
public class EnemyCombat : CharacterCombat
{
    private Enemy enemy;

    // attributes
    private PlayerCombat playerCombat; // used to create combat interaction between player and enemy
    private EnemyMovement enemyMovement; // used to know when the enemy is ready to attack the player
    private EnemyDamageTextHandler enemyDamageTextHandler; // the script that will handle the floating damage text for an enemy

    // mono behaviour methods
    protected override void Awake()
    {
        base.Awake();
        enemyMovement = GetComponent<EnemyMovement>();
        enemyDamageTextHandler = GetComponent<EnemyDamageTextHandler>();
        enemy = (Enemy)character;
    }

    private void Start()
    {
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>(); // get the player's combat script
    }

    private void Update()
    {
        if (enemyMovement.inAttackRange && BasicAttackLockOutPassed())
        {
            BasicAttack();
        }
    }

    // methods
    protected override void Die() // if the enemy dies, then disable the pool object script to return it to its pool
    {
        gameObject.SetActive(false);
        currentHealth = maxHealth; // resets the object's health so that when it spawns again it is full health
        enemyMovement.inAttackRange = false;
        UpdateGameManager();
    }

    protected override void BasicAttack ()
    {
        base.BasicAttack();
        playerCombat.TakeDamage(enemy.damage);
    }

    private void UpdateGameManager () // updates the round and spawner components of the game manager since an enemy has died
    {
        GameObject gameManager = GameObject.Find("GameManager");
        gameManager.GetComponent<Rounds>().ReduceEnemiesRemaining(); // reduce the amount of enemies remaining for that round
        gameManager.GetComponent<Spawner>().ReduceActiveEnemies(); // reduce the amount of active enemies
    }

    //public override void takeDamage(int damage, string attacker) // override so that when the enemy takes damage, it creates floating damage text
    //{
    //    base.takeDamage(damage, attacker);
    //    //enemyDamageTextHandler.NewDamage(damage); // pass the damage to the enemy damage text handler
    //}
}
