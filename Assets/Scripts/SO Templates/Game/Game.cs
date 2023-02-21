using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Game", menuName = "Game/Game")]
public class Game : ScriptableObject // stores information about the round system in the game
{
    [Header("Variables")]
    public int round;
    public float roundBreak;
    public int enemiesThisRound;
    public int roundsPerBoss;
    public int bossesPerBossRound;
    public int additionalEnemiesNextRound;

    // base values
    [Header("Variable Base Values")]
    [SerializeField] private int baseEnemiesThisRound;
    [SerializeField] private int baseAdditionalEnemiesNextRound;
    [SerializeField] private int baseRoundsPerBoss;
    [SerializeField] private int baseBossesPerBossRound;

    // game methods

    public void Reset () // reset the round variables to the starting values
    {
        round = 1;
        enemiesThisRound = baseEnemiesThisRound;
        roundsPerBoss = baseRoundsPerBoss;
        bossesPerBossRound = baseBossesPerBossRound;
        additionalEnemiesNextRound = baseAdditionalEnemiesNextRound;
    }

    public void IncreaseEnemiesThisRound (int moreEnemies) // increase the amount of enemies that will spawn during the round
    {
        enemiesThisRound += moreEnemies;
    }

    public void ReduceRoundsPerBoss (int rounds) // reduce the amount of rounds between each boss round
    {
        if (roundsPerBoss - rounds < 1)
        {
            roundsPerBoss = 1; // make sure that it can only go as every round being a boss round
        } else
        {
            roundsPerBoss -= rounds;
        }
    }

    public void IncreaseBossesPerBossRound (int bosses) // increase how many boss will spawn on a boss round
    {
        if (bossesPerBossRound + bosses > 3)
        {
            bossesPerBossRound = 3; // make sure the max bosses spawned per boss round is no more than the total number of boss prefabs
        } else
        {
            bossesPerBossRound += bosses;
        }
    }

    public void IncreaseAdditionalEnemiesNextRound (int enemies) // increase the additional enemies that will be spawned in relation to the previous round
    {
        additionalEnemiesNextRound += enemies;
    }
}
