using FiniteStateMachine;
using Gallerist.Data;
using System;

namespace Gallerist.States
{
    public class MainEvent : IState
    {
        PatronManager patronManager;
        GameStatsController gameStatsController;

        public bool IsComplete;

        public MainEvent(PatronManager patronManager, GameStatsController gameStatsController)
        {
            this.patronManager = patronManager;
            this.gameStatsController = gameStatsController;

        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public event EventHandler<string> MidPartyReportTextReady;

        public void OnEnter()
        {
            patronManager.OnMainEventEntered();
            GenerateMidPartyReport();
            IsComplete = false;
            StateEntered?.Invoke(this, EventArgs.Empty);
            
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }

        void GenerateMidPartyReport()
        {
            GameStats gameStats = gameStatsController.Stats;
            string reportText = $"{gameStats.MidPartyExits} guest{PluralHelpers.PluralS(gameStats.MidPartyExits)} left before the main event.\n";

            //reportText += $"{gameStats.BoredGuests} guest{PluralS(gameStats.BoredGuests)} {WasWere(gameStats.BoredGuests)} disinterested.\n";

            if (gameStats.PrintsThisMonth > 0)
                reportText += $"{gameStats.PrintsThisMonth} print{PluralHelpers.PluralS(gameStats.PrintsThisMonth)} {PluralHelpers.WasWere(gameStats.PrintsThisMonth)} purchased.\n";

            if (gameStats.OriginalsThisMonth > 1)
                reportText += $"{gameStats.OriginalsThisMonth} original{PluralHelpers.PluralS(gameStats.OriginalsThisMonth)} {PluralHelpers.HasHave(gameStats.OriginalsThisMonth)} been sold!\n";

            reportText += $"{gameStats.SubscribersThisMonth} patrons have joined {gameStats.GalleryName}'s mailing list!\n";

            reportText += $"{gameStats.MidPartyEntrances} new guests have joined the party!";

            MidPartyReportTextReady?.Invoke(this, reportText);
        }

        
    }
}