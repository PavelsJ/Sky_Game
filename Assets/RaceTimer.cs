using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    private bool timerActive = false;
    private float timer;
    
    public TextMeshProUGUI timerText;

    private void OnEnable()
    {
        GameManager.OnStartRace += StartRaceTimer;
        GameManager.OnEndRace += StopRaceTimer;
        GameManager.OnPenalty += Penalty;
    }

    private void OnDisable()
    {
        GameManager.OnStartRace -= StartRaceTimer;
        GameManager.OnEndRace -= StopRaceTimer;
        GameManager.OnPenalty -= Penalty;
    }

    private void Update()
    {
        if (timerActive)
        {
            timer += Time.deltaTime;
            UpdateTimer();
        }
    }

    private void StartRaceTimer()
    {
        timerActive = true;
        Debug.Log("Race Timer Started");
    }

    private void StopRaceTimer()
    {
        timerActive = false;
        Debug.Log("Race Timer Stopped");
    }

    private void Penalty()
    {
        timer += 10;
    }

    private void UpdateTimer()
    {
        timerText.text = timer.ToString("0.00");
    }
}
