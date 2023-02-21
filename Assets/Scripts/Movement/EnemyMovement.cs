using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(EnemyCombat), typeof(NavMeshAgent))]
public class EnemyMovement : CharacterMovement
{
    private Enemy enemy;
    private Transform target;
    public bool inAttackRange = false; // will be used by the enemy combat script to know when they are able to attack

    private NavMeshAgent navMeshAgent; // the nav mesh agent component attached to this enemy to control its movement

    void Awake()
    {
        enemy = (Enemy)character;
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void Start()
    {
        navMeshAgent.speed = enemy.movementSpeed; // set the nav mesh agent's speed to the enemy movement's speed
        navMeshAgent.stoppingDistance = character.range; // set the distance threshold where the enemy should stop moving to its attack range
        SetPlayerAsTarget();
    }

    // Update is called once per frame
    void Update()
    {
        navMeshAgent.SetDestination(target.position); // set player as destination for enemy
        if (!navMeshAgent.pathPending && navMeshAgent.remainingDistance <= character.range) // if the enemy is in attack range of the player and stopped, then signal that it is ready to attack
        {
            //Debug.Log($"player position = {target.position}, enemy position = {transform.position}. remaining distance = {navMeshAgent.remainingDistance}");
            inAttackRange = true;
            navMeshAgent.isStopped = true;
        }
        else
        {
            inAttackRange = false;
            navMeshAgent.isStopped = false;
        }
    }

    public NavMeshAgent GetNavMeshAgent () // retrieves this enemy's nav mesh agent
    {
        return navMeshAgent;
    }

    public void SetPlayerAsTarget () // set this object's target as the player's transform
    {
        target = GameObject.FindWithTag("Player").transform;
    }
}
