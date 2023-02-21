using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Player", menuName = "Character/Player")]
public class Player : Character
{
    [Header("Player Specific Combat")]
    public int cooldownReduction;
    public int attacksPerReload;
    public int reloadTime;

    [Header("Player Specific Movement")]
    public float jumpHeight;
}
