using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class SchmoozeDisplay : Display
    {
        ArtistManager artistManager;
        PatronsDisplay patronsDisplay;
        
        GameStateMachine gameStateMachine;

        [SerializeField] ArtistCard artistCard;
        [SerializeField] TimeTracker timeTracker;

        [SerializeField] Button chatButton;
        [SerializeField] Button nudgeButton;
        [SerializeField] Button introductionButton;

        [SerializeField] Button continueButton;


        protected override void Awake()
        {
            base.Awake();
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            artistManager = FindObjectOfType<ArtistManager>();
            patronsDisplay = GetComponentInChildren<PatronsDisplay>();
        }

        void Start()
        {
            gameStateMachine.Schmooze.ActionTaken += SchmoozeActionTaken;
            gameStateMachine.Schmooze.ActionComplete += SchmoozeActionComplete;

            gameStateMachine.Schmooze.StateEntered += OnSchmoozingStart;

            gameStateMachine.Schmooze.EnableChat += OnEnableChat;
            gameStateMachine.Schmooze.EnableNudge += OnEnableNudge;
            gameStateMachine.Schmooze.EnableIntroduction += OnEnableIntroduction;

            gameStateMachine.Schmooze.EnableContinue += OnEnableContinue;

            chatButton.onClick.AddListener(gameStateMachine.Schmooze.Chat);
            nudgeButton.onClick.AddListener(gameStateMachine.Schmooze.Nudge);
            introductionButton.onClick.AddListener(gameStateMachine.Schmooze.Introduce);

            continueButton.onClick.AddListener(gameStateMachine.Schmooze.EndSchmoozing);
        }

        void OnEnableContinue(object sender, EventArgs e)
        {
            continueButton.interactable = true;
        }

        void SchmoozeActionComplete(object sender, string e)
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
            timeTracker.Reset(gameStateMachine.Schmooze.TotalTime);
            introductionButton.interactable = true;
            nudgeButton.interactable = true;
            chatButton.interactable = true;
            continueButton.interactable = false;
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