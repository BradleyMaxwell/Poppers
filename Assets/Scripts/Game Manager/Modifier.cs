using UnityEngine;
using System.Collections;

public class Modifier : MonoBehaviour
{
    // player data
    [SerializeField] private Player player;

    // enemy data
    [SerializeField] private Enemy redEnemy;
    [SerializeField] private Enemy yellowEnemy;
    [SerializeField] private Player purpleEnemy;
    [SerializeField] private Enemy orangeBoss;
    [SerializeField] private Player greenBoss;
    [SerializeField] private Player pinkBoss;

    // modifier variables
    [SerializeField] private float playerModifier = 1.15f; // % increases for the player's applicable stats
    [SerializeField] private float enemyModifier = 1f; // % modifier for the enemy's stat increases

    // methods
    public void ScaleCharacters () // called during round preparation to scale the game
    {
        ScaleEnemies();
        ScalePlayer();
    }

    private void ScaleEnemies () // overrides the enemy and boss prefab's stats using the modifier to make their instances stronger
    {
        IncreaseStats(redEnemy);
        IncreaseStats(yellowEnemy);
        IncreaseStats(purpleEnemy);
        IncreaseStats(orangeBoss);
        IncreaseStats(greenBoss);
        IncreaseStats(pinkBoss);
    }

    private void IncreaseStats (Character character) // increase stats of given scriptable object using correct modifier
    {
        float modifier;
        if ((Player)character)
        {
            modifier = playerModifier;
        } else
        {
            modifier = enemyModifier;
        }
        character.damage = Mathf.CeilToInt(character.damage * modifier);
        character.damage = Mathf.CeilToInt(character.damage * modifier);
    }

    private void ScalePlayer () // scale up the damage and health of the player with the player modifier
    {
        IncreaseStats(player);
    }
}
