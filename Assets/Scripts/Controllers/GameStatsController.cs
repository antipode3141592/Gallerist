using Gallerist;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameStatsController : MonoBehaviour
{
    GameManager gameManager;

    GameStats gameStats;
    public GameStats Stats { get { return gameStats; } }
    
    [SerializeField] int totalMonths = 6;
    public int TotalMonths => totalMonths;

    void Awake()
    {
        gameManager = FindObjectOfType<GameManager>();
        gameStats = new GameStats();
        gameStats.CurrentMonth = 1;

        gameManager.GameStateChanged += OnGameStateChange;
    }

    void OnGameStateChange(object sender, GameStates e)
    {
        if (e == GameStates.End)
        {

        }
    }
}
