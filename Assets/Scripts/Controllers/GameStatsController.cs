using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsController : MonoBehaviour
{
    GameStats gameStats;
    public GameStats Stats { get { return gameStats; } }

    void Awake()
    {
        gameStats = new GameStats();
        gameStats.CurrentMonth = 1;
    }
}
