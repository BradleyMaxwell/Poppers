using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Yellow Enemy", menuName = "Character/Enemy/Yellow Enemy")]
public class YellowEnemy : Enemy
{
    private void OnDisable()
    {
        PowerUpNearbyEnemies();
    }

    public void PowerUpNearbyEnemies ()
    {
        Debug.Log("powering up enemies around me");
    }
}
