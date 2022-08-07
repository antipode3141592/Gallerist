using System;
using System.Collections.Generic;

namespace Gallerist
{
    [Serializable]
    public class GameStats
    {
        public GameStats()
        {
            
        }

        public string GalleristName = "";
        public string GalleryName = "";

        public int MidPartyExits = 0;
        public int MidPartyEntrances = 0;
        public int BoredGuests = 0;
        public int PrintsThisMonth = 0;
        public int OriginalsThisMonth = 0;
        public int SubscribersThisMonth = 0;
        public int RenownThisMonth = 0;

        public int TotalRenown = 0;
        public int PrintsSold = 0;
        public int OriginalsSold = 0;
        public int TotalSubscribers = 0;



        public int CurrentMonth = 0;

        public readonly List<MonthStats> MonthStats = new();



        public void SaveMonth()
        {
            PrintsSold += PrintsThisMonth;
            OriginalsSold += OriginalsThisMonth;
            TotalSubscribers += SubscribersThisMonth;
            TotalRenown += RenownThisMonth;

            MonthStats.Add(new MonthStats(CurrentMonth, PrintsThisMonth, OriginalsThisMonth, SubscribersThisMonth, TotalRenown, RenownThisMonth));

            CurrentMonth++;
            ResetMonth();
        }

        void ResetMonth()
        {
            PrintsThisMonth = 0;
            OriginalsThisMonth = 0;
            SubscribersThisMonth = 0;
            RenownThisMonth = 0;

            MidPartyExits = 0;
            BoredGuests = 0;
        }
    }
}