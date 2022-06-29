using System;
using UnityEngine;

namespace Gallerist
{
    public class EvaluationController : MonoBehaviour
    {
        GameManager gameManager;
        PatronManager patronManager;
        ArtManager artManager;

        

        [SerializeField] int maximumEvaluations = 5;

        int totalEvaluations = 0;
        int originalsSold = 0;
        int printsSold = 0;

        public int MaximumEvaluations => maximumEvaluations;
        public int OriginalsSold => originalsSold;
        public int PrintsSold => printsSold;

        public event EventHandler<string> EvaluationResultUpdated;
        public event EventHandler<string> EvaluationsTotalUpdated;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            patronManager = FindObjectOfType<PatronManager>();
            artManager = FindObjectOfType<ArtManager>();
        }

        public void Evaluate()
        {
            totalEvaluations++;
            //current Patron selection evaluates current Art selection
            var result = patronManager.SelectedPatron.EvaluateArt(artManager.SelectedArt);
            switch (result)
            {
                case EvaluationResultTypes.Original:
                    Debug.Log($"Patron {patronManager.SelectedPatron.Name} loves {artManager.SelectedArt.Name} and will buy the original!");
                    originalsSold++;
                    break;
                case EvaluationResultTypes.Print:
                    Debug.Log($"Patron {patronManager.SelectedPatron.Name} likes {artManager.SelectedArt.Name} and will buy a print!");
                    printsSold++;
                    break;
                case EvaluationResultTypes.None:
                    Debug.Log($"Patron {patronManager.SelectedPatron.Name} is not particularly drawn to {artManager.SelectedArt.Name}, but will take a business card.");
                    break;
            }
            EvaluationsTotalUpdated?.Invoke(this, $"{totalEvaluations} of {maximumEvaluations} Evaluations");
            EvaluationResultUpdated?.Invoke(this, $"Originals Sold: {originalsSold} , Prints Sold: {printsSold}");
            if (totalEvaluations >= maximumEvaluations)
                gameManager.CompleteEvaluation();
        }
    }
}