using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Gallerist.Data;

namespace Gallerist.UI
{
    public class StartDisplay : Display
    {
        GameStateMachine gameStateMachine;
        GameStatsController gameStatsController;

        [SerializeField] TextMeshProUGUI DescriptionText;
        [SerializeField] TextMeshProUGUI SummaryText;
        [SerializeField] Button continueButton;

        protected override void Awake()
        {
            base.Awake();
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            gameStatsController = FindObjectOfType<GameStatsController>();
        }

        public override void Show()
        {
            base.Show();
            SetDescription();
            SetSummary();
            continueButton.Select();
        }

        public void CompleteStart()
        {

            gameStateMachine.StartState.IsComplete = true;
        }

        void SetDescription()
        {
            string output = "";
            switch (gameStatsController.Stats.CurrentMonth)
            {
                case 0:
                    output = $"{gameStatsController.Stats.GalleryName} is nearing its grand opening.  As a {RenownLevelDescriptions.GetDescription(gameStatsController.Stats.TotalRenown).ToLower()} gallery in the town of Randomville, you are aiming to make a splash and draw the attention of more renowned artists and bigger crowds. Time to finalize preparations for the first opening night!";
                    break;
                case 1:
                    output = $"{gameStatsController.Stats.GalleryName} is now a {RenownLevelDescriptions.GetDescription(gameStatsController.Stats.TotalRenown).ToLower()} gallery in the community. You expect this month's show should have a larger turnout than last month's show.";
                    break;
                case 2:
                    output = $"Month 3 Text";
                    break;
                case 3:
                    output = $"Month 4 Text.  Other Stuff.";
                    break;
                case 4:
                    output = $"Month 5 Text.  Nearing the end of the year!";
                    break;
                case 5:
                    output = $"This is the last opening night of the year!  Holiday crowds are out in droves.  Good luck!";
                    break;
                default:
                    break;

            }
            DescriptionText.text = output;
        }

        void SetSummary()
        {
            SummaryText.text = $"{(MonthNames)gameStatsController.Stats.CurrentMonth} with {gameStatsController.BaseGameStats.TotalMonths - gameStatsController.Stats.CurrentMonth} months remaining in year";
        }
    }

    public enum MonthNames { Flonuary, Goblinary, Tromily, Konluth, Drokunary, Unover}
}