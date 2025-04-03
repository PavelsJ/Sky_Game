using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class GameData : MonoBehaviour
{
    private static GameData instance;

    public Transform leaderBoard;
    
    public int raceCount;
    public int maxRaces = 5;
    
    public GameObject racePrefab;
    private List<GameObject> races = new List<GameObject>();
    private List<float> racesTimes = new List<float>();

   
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
        for (int i = 0; i < maxRaces ; i++)
        {
            if (PlayerPrefs.HasKey("Race_" + i))
            {
                GameObject race = Instantiate(racePrefab, leaderBoard.transform);
                float score = PlayerPrefs.GetFloat("Race_" + i);
                race.GetComponentInChildren<TextMeshProUGUI>().text = score.ToString();
                races.Add(race);
            }
        }
    }


    public void AddRace(float score)
    {
        racesTimes.Clear();
        
        if (raceCount < maxRaces)
        {
            GameObject race = Instantiate(racePrefab, leaderBoard.transform);
            race.GetComponentInChildren<TextMeshProUGUI>().text = score.ToString();
            races.Add(race);
            
            PlayerPrefs.SetFloat("Race_" + races.Count + 1, score);
        }
        else
        {
            for (int i = 0; i < maxRaces; i++)
            {
                if (PlayerPrefs.HasKey("Race_" + i))
                {
                    float thisScore = PlayerPrefs.GetFloat("Race_" + i);
                    racesTimes.Add(thisScore);

                    float lowestScore = racesTimes.Max();
                    
                    if (score > lowestScore)
                    {
                        races[i].GetComponentInChildren<TextMeshProUGUI>().text = score.ToString();
                        
                        PlayerPrefs.SetFloat("Race_" + i, score);
                        PlayerPrefs.Save();
                        return;
                    }
                }
            }
        }
    }
}
