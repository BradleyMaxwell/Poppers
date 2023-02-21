using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class PlayerHealthbar : MonoBehaviour
{
    private Slider healthSlider;
    private PlayerCombat playerCombat;
    [SerializeField] private TextMeshProUGUI healthText;

    void Start()
    {
        healthSlider = GetComponent<Slider>(); // get the UI health bar slider
        playerCombat = GameObject.Find("Player").GetComponent<PlayerCombat>(); // get the player's combat script
    }

    // Update is called once per frame
    void Update()
    {
        // keep the healthbar updated with the player's max and current health values the whole game
        healthSlider.maxValue = playerCombat.maxHealth;
        healthSlider.value = playerCombat.currentHealth;
        healthText.text = playerCombat.currentHealth.ToString() + " / " + playerCombat.maxHealth.ToString();
    }
}
