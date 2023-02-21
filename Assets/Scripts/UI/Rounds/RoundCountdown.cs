using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class RoundCountdown : MonoBehaviour
{
    private TextMeshProUGUI roundCountdownDisplay;
    private float timeLeft;
    private bool timerOn;
    private Rounds rounds;

    private void Awake()
    {
        roundCountdownDisplay = GetComponent<TextMeshProUGUI>();
    }

    private void Start()
    {
        rounds = GameObject.Find("GameManager").GetComponent<Rounds>();
    }

    private void Update()
    {
        if (timerOn)
        {
            if (timeLeft > 1)
            {
                timeLeft -= Time.deltaTime; // reduce the time remaining on the timer independant of the frame rate, so the round countdown is accurate for every user
                roundCountdownDisplay.text = $"starting in {Mathf.FloorToInt(timeLeft % 60)}"; // shows the time remaining on the countdown clock in seconds form
            } else
            {
                roundCountdownDisplay.text = null; // remove the countdown text from the screen
                timerOn = false; // hide the timer element on the UI
                rounds.StartRound(); // once the timer has finished, then start the next round
            }
        }
    }

    public void StartCountdown (float roundBreak) // start a count down on the screen for the next round with the given round break duration
    {
        timeLeft = roundBreak;
        timerOn = true;
    }
}
