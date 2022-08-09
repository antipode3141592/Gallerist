using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class MainEventDisplay : Display
    {
        [SerializeField] Image mainEvent;
        [SerializeField] TextMeshProUGUI mainEventText;
        [SerializeField] Image foodAndDrink;
        [SerializeField] TextMeshProUGUI foodAndDrinkText;

        [SerializeField] TextMeshProUGUI reportText;

        [SerializeField] Button continueButton;

        GameStateMachine gameStateMachine;
        GameStatsController gameStatsController;
        PreparationController preparationController;


        protected override void Awake()
        {
            base.Awake();
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            gameStatsController = FindObjectOfType<GameStatsController>();
            preparationController = FindObjectOfType<PreparationController>();
        }

        void Start()
        {
            gameStateMachine.MainEvent.MidPartyReportTextReady += DisplayMidPartyReport;
        }

        private void DisplayMidPartyReport(object sender, string e)
        {
            reportText.text = e;
        }

        public override void Show()
        {
            base.Show();
            continueButton.Select();
            mainEventText.text = $"{preparationController.CurrentCenterpiece.Name}";
            foodAndDrinkText.text = $"{preparationController.CurrentFoodAndDrink.Name}";
        }

        public void EndMainEvent()
        {
            gameStateMachine.MainEvent.IsComplete = true;
            if (Debug.isDebugBuild)
                Debug.Log($"EndMainEvent() called");
            
        }

        
    }
}