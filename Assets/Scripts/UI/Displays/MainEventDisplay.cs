using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class MainEventDisplay : Display
    {
        [SerializeField] Image mainEvent;
        [SerializeField] string mainEventText;
        [SerializeField] Image foodAndDrink;
        [SerializeField] string foodAndDrinkText;

        [SerializeField] TextMeshProUGUI reportText;

        [SerializeField] Button continueButton;

        GameStateMachine gameStateMachine;

        protected override void Awake()
        {
            base.Awake();
            gameStateMachine = FindObjectOfType<GameStateMachine>();
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
        }

        public void EndMainEvent()
        {
            gameStateMachine.MainEvent.IsComplete = true;
            if (Debug.isDebugBuild)
                Debug.Log($"EndMainEvent() called");
            
        }

        
    }
}