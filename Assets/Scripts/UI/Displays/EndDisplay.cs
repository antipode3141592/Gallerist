using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class EndDisplay : Display
    {
        GameStateMachine gameStateMachine;
        GameStatsController gameStatsController;

        [SerializeField] TextMeshProUGUI SummaryResultText;
        [SerializeField] TextMeshProUGUI NightDescriptionText;
        [SerializeField] TextMeshProUGUI OverallEnjoymentText;
        [SerializeField] TextMeshProUGUI OriginalsSoldText;
        [SerializeField] TextMeshProUGUI PrintsSoldText;
        [SerializeField] TextMeshProUGUI NewSubscribers;

        [SerializeField] Button continueButton;

        protected override void Awake()
        {
            base.Awake();
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            gameStatsController = FindObjectOfType<GameStatsController>();
        }

        void Start()
        {
            gameStateMachine.End.EndResultsReady += DisplayEndResults;
        }

        private void DisplayEndResults(object sender, ResultsArgs e)
        {
            SummaryResultText.text = e.Summary;
            NightDescriptionText.text = e.Description;
            SummarizeNight();
        }

        public override void Show()
        {
            base.Show();
            continueButton.Select();
        }

        void SummarizeNight()
        {
            OriginalsSoldText.text = $"Originals Sold: {gameStatsController.Stats.OriginalsThisMonth}";
            PrintsSoldText.text = $"Prints Sold: {gameStatsController.Stats.PrintsThisMonth}";
            NewSubscribers.text = $"New Subscribers: {gameStatsController.Stats.SubscribersThisMonth}";
        }

        public void CompleteEnd()
        {
            gameStatsController.Stats.SaveMonth();
            gameStateMachine.End.IsComplete = true;
        }
    }
}