using UnityEngine;

public abstract class Ability : ScriptableObject
{
    [Header("General Ability Variables")]
    public new string name;
    public float cooldown;
    public Sprite icon;
    public Color backgroundColor;
    public float activeTime;
    public string tooltip;

    // useful functions that may be used by child abilities
    public int CalcPercentageValue (int value, float percentage) // percentage must be greater than 0 and less than 1
    {
        return Mathf.CeilToInt(value * percentage); 
    }
}
