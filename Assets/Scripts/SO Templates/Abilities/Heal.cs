using UnityEngine;
using System.Collections;

[CreateAssetMenu(fileName = "Heal Ability", menuName = "Ability/Single Target/Heal")]
public class Heal : SingleTarget
{
    [Header("Heal Variables")]
    public float healPercentage;

    public override void Use(GameObject character) // heal the player for a % of their max health
    {
        CharacterCombat characterCombat = character.GetComponent<CharacterCombat>();
        int healAmount = CalcPercentageValue(characterCombat.maxHealth, healPercentage); // calculate how much to heal player based on player's max health
        characterCombat.IncreaseCurrentHealth(healAmount);
    }
}
