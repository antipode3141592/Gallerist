using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class END : IState
    {

        GameStatsController gameStatsController;

        public bool IsComplete = false;

        public END(GameStatsController gameStatsController)
        {
            this.gameStatsController = gameStatsController;
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

            if (stats.OriginalsThisMonth > 0)
            {
                results.Summary = $"The night was a great success!";
                results.Description = $"";
                stats.RenownThisMonth = 1;
                
            }
            else if (stats.PrintsThisMonth > 0)
            {
                results.Summary = $"The night was a moderate success.";
                results.Description = $"";
                stats.RenownThisMonth = 0;
            }
            else
            {
                results.Summary = $"The night was a dissapointment.";
                results.Description = $"{stats.GalleryName} had a dismal opening night where no guests connected with the art or artist. Hopefully next month's crowd will be more forgiving.";
                stats.RenownThisMonth = -1;
            }

            EndResultsReady?.Invoke(this, results);
        }
    }
}