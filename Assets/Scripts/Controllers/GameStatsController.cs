using Gallerist;
using UnityEngine;

public class GameStatsController : MonoBehaviour
{
    GameStats gameStats;
    BaseGameStats baseGameStats;
    public GameStats Stats => gameStats;
    public BaseGameStats BaseGameStats => baseGameStats;

    void Awake()
    {
        gameStats = new GameStats();
        baseGameStats = new BaseGameStats(totalMonths: 6, startingRenown: 1);
        gameStats.CurrentMonth = 0;
    }
}
