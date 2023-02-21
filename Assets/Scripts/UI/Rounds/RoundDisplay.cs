using UnityEngine;
using TMPro;

public class RoundDisplay : MonoBehaviour
{
    public Game game;
    private TextMeshProUGUI roundDisplay;

    private void Awake()
    {
        roundDisplay = GetComponent<TextMeshProUGUI>(); // get the component of the UI element that shows the text
    }

    // Update is called once per frame
    void Update()
    {
        roundDisplay.text = game.round.ToString(); // make the UI element's text display the current round
    }
}
