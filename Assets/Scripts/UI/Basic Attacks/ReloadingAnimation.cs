using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ReloadingAnimation : MonoBehaviour
{
    public float flickerSpeed = 2.5f;

    private PlayerCombat playerCombat;
    private Image background;
    [SerializeField] private Color backgroundColor;

    private void Awake()
    {
        background = GetComponent<Image>();
    }

    // Start is called before the first frame update
    void Start()
    {
        playerCombat = GameObject.FindWithTag("Player").GetComponent<PlayerCombat>();
        backgroundColor = background.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (playerCombat.reloading) // if the player is reloading, then flicker the basic attack UI element's background to white
        {
            background.color = Color.Lerp(backgroundColor, Color.white, Mathf.PingPong(Time.time * flickerSpeed, 1));
        }

        if (!playerCombat.reloading) // if the player is no longer reloading then reset the background color
        {
            background.color = backgroundColor;
        }
    }
}
