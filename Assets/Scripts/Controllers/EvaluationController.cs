using System;
using System.Collections;
using UnityEngine;

namespace Gallerist
{
    public class EvaluationController : MonoBehaviour
    {
        GameManager gameManager;
        PatronManager patronManager;
        ArtManager artManager;

        public bool ShowResults = false;
        public string ResultsText = "";

        [SerializeField] int maximumEvaluations = 5;

        int totalEvaluations = 0;
        int originalsSold = 0;
        int printsSold = 0;

        public int MaximumEvaluations => maximumEvaluations;
        public int OriginalsSold => originalsSold;
        public int PrintsSold => printsSold;

        public event EventHandler<string> EvaluationResultUpdated;
        public event EventHandler<string> EvaluationsTotalUpdated;
        public event EventHandler EvaluationResultsReady;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            patronManager = FindObjectOfType<PatronManager>();
            artManager = FindObjectOfType<ArtManager>();
        }

        public void Evaluate()
        {
            var currentPatron = patronManager.SelectedPatron;
            var currentArt = artManager.SelectedArt;

            //if currentPatron hasn't bought an original and currentArt hasn't been sold, continue

            StartCoroutine(Evaluation(currentPatron, currentArt));
        }

        IEnumerator Evaluation(Patron currentPatron, Art currentArt)
        {
            totalEvaluations++;
            //current Patron selection evaluates current Art selection
            var result = currentPatron.EvaluateArt(currentArt);
            switch (result)
            {
                case EvaluationResultTypes.Original:
                    ResultsText = $"Patron {currentPatron.Name} loves {currentArt.Name} and will buy the original!";
                    originalsSold++;
                    break;
                case EvaluationResultTypes.Print:
                    ResultsText = $"Patron {currentPatron.Name} likes {currentArt.Name} and will buy a print!";
                    printsSold++;
                    break;
                case EvaluationResultTypes.None:
                    ResultsText = $"Patron {currentPatron.Name} is not particularly drawn to {currentArt.Name}, but will take a business card.";
                    break;
                default:
                    ResultsText = "FLAGRANT ERROR";
                    break;
            }
            Debug.Log(ResultsText);
            EvaluationResultsReady?.Invoke(this, EventArgs.Empty);
            ShowResults = true;

            while (ShowResults)
            {
                yield return null;
            }

            EvaluationsTotalUpdated?.Invoke(this, $"{totalEvaluations} of {maximumEvaluations} Evaluations");
            EvaluationResultUpdated?.Invoke(this, $"Originals Sold: {originalsSold} , Prints Sold: {printsSold}");
            if (totalEvaluations >= maximumEvaluations)
                gameManager.CompleteEvaluation();
        }
    }
}