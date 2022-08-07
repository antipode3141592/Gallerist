using Gallerist.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

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

        void Start()
        {
            gameStateMachine.StartState.StartReportReady += OnStartReportReady;
        }

        void OnStartReportReady(object sender, string e)
        {
            DescriptionText.text = e;
        }

        public override void Show()
        {
            base.Show();
            SetSummary();
            continueButton.Select();
        }

        public void CompleteStart()
        {

            gameStateMachine.StartState.IsComplete = true;
        }

        void SetSummary()
        {
            SummaryText.text = $"{(MonthNames)gameStatsController.Stats.CurrentMonth} with {gameStatsController.BaseGameStats.TotalMonths - gameStatsController.Stats.CurrentMonth} months remaining in year\n" +
                $"Sell {gameStatsController.Stats.TotalRenown + 1} original{PluralHelpers.PluralS(gameStatsController.Stats.TotalRenown + 1)} to increase Renown";
        }
    }
}