using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class StartDisplay : MonoBehaviour
    {
        GameStateMachine gameStateMachine;
        GameStatsController gameStatsController;

        [SerializeField] TextMeshProUGUI DescriptionText;
        [SerializeField] TextMeshProUGUI SummaryText;
        [SerializeField] Button continueButton;

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            gameStatsController = FindObjectOfType<GameStatsController>();
        }

        void OnEnable()
        {
            SetDescription();

            SetSummary();

            continueButton.Select();
        }

        public void CompleteStart()
        {

            gameStateMachine.StartState.IsComplete = true;
        }

        void SetDescription()
        {
            string output = "";
            switch (gameStatsController.Stats.CurrentMonth)
            {
                case 0:
                    output = $"{gameStatsController.Stats.GalleryName} is nearing its grand opening.  Time to finalize preparations for the first opening night!";
                    break;
                case 1:
                    output = $"{gameStatsController.Stats.GalleryName} is becoming more well known in the community and you expect this month's show should have a larger turnout.";
                    break;
                case 2:
                    output = $"Month 3 Text";
                    break;
                case 3:
                    output = $"Month 4 Text.  Other Stuff.";
                    break;
                case 4:
                    output = $"Month 5 Text.  Nearing the end of the year!";
                    break;
                case 5:
                    output = $"This is the last opening night of the year!  Holiday crowds are out in droves.  Good luck!";
                    break;
                default:
                    break;

            }
            DescriptionText.text = output;
        }

        void SetSummary()
        {
            SummaryText.text = $"{gameStatsController.Stats.CurrentMonth + 1} of {gameStatsController.BaseGameStats.TotalMonths}";
        }
    }
}