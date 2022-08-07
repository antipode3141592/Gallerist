using FiniteStateMachine;
using Gallerist.Data;
using System;

namespace Gallerist.States
{
    public class END : IState
    {

        GameStatsController gameStatsController;
        ArtistManager artistManager;
        PatronManager patronManager;
        ArtManager artManager;

        public bool IsComplete = false;

        public END(GameStatsController gameStatsController, ArtistManager artistManager, PatronManager patronManager, ArtManager artManager)
        {
            this.gameStatsController = gameStatsController;
            this.artistManager = artistManager;
            this.patronManager = patronManager;
            this.artManager = artManager;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public event EventHandler<ResultsArgs> EndResultsReady;


        public void OnEnter()
        {
            IsComplete = false;
            StateEntered?.Invoke(this, EventArgs.Empty);
            SummarizeNight();
        }

        public void OnExit()
        {

        }

        public void Tick()
        {

        }

        void SummarizeNight()
        {
            var stats = gameStatsController.Stats;

            ResultsArgs results = new("","");

            if (stats.OriginalsThisMonth > 0 + gameStatsController.Stats.TotalRenown)
            {
                results.Summary = $"The night was a great success!";
                results.Description = $"Through observation, schmoozing, and a little bit of luck, you were able to sell {gameStatsController.Stats.OriginalsThisMonth} original{PluralHelpers.PluralS(gameStatsController.Stats.OriginalsThisMonth)} this month.  {artistManager.Artist.Name} is pleased and will tell their friends of their positive experience with {gameStatsController.Stats.GalleryName}! (+1 Renown)";
                stats.RenownThisMonth = 1;
                
            }
            else if (stats.PrintsThisMonth > 0)
            {
                results.Summary = $"The night was sustaining.";
                results.Description = $"Selling prints is the backbone of a gallery's operations and a sure sign that patrons are truly interested in the displayed works, but selling originals is the key to increasing {gameStatsController.Stats.GalleryName}'s renown in Randomville.  {artistManager.Artist.Name} is satisfied with the reception to their work. (+0 Renown)";
                stats.RenownThisMonth = 0;
            }
            else
            {
                results.Summary = $"The night was a dissapointment.";
                results.Description = $"{stats.GalleryName} had a dismal opening night where no guests connected with the art or artist. Hopefully next month's crowd will be more forgiving. (-1 Renown)";
                stats.RenownThisMonth = -1;
            }

            EndResultsReady?.Invoke(this, results);
        }
    }
}