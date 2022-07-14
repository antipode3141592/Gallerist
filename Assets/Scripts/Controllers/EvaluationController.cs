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
        GameStatsController gameStatsController;

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
            gameStatsController = FindObjectOfType<GameStatsController>();
        }

        public void Evaluate()
        {
            var currentPatron = patronManager.SelectedObject;
            var currentArt = artManager.SelectedObject;

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
                    currentArt.IsSold = true;
                    patronManager.SubscribeToMailingList(currentPatron);
                    currentPatron.Acquisitions.Add(currentArt);
                    originalsSold++;
                    gameStatsController.Stats.OriginalsSold++;
                    break;
                case EvaluationResultTypes.Print:
                    ResultsText = $"Patron {currentPatron.Name} likes {currentArt.Name} and will buy a print!";
                    printsSold++;
                    currentArt.PrintsSold++;
                    gameStatsController.Stats.PrintsSold++;
                    break;
                case EvaluationResultTypes.Subscribe:
                    ResultsText = $"Patron {currentPatron.Name} is not particularly drawn to {currentArt.Name}, but will subscribe to the mailing list.";
                    break;
                case EvaluationResultTypes.None:
                    ResultsText = $"Patron {currentPatron.Name} is completely disinterested in {currentArt.Name} and will likely not return to the gallery.";
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

        public EvaluationResultTypes TryEvaluate(Patron patron, Art art)
        {
            return patron.EvaluateArt(art);
        }

        
    }
}