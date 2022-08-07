using Gallerist.Data;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class FinalDisplay : Display
    {
        GameStatsController gameStatsController;

        [SerializeField] TextMeshProUGUI SummaryText;
        [SerializeField] TextMeshProUGUI DescriptionText;
        [SerializeField] TextMeshProUGUI StatsText;

        protected override void Awake()
        {
            base.Awake();
            gameStatsController = FindObjectOfType<GameStatsController>();
        }

        public override void Show()
        {
            base.Show();
            GenerateSummary();
            GenerateDescription();
            GenerateStatsBlock();
        }

        void GenerateSummary()
        {
            SummaryText.text = Summaries[gameStatsController.Stats.TotalRenown];
        }

        void GenerateDescription()
        {

        }

        void GenerateStatsBlock()
        {
            var stats = gameStatsController.Stats;
            StatsText.text = $"Renown: {RenownLevelDescriptions.GetDescription(stats.TotalRenown)} ({stats.TotalRenown})\n" + 
                $"Originals Sold: {stats.OriginalsSold}\n" + 
                $"Prints Sold: {stats.PrintsSold}\n" +
                $"Subscribers: {stats.TotalSubscribers}";
        }

        static Dictionary<int, string> Summaries = new()
        {
            { -5, $"Terrible, no good year." },
            { -4, $"Rotten year." },
            { -3, $"Dismal year." },
            { -2, $"Bad year." },
            { -1, $"Somewhat disappointing year."},
            { 0, $"Year without growth."},
            { 1, $"Moderate year." },
            { 2, $"Good year."},
            { 3, $"Great end to the year!"},
            { 4, $"Fantastic year!"},
            { 5, $"Perfect year!"}
        };
    }
}