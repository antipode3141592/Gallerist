using System;

namespace Gallerist
{
    [Serializable]
    public class BaseGameStats
    {
        public int TotalMonths;
        public int StartingRenown;

        public BaseGameStats(int totalMonths, int startingRenown)
        {
            TotalMonths = totalMonths;
            StartingRenown = startingRenown;
        }
    }
}