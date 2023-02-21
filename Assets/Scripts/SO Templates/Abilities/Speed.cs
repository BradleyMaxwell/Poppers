using UnityEngine;

[CreateAssetMenu(fileName = "Speed Ability", menuName = "Ability/Single Target/Speed")]
public class Speed : SingleTarget
{
    [Header("Speed Variables")]
    public float speedPercentage;

    public override void Use(GameObject target)
    {
        // how the speed ability works
    }
}
