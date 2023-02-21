    using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class DamageText : MonoBehaviour
{
    private TextMeshPro damageText;

    private void Awake()
    {
        damageText = GetComponent<TextMeshPro>(); // gets the text mesh pro component of the prefab
    }

    public void Set (int damage) // creates a new damage text prefab instance with a given damage amount
    {
        damageText.SetText(damage.ToString());
    }

    public string Get () // return the existing damage value shown
    {
        return damageText.text;
    }
}
