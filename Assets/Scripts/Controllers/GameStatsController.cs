using Gallerist;
using UnityEngine;

public class GameStatsController : MonoBehaviour
{
    GameStats gameStats;
    public GameStats Stats => gameStats;

    void Awake()
    {
        gameStats = new GameStats();
        gameStats.CurrentMonth = 0;
    }
}
