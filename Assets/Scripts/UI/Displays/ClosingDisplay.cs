using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class ClosingDisplay : Display
    {
        EvaluationController evaluationController;

        ArtPiecesDisplay artPiecesDisplay;
        PatronsDisplay patronsDisplay;

        [SerializeField] TextMeshProUGUI evaluationsText;
        [SerializeField] TextMeshProUGUI evaluationResultsText;

        protected override void Awake()
        {
            base.Awake();
            evaluationController = FindObjectOfType<EvaluationController>();
            artPiecesDisplay = GetComponentInChildren<ArtPiecesDisplay>();
            patronsDisplay = GetComponentInChildren<PatronsDisplay>();

            evaluationController.EvaluationResultUpdated += OnResultsUpdated;
            evaluationController.EvaluationsTotalUpdated += OnTotalsUpdated;

            evaluationsText.text = "";
            evaluationResultsText.text = "";
        }

        void OnTotalsUpdated(object sender, string e)
        {
            evaluationsText.text = e;
        }

        void OnResultsUpdated(object sender, string e)
        {
            evaluationResultsText.text = e;
        }

        public override void Show()
        {
            base.Show();
            artPiecesDisplay.Pagination.SelectPage(0);
            patronsDisplay.Pagination.SelectPage(0);
        }
    }
}