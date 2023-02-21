using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBasicAttacks : MonoBehaviour
{
    public PlayerCombat playerCombat;
    private Slider basicAttacksSlider;

    private void Awake()
    {
        basicAttacksSlider = GetComponent<Slider>(); // gets the slider component of the basic attacks UI element
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GameObject.FindWithTag("Player").GetComponent<PlayerCombat>(); // get the active player object's player combat script
    }

    // Update is called once per frame
    void Update()
    {
        basicAttacksSlider.maxValue = playerCombat.player.attacksPerReload;
        basicAttacksSlider.value = playerCombat.attacksRemaining;
    }

}
