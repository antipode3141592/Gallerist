using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gallerist.UI
{
    public class MainEventDisplay : Display
    {
        [SerializeField] Image mainEvent;
        [SerializeField] Image foodAndDrink;

        [SerializeField] TextMeshProUGUI exitedText;
        [SerializeField] TextMeshProUGUI boredText;
        [SerializeField] TextMeshProUGUI subscribersText;
        [SerializeField] TextMeshProUGUI printText;
        [SerializeField] TextMeshProUGUI originalText;
        [SerializeField] TextMeshProUGUI newPatronsText;

        [SerializeField] Button continueButton;

        GameStateMachine gameStateMachine;
        GameStatsController gameStatsController;

        protected override void Awake()
        {
            base.Awake();
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            gameStatsController = FindObjectOfType<GameStatsController>();
        }

        public override void Show()
        {
            base.Show();
            continueButton.Select();
            DisplayMidPartyStats();
        }

        void DisplayMidPartyStats()
        {
            exitedText.text = $"{gameStatsController.Stats.MidPartyExits} guests left before the main event";
            boredText.text = $"{gameStatsController.Stats.BoredGuests} guests left due to boredom";
            if (gameStatsController.Stats.PrintsThisMonth > 0)
                printText.text = $"{gameStatsController.Stats.PrintsThisMonth} guests purchased prints and left";
            else
                printText.text = "";
            if (gameStatsController.Stats.OriginalsThisMonth > 1)
                originalText.text = $"{gameStatsController.Stats.OriginalsThisMonth} guests fell in love with the art and purchased the originals before leaving";
            else if (gameStatsController.Stats.OriginalsThisMonth == 1)
                originalText.text = $"One guest fell in love with an original and purchased it before leaving";
            else
                originalText.text = "";

            subscribersText.text = $"{gameStatsController.Stats.SubscribersThisMonth} guests have joined the gallery's mailing list!";

            newPatronsText.text = $"{gameStatsController.Stats.MidPartyEntrances} new guests have joined!";

        }

        public void EndMainEvent()
        {
            gameStateMachine.MainEvent.IsComplete = true;
            if (Debug.isDebugBuild)
                Debug.Log($"EndMainEvent() called");
            
        }
    }
}