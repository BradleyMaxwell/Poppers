using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[CreateAssetMenu(fileName = "Spawner", menuName = "Game/Spawner")]
public class Spawn : ScriptableObject // stores the game's spawner data and methods
{
    public float spawnRate;
    public float minimumSpawnRate;
    public Dictionary<string, float> enemyPoolProbabilities = new Dictionary<string, float>() { // probability a spawner will spawn a certain enemy type. Values add to 1
        { "Red Enemy", 0.7f },
        { "Yellow Enemy", 0.15f },
        { "Purple Enemy", 0.15f }
    };
    public Dictionary<string, float> bossPoolProbabilities = new Dictionary<string, float>() {
        { "Orange Boss", 0.33333f },
        { "Green Boss", 0.33333f },
        { "Pink Boss", 0.33334f }
    };
    public int maxActiveEnemies; // the most enemies that can be active on the map at one time
    [Header("Variable Base Values")]
    [SerializeField] private float baseSpawnRate;

    // spawner methods
    public void Reset () // reset the spawner
    {
        spawnRate = baseSpawnRate;
    }

    public void SpeedUp (float seconds) // speed the spawner up by x seconds per spawn
    {
        if (spawnRate - seconds < minimumSpawnRate)
        {
            spawnRate = minimumSpawnRate;
        }
        else
        {
            spawnRate -= seconds;
        }
        spawnRate -= seconds;
    }

    public void SpawnEnemy(Transform spawnPoint, Pool pool) // find the nearest location on the NavMesh to the spawn point, then spawn an object from the given pool
    {
        bool navMeshPointFound = NavMesh.SamplePosition(spawnPoint.position, out NavMeshHit navMeshPoint, 1f, NavMesh.AllAreas); // attempt to find the nearest NavMesh point to the spawn point
        if (navMeshPointFound)
        {
            PoolObject poolObject = pool.GetPoolObject();
            EnemyMovement enemyMovement = poolObject.GetComponent<EnemyMovement>();
            NavMeshAgent navMeshAgent = enemyMovement.GetNavMeshAgent();
            navMeshAgent.Warp(navMeshPoint.position);
            navMeshAgent.enabled = true;
        }
    }

    public Pool ChoosePoolToSpawnFrom(Dictionary<string, Pool> pools, Dictionary<string, float> probabilities) // based on the spawner's pools, determine the next pool to spawn an enemy to spawn based off probabilities
    {
        //// generate a random float between 0 and 1.
        float randomValue = Random.value;
        float accumulation = 0; // will be compared with the random value to see if the random value corresponds with the current enemy being checked

        //// go through every probability in the probability dictionary to find what enemy should be spawned
        foreach (KeyValuePair<string, float> probability in probabilities)
        {
            accumulation += probability.Value;
            if (randomValue <= accumulation)
            {
                return pools[probability.Key];
            }
        }
        Debug.Log("something went wrong"); // the code should never get to this line, but just in case
        return null;
    }
}
