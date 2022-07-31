using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class SchmoozeDisplay : Display
    {
        ArtistManager artistManager;
        SchmoozeController schmoozeController;
        
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
            schmoozeController.ActionTaken += SchmoozeActionTaken;
            gameStateMachine.Schmooze.StateEntered += OnSchmoozingStart;
            schmoozeController.EnableChat += OnEnableChat;
            schmoozeController.EnableNudge += OnEnableNudge;
            schmoozeController.EnableIntroduction += OnEnableIntroduction;

            chatButton.onClick.AddListener(schmoozeController.Chat);
            nudgeButton.onClick.AddListener(schmoozeController.Nudge);
            introductionButton.onClick.AddListener(schmoozeController.Introduce);
        }

        public override void Show()
        {
            base.Show();
            timeTracker.Reset(schmoozeController.TotalSchmoozingTime);
            introductionButton.interactable = true;
            nudgeButton.interactable = true;
            chatButton.interactable = true;
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