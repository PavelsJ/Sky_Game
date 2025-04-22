using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class RaceTimer : MonoBehaviour
{
    private bool timerActive = false;
    private float timer;
    private float finalTimer;
    
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

        if (Input.GetKeyDown(KeyCode.R))
        {
            PlayerPrefs.DeleteAll();
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
        finalTimer = timer;

        GameData.Instance.AddRace(finalTimer);
        Debug.Log("Race Timer Stopped");
    }

    private void Penalty()
    {
        timer += 10;
    }

    private void UpdateTimer()
    {
        timerText.text = timer.ToString("0.00");
        
        List<float> raceTimes = GameData.Instance.GetRaceTimes();
        if (raceTimes.Count == 0) return;

        float best = raceTimes[0]; 
        float worst = raceTimes[^1]; 

        if (timer < best)
        {
            timerText.color = Color.green;
        }
        else if (timer > worst)
        {
            timerText.color = Color.red;
        }
        else
        {
            timerText.color = Color.yellow;
        }
    }
}
