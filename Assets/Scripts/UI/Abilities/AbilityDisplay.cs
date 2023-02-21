using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class AbilityDisplay : MonoBehaviour // script that takes care of displaying a player ability and its state on the UI
{
    [SerializeField] private Image icon;
    [SerializeField] private TextMeshProUGUI cooldownRemainingDisplay;

    // what the ability script can call on the UI element that is displaying it
    public void SetUpAbilityDisplay (PlayerAbility playerAbility)
    {
        icon.sprite = playerAbility.ability.icon;
    }

    public void DisplayReady ()
    {
        icon.fillAmount = 1;
        cooldownRemainingDisplay.text = ""; // do not show any cooldown text
    }

    public void DisplayCooldown (float cooldownRemaining, float cooldown)
    {
        icon.fillAmount = 1 - CalcCooldownPercentage(cooldownRemaining, cooldown);
        int secondsRemaining = Mathf.FloorToInt(cooldownRemaining % 60);
        if (secondsRemaining > 0) // only display the seconds remaining if there is 1 or more seconds remaining on the cooldown
        {
            cooldownRemainingDisplay.text = $"{secondsRemaining}";
        }
    }

    // helper functions
    private float CalcCooldownPercentage(float remainingCooldown, float cooldown) // calculate a percentage value between 0 and 1 to set the fill amount
    {
        return remainingCooldown / cooldown;
    }
}
