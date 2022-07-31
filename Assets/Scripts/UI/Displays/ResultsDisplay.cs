using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class ResultsDisplay : Display
    {
        EvaluationController evaluationController;

        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] TextMeshProUGUI resultsText;

        [SerializeField] Button continueButton;

        protected override void Awake()
        {
            base.Awake();
            evaluationController = FindObjectOfType<EvaluationController>();
            evaluationController.EvaluationResultsReady += DisplayResults;
        }

        private void DisplayResults(object sender, ResultsArgs e)
        {
            Show();
            descriptionText.text = e.Description;
            resultsText.text = e.Summary;
        }

        public void Continue()
        {
            Hide();
            evaluationController.ShowResults = false;
        }

        public override void Show()
        {
            base.Show();
            continueButton.Select();
        }
    }
}