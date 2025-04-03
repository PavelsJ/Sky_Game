using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public delegate void RaceEvent();
    public static event RaceEvent OnStartRace;
    public static event RaceEvent OnPenalty;

    public static event RaceEvent OnBoost;
    public static event RaceEvent OnEndRace;

    public static void CallStartRace()
    {
        if (OnStartRace != null)
        {
            OnStartRace();
        }
    }

    public static void CallPenalty()
    {
        if (OnPenalty != null)
        {
            OnPenalty();
        }
    }

    public static void CallBoost()
    {
        if (OnBoost != null)
        {
            OnBoost();
        }
    }

    public static void CallEndRace()
    {
        if (OnEndRace != null)
        {
            OnEndRace();
        }
    }
}
