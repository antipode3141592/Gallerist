using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class ResultsDisplay : Display
    {
        SalesController salesController;
        SchmoozeController schmoozeController;

        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] TextMeshProUGUI resultsText;

        [SerializeField] Button continueButton;

        protected override void Awake()
        {
            base.Awake();
            salesController = FindObjectOfType<SalesController>();
            schmoozeController = FindObjectOfType<SchmoozeController>();   
        }

        void Start()
        {
            salesController.SalesAttemptResultsReady += DisplayResults;
            schmoozeController.ResultsReady += DisplayResults;
        }

        void DisplayResults(object sender, ResultsArgs e)
        {
            Show();
            descriptionText.text = e.Description;
            resultsText.text = e.Summary;
        }

        public void Continue()
        {
            Hide();
            salesController.ShowResults = false;
            schmoozeController.ShowResults = false;
        }

        public override void Show()
        {
            base.Show();
            continueButton.Select();
        }
    }
}