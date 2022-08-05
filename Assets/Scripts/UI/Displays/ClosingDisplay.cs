using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class ClosingDisplay : Display
    {
        SalesController salesController;

        ArtPiecesDisplay artPiecesDisplay;
        PatronsDisplay patronsDisplay;

        [SerializeField] TextMeshProUGUI evaluationsText;
        [SerializeField] TextMeshProUGUI evaluationResultsText;

        protected override void Awake()
        {
            base.Awake();
            salesController = FindObjectOfType<SalesController>();
            artPiecesDisplay = GetComponentInChildren<ArtPiecesDisplay>();
            patronsDisplay = GetComponentInChildren<PatronsDisplay>();

            salesController.SalesResultUpdated += OnResultsUpdated;
            salesController.SalesTimeUpdated += OnTotalsUpdated;

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