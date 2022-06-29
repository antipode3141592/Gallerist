using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class ClosingDisplay : MonoBehaviour
    {
        EvaluationController evaluationController;

        [SerializeField] TextMeshProUGUI evaluationsText;
        [SerializeField] TextMeshProUGUI evaluationResultsText;

        void Awake()
        {
            evaluationController = FindObjectOfType<EvaluationController>();
            evaluationController.EvaluationResultUpdated += OnResultsUpdated;
            evaluationController.EvaluationsTotalUpdated += OnTotalsUpdated;
        }

        private void OnTotalsUpdated(object sender, string e)
        {
            evaluationsText.text = e;
        }

        private void OnResultsUpdated(object sender, string e)
        {
            evaluationResultsText.text = e;
        }
    }
}