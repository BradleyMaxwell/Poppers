using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rounds))]
public class Spawner : MonoBehaviour // spawner that is responsible for spawning when the game is being played
{
    [SerializeField] private Game game;
    [SerializeField] private Spawn spawn;

    // only kept here because I could not put it in the scriptable object because they store objects that live in the scene
    [SerializeField] private Transform[] spawnPoints;
    [SerializeField] private List<PoolObject> enemyPrefabs = new List<PoolObject>(); // stores the prefabs of all the enemies that can spawn
    [SerializeField] private List<PoolObject> bossPrefabs = new List<PoolObject>(); // stores the boss prefabs that can spawn

    private Dictionary<string, Pool> enemyPools = new Dictionary<string, Pool>(); // stores all the objects pools of the different types of enemies
    private Dictionary<string, Pool> bossPools = new Dictionary<string, Pool>(); // stores all the objects pools of the different types of bosses
    private int activeEnemies = 0; // counter for how many enemies are active at one time
    private List<Pool> alreadySpawned = new List<Pool>(); // this will keep track of what bosses have been spawned already so that the 5th, 10th and 15th round bosses are unique

    private void Awake()
    {
        // before the rounds start, set up the initial pools of enemies
        for (int i = 0; i < enemyPrefabs.Count; i++)
        {
            PoolObject currentPrefab = enemyPrefabs[i];
            enemyPools.Add(currentPrefab.name, Pool.CreatePool(currentPrefab, GetSufficientPoolSize(currentPrefab.name))); // give each pool a number of object it should create
        }

        // instantiate the boss objects too. Only one boss of each type can be spawned per round so there will only ever be need of 1 pool object
        for (int i = 0; i < bossPrefabs.Count; i++)
        {
            PoolObject currentPrefab = bossPrefabs[i];
            bossPools.Add(currentPrefab.name, Pool.CreatePool(currentPrefab, bossPrefabs.Count)); // the max bosses that will be spawned is 1 x number of boss types
        }
    }

    public IEnumerator StartSpawning(int enemiesToSpawn, int bossesToSpawn) // when this coroutine is called, the spawner will start choosing enemies from the list of pools to spawn
    {
        // spawn the bosses at the start of the round if the round manager wants to spawn bosses this round
        if (bossesToSpawn > 0)
        {
            for (int i = 0; i < bossesToSpawn; i++) // spawn a certain amount of bosses
            {
                Pool chosenBossPool = spawn.ChoosePoolToSpawnFrom(bossPools, spawn.bossPoolProbabilities);
                if (game.round <= 15) // if the boss round is either the 5th, 10th or 15th, then make sure the boss being spawned is unique
                {
                    while (alreadySpawned.Contains(chosenBossPool) == true) // keep re-rolling until a boss is found that has not already been spawned
                    {
                        chosenBossPool = spawn.ChoosePoolToSpawnFrom(bossPools, spawn.bossPoolProbabilities);
                    }
                    alreadySpawned.Add(chosenBossPool); // mark this pool as already spawned this round
                }

                Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
                spawn.SpawnEnemy(spawnPoint, chosenBossPool); // will choose a random boss to spawn
            }
        }

        // spawn the enemies for the round
        for (int i = 0; i < enemiesToSpawn; i++)
        {
            yield return new WaitUntil(() => spawn.maxActiveEnemies > activeEnemies); // pause the spawner until there is room for more to spawn 
            // pick a random spawn point and random pool to spawn from (based on probability), then spawn enemy
            Transform spawnPoint = spawnPoints[Random.Range(0, spawnPoints.Length)];
            spawn.SpawnEnemy(spawnPoint, spawn.ChoosePoolToSpawnFrom(enemyPools, spawn.enemyPoolProbabilities)); // spawns a random enemy from weighted probabilities at a random spawn point on the map
            activeEnemies++;
            yield return new WaitForSeconds(spawn.spawnRate);
        }
    }

    private int GetSufficientPoolSize (string enemyName) // calculates the expected number of objects that will be active in relation to the max active enemy amount + 50%
    {
        // find the probability of the enemy spawning
        float enemyProbability = spawn.enemyPoolProbabilities[enemyName];
        int expectedEnemiesWhenMax = Mathf.CeilToInt(enemyProbability * spawn.maxActiveEnemies); // calculates expected active enemies of the given enemy name when the total active enemies = max active enemies
        int sufficientPoolSize = Mathf.CeilToInt(expectedEnemiesWhenMax * 1.5f); // the actual amount can deviate from the expected amount, so adding 50% more objects minimising any runtime instantiations
        return sufficientPoolSize;
    }

    public void ReduceActiveEnemies () // called by enemies when they die to reduce the counter for how many active enemies there are
    {
        if (activeEnemies - 1 < 0)
        {
            activeEnemies = 0;
        } else
        {
            activeEnemies--;
        }
    }
 }
