using System;
using System.Collections;
using UnityEngine;

namespace Gallerist
{
    public class SalesController : MonoBehaviour
    {
        GameStateMachine gameStateMachine;
        PatronManager patronManager;
        ArtManager artManager;
        GameStatsController gameStatsController;

        public bool ShowResults = false;
        public string ResultsText = "";
        public string SummaryText = "";

        public event EventHandler<string> SalesResultUpdated;
        public event EventHandler<string> SalesTimeUpdated;
        public event EventHandler<ResultsArgs> SalesAttemptResultsReady;

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            patronManager = FindObjectOfType<PatronManager>();
            artManager = FindObjectOfType<ArtManager>();
            gameStatsController = FindObjectOfType<GameStatsController>();
        }

        void Start()
        {
            patronManager.SelectedObjectChanged += OnSelectedPatronChanged;
        }

        void OnSelectedPatronChanged(object sender, EventArgs e)
        {
            
        }

        public void OnSchmoozeEnd()
        {
            CheckSalesForAllPatrons();
        }

        void CheckSalesForAllPatrons()
        {
            foreach (var patron in patronManager.CurrentObjects)
            {
                CalculatePatronSatisfactionLevel(patron);
                Bag artBag = new Bag(artManager.CurrentObjects.Count);
                for (int i = 0; i < artManager.CurrentObjects.Count; i++)
                {
                    var result = Sale(patron, artManager.CurrentObjects[artBag.DrawFromBag()]);
                }

                if (Debug.isDebugBuild)
                    Debug.Log($"{patron.Name} has Satisfaction: {patron.Satisfaction} and BoredomThresh: {patron.BoredomThreshold}");
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

        void CalculateAllSatisfactionLevels()
        {
            foreach(var patron in patronManager.CurrentObjects)
            {
                CalculatePatronSatisfactionLevel(patron);
            }
        }

        void CalculatePatronSatisfactionLevel(Patron patron)
        {
            int satisfaction = 0;
            foreach (var art in artManager.CurrentObjects)
            {
                var result = patron.EvaluateArt(art);
                satisfaction += result.SatisfactionRating;
            }
            patron.Satisfaction = Mathf.CeilToInt(satisfaction / artManager.CurrentObjects.Count);
            if (Debug.isDebugBuild)
                Debug.Log($"{patron.Name} has average satisfaction of {patron.Satisfaction}");
        }

        //user clicks button to initiate closing sale
        public void ClosingEvaluation()
        {
            var currentPatron = patronManager.CurrentObject;
            var currentArt = artManager.CurrentObject;

            //if currentPatron hasn't bought an original and currentArt hasn't been sold, continue

            StartCoroutine(SalesAttempt(currentPatron, currentArt));
        }

        IEnumerator SalesAttempt(Patron currentPatron, Art currentArt)
        {
            var saleResult = Sale(currentPatron, currentArt);

            ShowResults = true;
            if (ShowResults)
                SalesAttemptResultsReady?.Invoke(this, saleResult);
            while (ShowResults)
            {
                yield return null;
            }

            gameStateMachine.Closing.Evaluations++;

            SalesTimeUpdated?.Invoke(this, $"{gameStateMachine.Closing.Evaluations} of {gameStateMachine.Closing.TotalSalesAttempts} Closing Sale Attempts");

            SalesResultUpdated?.Invoke(this, $"Originals Sold: {gameStatsController.Stats.OriginalsThisMonth} , Prints Sold: {gameStatsController.Stats.PrintsThisMonth}");

        }

        ResultsArgs Sale(Patron currentPatron, Art currentArt)
        {
            //current Patron selection evaluates current Art selection
            var result = currentPatron.EvaluateArt(currentArt);

            if (result.ResultType == EvaluationResultTypes.Subscribe)
            {
                if (!currentPatron.IsSubscriber)
                {
                    currentPatron.SetSubscription();
                    gameStatsController.Stats.SubscribersThisMonth++;
                    return new ResultsArgs($"{currentPatron.Name} is uninterested in {currentArt.Name} but has joined the gallery's mailing list.", "(+1 Mailing List Subscription)");
                }
            }

            if (result.ResultType == EvaluationResultTypes.Print)
            {
                currentPatron.SetSubscription();
                gameStatsController.Stats.SubscribersThisMonth++;
                var retval = currentPatron.BuyArt(currentArt, isOriginal: false, gameStatsController.Stats);
                return retval;
            }

            if (result.ResultType == EvaluationResultTypes.Original)
            {
                currentPatron.SetSubscription();
                gameStatsController.Stats.SubscribersThisMonth++;
                var retval = currentPatron.BuyArt(currentArt, true, gameStatsController.Stats);
                return retval;
            }

            return new ResultsArgs($"Patron {currentPatron.Name} is not interested in {currentArt.Name}.", "(No Sale)");
        }
    }
}