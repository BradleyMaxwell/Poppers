using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Green Boss", menuName = "Character/Enemy/Green Boss")]
public class GreenBoss : Enemy
{
    public void SharpenAim () // as time goes on, the green boss' range increases
    {
        Debug.Log("i have got a clearer sight on my target");
    }
}
