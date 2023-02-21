using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class PoolObject : MonoBehaviour
{
    private Pool parentPool; // the pool that the pool object belongs to
    private NavMeshAgent navMeshAgent;

    private void Awake()
    {
        navMeshAgent = GetComponent<NavMeshAgent>();
    }

    private void OnDisable() // when this object is disabled, disable its navMeshAgent and return it to pool
    {
        navMeshAgent.enabled = false;
        parentPool.ReturnPoolObject(this);
    }

    public void SetParentPool (Pool pool) // sets the pool that the pool object is part of 
    {
        parentPool = pool;
    }

    public Pool GetParentPool () // retrieves object's parent pool
    {
        return parentPool;
    }
}
