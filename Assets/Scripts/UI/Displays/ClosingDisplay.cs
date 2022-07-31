using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class ClosingDisplay : Display
    {
        EvaluationController evaluationController;

        [SerializeField] TextMeshProUGUI evaluationsText;
        [SerializeField] TextMeshProUGUI evaluationResultsText;

        protected override void Awake()
        {
            base.Awake();
            evaluationController = FindObjectOfType<EvaluationController>();
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


    }
}