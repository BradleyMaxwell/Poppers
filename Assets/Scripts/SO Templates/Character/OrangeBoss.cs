using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Orange Boss", menuName = "Character/Enemy/Orange Boss")]
public class OrangeBoss : Enemy
{
    public void ConsumeMinion ()
    {
        Debug.Log("I have consumed an enemy minion");
    }

    public void FeedingFrenzy ()
    {
        Debug.Log("I am in a feeding frenzy");
    }
}
