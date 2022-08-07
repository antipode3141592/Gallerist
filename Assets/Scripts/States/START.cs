using FiniteStateMachine;
using Gallerist.Data;
using System;

namespace Gallerist.States
{
    public class START : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public bool IsComplete = false;

        ArtManager _artManager;
        ArtistManager _artistManager;
        PatronManager _patronManager;

        GameStatsController _gameStatsController;

        public event EventHandler<string> StartReportReady;

        public START(ArtManager artManager, ArtistManager artistManager, PatronManager patronManager, GameStatsController gameStatsController)
        {
            _artManager = artManager;
            _artManager.ObjectsGenerated += OnArtPiecesGenerated;
            _artistManager = artistManager;
            _artistManager.ArtistGenerated += OnArtistGenerated;
            _patronManager = patronManager;
            _patronManager.ObjectsGenerated += OnPatronsGenerated;
            _gameStatsController = gameStatsController;
        }

        public void OnEnter()
        {
            StateEntered?.Invoke(this, EventArgs.Empty);
            IsComplete = false;

            _artistManager.NewArtist();
        }

        void OnArtistGenerated(object sender, EventArgs e)
        {
            _artManager.GenerateArtpieces(10 + _artistManager.Artist.Experience * 2);
        }

        void OnArtPiecesGenerated(object sender, EventArgs e)
        {
            _patronManager.NewPatrons(20 + 5 * _gameStatsController.Stats.TotalRenown);
        }

        void OnPatronsGenerated(object sender, EventArgs e)
        {
            GenerateMonthDescription();
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

        void GenerateMonthDescription()
        {
            int renownChange = CheckRenownChange();
            string monthDescription = MonthStartingReportDescriptions.MonthStartingReports[_gameStatsController.Stats.CurrentMonth][renownChange];
            monthDescription = monthDescription.Replace("[GalleryName]", _gameStatsController.Stats.GalleryName);
            monthDescription = monthDescription.Replace("[RenownDescription]", RenownLevelDescriptions.GetDescription(_gameStatsController.Stats.TotalRenown).ToLower());
            monthDescription = monthDescription.Replace("[SubscriberCount]", $"{_gameStatsController.Stats.TotalSubscribers}");
            monthDescription = monthDescription.Replace("[TownName]", $"Randomville");
            monthDescription = monthDescription.Replace("[ArtistExperience]", $"{ArtistExperienceLevelDescription.GetDesciption(_artistManager.Artist.Experience).ToLower()}");
            monthDescription = monthDescription.Replace("[OriginalsSold]", $"{_gameStatsController.Stats.OriginalsSold}");
            monthDescription = monthDescription.Replace("[PrintsSold]", $"{_gameStatsController.Stats.PrintsSold}");
            StartReportReady?.Invoke(this, monthDescription);
        }

        int CheckRenownChange()
        {
            if (_gameStatsController.Stats.CurrentMonth == 0)
                return 0;
            return _gameStatsController.Stats.MonthStats.Find(x => x.Month == _gameStatsController.Stats.CurrentMonth-1).RenownGain;
        }

        
    }
}