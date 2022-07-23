using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class SchmoozeDisplay : MonoBehaviour
    {
        ArtistManager artistManager;
        SchmoozeController schmoozeController;
        
        GameStateMachine gameStateMachine;

        [SerializeField] ArtistCard artistCard;
        [SerializeField] TimeTracker timeTracker;

        [SerializeField] Button chatButton;
        [SerializeField] Button nudgeButton;
        [SerializeField] Button introductionButton;


        void Awake()
        {
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

        private void OnEnableIntroduction(object sender, bool e)
        {
            introductionButton.interactable = e;
        }

        private void OnEnableNudge(object sender, bool e)
        {
            nudgeButton.interactable = e;
        }

        private void OnEnableChat(object sender, bool e)
        {
            chatButton.interactable = e;
        }

        void OnEnable()
        {
            timeTracker.Reset(schmoozeController.TotalSchmoozingTime);
            introductionButton.interactable = true;
            nudgeButton.interactable = true;
            chatButton.interactable = true;
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