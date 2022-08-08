using Gallerist.Data;
using Gallerist.States;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class FinalDisplay : Display
    {
        GameStatsController gameStatsController;
        GameStateMachine gameStateMachine;

        [SerializeField] TextMeshProUGUI SummaryText;
        [SerializeField] TextMeshProUGUI DescriptionText;
        [SerializeField] TextMeshProUGUI StatsText;

        protected override void Awake()
        {
            base.Awake();
            gameStatsController = FindObjectOfType<GameStatsController>();
            gameStateMachine = FindObjectOfType<GameStateMachine>();
        }

        void Start()
        {
            gameStateMachine.Final.FinalReportReady += OnFinalReportReady;
        }

        void OnFinalReportReady(object sender, string e)
        {
            GenerateDescription(e);
        }

        public override void Show()
        {
            base.Show();
            GenerateSummary();
            GenerateStatsBlock();
        }

        void GenerateSummary()
        {
            SummaryText.text = Summaries[gameStatsController.Stats.TotalRenown];
        }

        void GenerateDescription(string report)
        {
            DescriptionText.text = report;
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
            { -5, $"Terrible, No Good Year." },
            { -4, $"Rotten Year" },
            { -3, $"Dismal Year" },
            { -2, $"Bad Year" },
            { -1, $"Disappointing Year"},
            { 0, $"Year Without Growth"},
            { 1, $"Moderate Year" },
            { 2, $"Year of Growth"},
            { 3, $"Great Year!"},
            { 4, $"Fantastic Year!"},
            { 5, $"Perfect Year!"}
        };
    }
}