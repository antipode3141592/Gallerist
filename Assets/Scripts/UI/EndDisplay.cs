using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class EndDisplay : MonoBehaviour
    {
        GameStateMachine gameStateMachine;
        GameStatsController gameStatsController;

        [SerializeField] TextMeshProUGUI SummaryResultText;
        [SerializeField] TextMeshProUGUI NightDescriptionText;
        [SerializeField] TextMeshProUGUI OverallEnjoymentText;
        [SerializeField] TextMeshProUGUI OriginalsSoldText;
        [SerializeField] TextMeshProUGUI PrintsSoldText;
        [SerializeField] TextMeshProUGUI NewSubscribers;

        [SerializeField] Button continueButton;

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            gameStatsController = FindObjectOfType<GameStatsController>();
        }

        void OnEnable()
        {
            continueButton.Select();
            SummarizeNight();
        }

        public void SummarizeNight()
        {
            int originalsSold = gameStatsController.Stats.OriginalsThisMonth;
            int printsSold = gameStatsController.Stats.PrintsThisMonth;
            int subscribers = gameStatsController.Stats.SubscribersThisMonth;

            if (originalsSold == gameStateMachine.Closing.TotalEvaluations)
            {
                SummaryResultText.text = $"The night was a roaring success!";
            }
            else if (originalsSold > 0)
            {
                SummaryResultText.text = $"The night was a great success!";
            }
            else if (originalsSold == 0 && printsSold > 0)
            {
                SummaryResultText.text = $"The night was a moderate success.";
            }
            else
            {
                SummaryResultText.text = $"The night was a dissapointment.";
            }

            OriginalsSoldText.text = $"Originals Sold: {originalsSold}";
            PrintsSoldText.text = $"Prints Sold: {printsSold}";
            NewSubscribers.text = $"New Subscribers: {subscribers}";
        }

        public void CompleteEnd()
        {
            gameStatsController.Stats.SaveMonth();
            gameStateMachine.End.IsComplete = true;
        }
    }
}