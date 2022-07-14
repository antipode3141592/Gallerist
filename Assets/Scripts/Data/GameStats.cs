using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class GameStats
    {
        public GameStats()
        {
        }
        public int PrintsSold { get; set; }
        public int OriginalsSold { get; set; }
        public int TotalSubscribers { get; set; }

        public int TotalCustomerSatisfaction => TotalSubscribers + PrintsSold * 5 + OriginalsSold * 10;

        public int CurrentMonth { get; set; }

        public List<MonthStats> MonthStats { get; } = new();
    }

    public class MonthStats
    {
        public int Month;
        public int PrintsSold;
        public int OriginalsSold;
        public int TotalSubscribers;
    }
}