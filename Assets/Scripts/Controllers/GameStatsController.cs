using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsController : MonoBehaviour
{
    GameStats gameStats;
    public GameStats Stats { get { return gameStats; } }
    
    [SerializeField] int totalMonths = 6;
    public int TotalMonths => totalMonths;

    void Awake()
    {
        gameStats = new GameStats();
        gameStats.CurrentMonth = 1;
    }
}
