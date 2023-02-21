using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Pink Boss", menuName = "Character/Enemy/Pink Boss")]
public class PinkBoss : Enemy
{
    public void Isolated () // take more damage when isolated
    {
        Debug.Log("I am isolated");
    }

    public void LonelyRage () // after being isolated for a certain period of time, have a meltdown until surrounded by other enemies for a period
    {

    }
}
