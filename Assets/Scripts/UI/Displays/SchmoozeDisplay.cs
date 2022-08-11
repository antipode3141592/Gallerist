using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class SchmoozeDisplay : Display
    {
        ArtistManager artistManager;
        SchmoozeController schmoozeController;
        PatronsDisplay patronsDisplay;
        
        GameStateMachine gameStateMachine;

        [SerializeField] ArtistCard artistCard;
        [SerializeField] TimeTracker timeTracker;

        [SerializeField] Button chatButton;
        [SerializeField] Button nudgeButton;
        [SerializeField] Button introductionButton;


        protected override void Awake()
        {
            base.Awake();
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            artistManager = FindObjectOfType<ArtistManager>();
            schmoozeController = FindObjectOfType<SchmoozeController>();
            patronsDisplay = GetComponentInChildren<PatronsDisplay>();
        }

        void Start()
        {
            schmoozeController.ActionTaken += SchmoozeActionTaken;
            schmoozeController.ActionComplete += SchmoozeActionComplete;

            gameStateMachine.Schmooze.StateEntered += OnSchmoozingStart;

            schmoozeController.EnableChat += OnEnableChat;
            schmoozeController.EnableNudge += OnEnableNudge;
            schmoozeController.EnableIntroduction += OnEnableIntroduction;

            chatButton.onClick.AddListener(schmoozeController.Chat);
            nudgeButton.onClick.AddListener(schmoozeController.Nudge);
            introductionButton.onClick.AddListener(schmoozeController.Introduce);
        }

        void SchmoozeActionComplete(object sender, EventArgs e)
        {
            if (chatButton.IsInteractable())
                chatButton.Select();
            else if (nudgeButton.IsInteractable())
                nudgeButton.Select();
            else if (introductionButton.IsInteractable())
                introductionButton.Select();
        }

        public override void Show()
        {
            base.Show();
            timeTracker.Reset(schmoozeController.TotalSchmoozingTime);
            introductionButton.interactable = true;
            nudgeButton.interactable = true;
            chatButton.interactable = true;
            patronsDisplay.Pagination.SelectPage(0);
            chatButton.Select();
        }

        void OnEnableIntroduction(object sender, bool e)
        {
            introductionButton.interactable = e;
        }

        void OnEnableNudge(object sender, bool e)
        {
            nudgeButton.interactable = e;
        }

        void OnEnableChat(object sender, bool e)
        {
            chatButton.interactable = e;
        }

        void SchmoozeActionTaken(object sender, EventArgs e)
        {
            timeTracker.UpdateForeground(gameStateMachine.Schmooze.ElapsedTime);
        }

        void OnSchmoozingStart(object sender, EventArgs e)
        {
            timeTracker.Reset(gameStateMachine.Schmooze.TotalTime);
        }

        public void UpdateArtistCard()
        {
            artistCard.LoadArtistCardData(artistManager.Artist);
        }

        public void ShowDialogue()
        {

        }

        public void HideDialogue()
        {

        }
    }
}