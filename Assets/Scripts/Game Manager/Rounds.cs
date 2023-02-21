using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Spawner), typeof(Modifier))]
public class Rounds : MonoBehaviour
{
    private Spawner spawner; // run time spawner
    [SerializeField] private Game game; // game data and methods
    [SerializeField] private Spawn spawn; // spawner data and methods
    private bool roundComplete = false;
    private RoundCountdown roundCountdown;
    private bool lastRoundWasBossRound = false;
    private int enemiesRemaining;
    private int roundsSinceBoss = 0; // keeps track of how many rounds has passed since the last boss round

    private void Awake()
    {
        // when the game first starts, set the game and spawner data to their base values
        game.Reset();
        spawn.Reset();
        SetEnemiesRemaining(); // makes sure that remaining enemies is not 0 before the first frame so it does not immediately go into the 2nd round
        spawner = GetComponent<Spawner>();
    }

    private void Start()
    {
        roundCountdown = GameObject.Find("RoundCountdown").GetComponent<RoundCountdown>(); // retrieves the round countdown script of the UI element
        roundCountdown.StartCountdown(game.roundBreak); // start the countdown for the first round
    }

    // Update is called once per frame
    void Update()
    {
        if (enemiesRemaining <= 0 && !roundComplete) // boolean is used to make sure that this logic only happens once in the frame it is found
        {
            roundComplete = true;
            PrepareNextRound();
        }
    }

    private void PrepareNextRound () // starts the preparation phase for the next round
    {
        game.round++; // go onto the next round
        if (IsBossRound())
        {
            roundsSinceBoss = 0;
            lastRoundWasBossRound = true; // used to indicate to the prepare stage of the next round that the last round was a boss round
            ChangesBeforeBossRound();
        } else // for non boss rounds
        {
            roundsSinceBoss++; // if its not a boss round, then it means that another round has passed since the last boss round
            lastRoundWasBossRound = false;
        }

        if (lastRoundWasBossRound) // if the round is the round after a boss round, then execute the changes that needed to be applied after the boss was defeated
        {
            ChangesAfterBossRound();
        }
        game.enemiesThisRound += game.additionalEnemiesNextRound; // increase the number of enemies that will be spawned next round
        spawn.SpeedUp(0.2f); // a lower spawn rate means that the time between each spawn is quicker, so faster spawning
        roundCountdown.StartCountdown(game.roundBreak); // starts a timer on the RoundCountdown script that will make a call to the game manager to start the next round when the timer ends
    }

    public void StartRound() // adjust all the stats before starting the next round after a certain break period
    {
        roundComplete = false;
        SetEnemiesRemaining();
        if (IsBossRound()) // if it is a boss round, then spawn bosses too
        {
            StartCoroutine(spawner.StartSpawning(game.enemiesThisRound, game.bossesPerBossRound));
        } else
        {
            StartCoroutine(spawner.StartSpawning(game.enemiesThisRound, 0));
        }
    }

    public void ReduceEnemiesRemaining ()
    {
        enemiesRemaining--;
    }

    private void SetEnemiesRemaining ()
    {
        if (IsBossRound())
        {
            enemiesRemaining = game.enemiesThisRound + game.bossesPerBossRound; // include the bosses being spawned as part of the enemies remaining counter
        } else
        {
            enemiesRemaining = game.enemiesThisRound;
        }
    }

    private void ChangesBeforeBossRound () // these changes need to come into effect during the boss round
    {
        if (game.round >= 15) // rounds start getting hard
        {
            game.ReduceRoundsPerBoss(1); // reduce the rounds in between bosses
        }

        if (game.round >= 25 && game.round % 5 == 0) // increase the bosses per round on round 25 to 2 and then on 30 to all bosses
        {
            game.IncreaseBossesPerBossRound(1); // after round 25
        }
    }

    private void ChangesAfterBossRound () // these changes need to come into effect after the boss has been defeated
    {
        game.IncreaseAdditionalEnemiesNextRound(1);
        //modifier.enemyModifier += 0.05f; // add 5% to the enemy stat increase modifier
    }

    private bool IsBossRound () // determine if a given round is a boss round
    {
        if (roundsSinceBoss == game.roundsPerBoss - 1) // - 1 because if for e.g. boss round is every 5 rounds, then 4 rounds will have passed since the last boss round
        {
            return true;
        } else
        {
            return false;
        }
    }
}
