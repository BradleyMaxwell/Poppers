using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Enemy", menuName = "Character/Enemy/Normal Enemy")]
public class Enemy : Character
{
    public enum EnemyType
    {
        Minion,
        Boss
    }

    [Header("Enemy Metadata")]
    public EnemyType enemyType;
}
