using System;
using System.Collections;
using UnityEngine;

namespace Gallerist
{
    public class EvaluationController : MonoBehaviour
    {
        GameStateMachine gameStateMachine;
        PatronManager patronManager;
        ArtManager artManager;
        GameStatsController gameStatsController;

        public bool ShowResults = false;
        public string ResultsText = "";
        public string SummaryText = "";

        public event EventHandler<string> EvaluationResultUpdated;
        public event EventHandler<string> EvaluationsTotalUpdated;
        public event EventHandler<ResultsArgs> EvaluationResultsReady;

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            patronManager = FindObjectOfType<PatronManager>();
            artManager = FindObjectOfType<ArtManager>();
            gameStatsController = FindObjectOfType<GameStatsController>();
        }

        public void OnSchmoozeEnd()
        {
            EvaluateAll();
        }

        void EvaluateAll()
        {
            foreach (var patron in patronManager.CurrentObjects)
            {
                foreach (var art in artManager.CurrentObjects)
                {
                    Evaluate(patron, art);   
                }
                //after evaluating all art, check patron satisfaction
                // if bored, mark for exit
                if (patron.Satisfaction < patron.BoredomThreshold)
                {
                    if (Debug.isDebugBuild)
                        Debug.Log($"{patron.Name} is bored and will leave.");
                    patronManager.ExitingPatrons.Add(patron);
                    gameStatsController.Stats.BoredGuests++;
                }
            }
        }

        public void ClosingEvaluation()
        {
            var currentPatron = patronManager.CurrentObject;
            var currentArt = artManager.CurrentObject;

            //if currentPatron hasn't bought an original and currentArt hasn't been sold, continue

            StartCoroutine(Evaluation(currentPatron, currentArt));
        }

        IEnumerator Evaluation(Patron currentPatron, Art currentArt)
        {
            Evaluate(currentPatron, currentArt);

            ShowResults = true;
            if (ShowResults)
                EvaluationResultsReady?.Invoke(this, new ResultsArgs(
                    description: $"{ResultsText}",
                    summary: $""));
            while (ShowResults)
            {
                yield return null;
            }

            gameStateMachine.Closing.Evaluations++;
            EvaluationsTotalUpdated?.Invoke(this, $"{gameStateMachine.Closing.Evaluations} of {gameStateMachine.Closing.TotalEvaluations} Evaluations");
            EvaluationResultUpdated?.Invoke(this, $"Originals Sold: {gameStatsController.Stats.OriginalsThisMonth} , Prints Sold: {gameStatsController.Stats.PrintsThisMonth}");
            
        }

        void Evaluate(Patron currentPatron, Art currentArt)
        {
            //current Patron selection evaluates current Art selection
            var result = currentPatron.EvaluateArt(currentArt);

            if (result == EvaluationResultTypes.None)
            {
                ResultsText = $"Patron {currentPatron.Name} finds {currentArt.Name} boring.";
                SummaryText = $"";
                return;
            }

            if (result == EvaluationResultTypes.Subscribe ||
                result == EvaluationResultTypes.Print ||
                result == EvaluationResultTypes.Original)
            {
                if (!currentPatron.IsSubscriber)
                {
                    currentPatron.Satisfaction += 5;
                    ResultsText = $"{currentPatron.Name} is uninterested in {currentArt.Name} but has joined the gallery's mailing list.";
                    SummaryText = $"+1 Subscription";
                    currentPatron.SetSubscription();
                    gameStatsController.Stats.SubscribersThisMonth++;
                }
            }

                if (result == EvaluationResultTypes.Print)
            {
                currentPatron.Satisfaction += 5;
                ResultsText = $"Patron {currentPatron.Name} likes {currentArt.Name} and will buy a print!";
                SummaryText = $"+1 Print Sale";
                currentArt.PrintsSold++;
                gameStatsController.Stats.PrintsThisMonth++;
                return;
            }

            if (result == EvaluationResultTypes.Original)
            {
                currentPatron.Satisfaction += 10;
                ResultsText = $"Patron {currentPatron.Name} loves {currentArt.Name} and will buy the original!";
                SummaryText = $"+1 Original Sale";
                currentArt.IsSold = true;
                currentPatron.Acquisitions.Add(currentArt);
                gameStatsController.Stats.OriginalsThisMonth++;
            }
        }
    }

    public class ResultsArgs : EventArgs
    {
        public string Description;
        public string Summary;

        public ResultsArgs(string description, string summary)
        {
            Description = description;
            Summary = summary;
        }
    }
}