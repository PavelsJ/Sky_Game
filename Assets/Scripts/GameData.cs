using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameData : MonoBehaviour
{
    private static GameData instance;
    
    public Transform leaderBoard;
    public GameObject racePrefab;
    private const int maxRaces = 5;

    private List<float> raceTimes = new List<float>();
    private string SceneKeyPrefix => SceneManager.GetActiveScene().name;
    
    private void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(gameObject);
        }
        else
        {
            instance = this;    
            DontDestroyOnLoad(gameObject);
        }
    }
    
    public static GameData Instance
    {
        get { return instance; }
    }
    
    private void Start()
    {
        LoadScores();
        UpdateLeaderboardUI();
    }

    public void AddRace(float newScore)
    {
        LoadScores();

        if (raceTimes.Count < maxRaces)
        {
            raceTimes.Add(newScore);
        }
        else
        {
            float maxScore = raceTimes.Max();
            if (newScore < maxScore)
            {
                int maxIndex = raceTimes.IndexOf(maxScore);
                raceTimes[maxIndex] = newScore;
            }
        }

        raceTimes = raceTimes.OrderBy(s => s).ToList();

        SaveScores();
        UpdateLeaderboardUI();
    }

    private void LoadScores()
    {
        raceTimes.Clear();
        for (int i = 0; i < maxRaces; i++)
        {
            string key = $"{SceneKeyPrefix}_Race_{i}";
            if (PlayerPrefs.HasKey(key))
            {
                raceTimes.Add(PlayerPrefs.GetFloat(key));
            }
        }
        raceTimes = raceTimes.OrderBy(s => s).ToList();
    }

    private void SaveScores()
    {
        for (int i = 0; i < maxRaces; i++)
        {
            string key = $"{SceneKeyPrefix}_Race_{i}";
            if (i < raceTimes.Count)
                PlayerPrefs.SetFloat(key, raceTimes[i]);
            else
                PlayerPrefs.DeleteKey(key);
        }
        PlayerPrefs.Save();
    }

    private void UpdateLeaderboardUI()
    {
        foreach (Transform child in leaderBoard)
        {
            Destroy(child.gameObject);
        }

        foreach (float score in raceTimes)
        {
            GameObject entry = Instantiate(racePrefab, leaderBoard);
            entry.GetComponentInChildren<TextMeshProUGUI>().text = score.ToString("F2");
        }
    }
    
    public List<float> GetRaceTimes()
    {
        return new List<float>(raceTimes);
    }
}
