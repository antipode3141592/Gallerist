using FiniteStateMachine;
using Gallerist.Data;
using System;

namespace Gallerist.States
{
    public class Final : IState
    {
        GameStatsController _gameStatsController;

        public bool IsComplete = false;

        public Final(GameStatsController gameStatsController)
        {
            _gameStatsController = gameStatsController;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public event EventHandler<string> FinalReportReady;

        public void OnEnter()
        {
            GenerateFinalReport();
            IsComplete = false;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
        }

        public void Tick()
        {
        }

        void GenerateFinalReport()
        {
            string report = FinalReportDescriptions.ReportsByRenown[_gameStatsController.Stats.TotalRenown];
            report = report.Replace($"[TownName]", "Randomville");
            report = report.Replace($"[GalleryName]", _gameStatsController.Stats.GalleryName);
            FinalReportReady?.Invoke(this, report);
        }
    }
}