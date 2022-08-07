using System;

namespace Gallerist
{
    [Serializable]
    public class MonthStats
    {
        public int Month;
        public int PrintsSold;
        public int OriginalsSold;
        public int TotalSubscribers;
        public int TotalRenown;
        public int RenownGain;

        public MonthStats(int month, int printsSold, int originalsSold, int totalSubscribers, int renown, int renownGain)
        {
            Month = month;
            PrintsSold = printsSold;
            OriginalsSold = originalsSold;
            TotalSubscribers = totalSubscribers;
            TotalRenown = renown;
            RenownGain = renownGain;
        }
    }
}