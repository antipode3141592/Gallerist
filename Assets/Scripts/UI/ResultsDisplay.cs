using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class ResultsDisplay : MonoBehaviour
    {
        EvaluationController evaluationController;

        Vector3 originalPosition;

        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] TextMeshProUGUI resultsText;

        [SerializeField] Button continueButton;

        void Awake()
        {
            evaluationController = FindObjectOfType<EvaluationController>();
            originalPosition = transform.position;

            evaluationController.EvaluationResultsReady += DisplayResults;
            MoveOffscreen();
        }

        private void DisplayResults(object sender, ResultsArgs e)
        {
            MoveOnScreen();
            descriptionText.text = e.Description;
            resultsText.text = e.Summary;
        }

        public void Continue()
        {
            MoveOffscreen();
            evaluationController.ShowResults = false;
        }

        void MoveOffscreen()
        {
            transform.position = originalPosition + new Vector3(0f, 2000f);
        }

        void MoveOnScreen()
        {
            transform.position = originalPosition;
            continueButton.Select();
        }
    }
}