using Gallerist.UI;
using System;
using UnityEngine;

namespace Gallerist
{
    public class UIManager : MonoBehaviour
    {
        GameStateMachine gameStateMachine;

        [SerializeField] ArtCard artCard;
        [SerializeField] PatronCard patronCard;

        [SerializeField] NewGameDisplay _newGameDisplay;
        [SerializeField] StartDisplay _startDisplay;
        [SerializeField] PatronsDisplay _patronsDisplay;
        [SerializeField] ArtPiecesDisplay _artPiecesDisplay;
        [SerializeField] SchmoozeDisplay _schmoozeDisplay;
        [SerializeField] PreparationDisplay _preparationDisplay;
        [SerializeField] MainEventDisplay _mainEventDisplay;
        [SerializeField] ClosingDisplay _closingDisplay;
        [SerializeField] EndDisplay _endDisplay;
        [SerializeField] DialogueDisplay _dialogueDisplay;
        [SerializeField] FinalDisplay _finalDisplay;

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();

            gameStateMachine.NewGame.StateEntered += LoadNewGameUI;
            gameStateMachine.StartState.StateEntered += LoadStartUI;
            gameStateMachine.Preparation.StateEntered += LoadPreparationUI;
            gameStateMachine.Schmooze.StateEntered += LoadSchmoozeUI;
            gameStateMachine.MainEvent.StateEntered += LoadMainEventUI;
            gameStateMachine.Closing.StateEntered += LoadClosingUI;
            gameStateMachine.End.StateEntered += LoadEndUI;
            gameStateMachine.Final.StateEntered += LoadFinalUI;
        }

        

        public void LoadNewGameUI(object sender, EventArgs e)
        {
            _newGameDisplay.gameObject.SetActive(true);
            _startDisplay.gameObject.SetActive(false);
            _mainEventDisplay.gameObject.SetActive(false);
            _preparationDisplay.gameObject.SetActive(false);
            _schmoozeDisplay.gameObject.SetActive(false);
            _patronsDisplay.gameObject.SetActive(false);
            _artPiecesDisplay.gameObject.SetActive(false);
            _closingDisplay.gameObject.SetActive(false);
            _endDisplay.gameObject.SetActive(false);
            _dialogueDisplay.gameObject.SetActive(false);
            _finalDisplay.gameObject.SetActive(false);
        }

        public void LoadStartUI(object sender, EventArgs e)
        {
            _newGameDisplay.gameObject.SetActive(false);
            _startDisplay.gameObject.SetActive(true);
            _mainEventDisplay.gameObject.SetActive(false);
            _preparationDisplay.gameObject.SetActive(false);
            _schmoozeDisplay.gameObject.SetActive(false);
            _patronsDisplay.gameObject.SetActive(false);
            _artPiecesDisplay.gameObject.SetActive(false);
            _closingDisplay.gameObject.SetActive(false);
            _endDisplay.gameObject.SetActive(false);
            _dialogueDisplay.gameObject.SetActive(false);
            _finalDisplay.gameObject.SetActive(false);
        }
        public void LoadPreparationUI(object sender, EventArgs e)
        {
            _newGameDisplay.gameObject.SetActive(false);
            _startDisplay.gameObject.SetActive(false);
            _mainEventDisplay.gameObject.SetActive(false);
            _preparationDisplay.gameObject.SetActive(true);
            _schmoozeDisplay.gameObject.SetActive(false);
            _patronsDisplay.gameObject.SetActive(false);
            _artPiecesDisplay.gameObject.SetActive(true);
            _closingDisplay.gameObject.SetActive(false);
            _endDisplay.gameObject.SetActive(false);
            _preparationDisplay.UpdateDisplay();
            _dialogueDisplay.gameObject.SetActive(false);
            _finalDisplay.gameObject.SetActive(false);
        }

        public void LoadSchmoozeUI(object sender, EventArgs e)
        {
            _newGameDisplay.gameObject.SetActive(false);
            _startDisplay.gameObject.SetActive(false);
            _mainEventDisplay.gameObject.SetActive(false);
            _preparationDisplay.gameObject.SetActive(false);
            _schmoozeDisplay.gameObject.SetActive(true);
            _patronsDisplay.gameObject.SetActive(true);
            _artPiecesDisplay.gameObject.SetActive(false);
            _closingDisplay.gameObject.SetActive(false);
            _endDisplay.gameObject.SetActive(false);
            _dialogueDisplay.gameObject.SetActive(false);
            _schmoozeDisplay.UpdateArtistCard();
            _finalDisplay.gameObject.SetActive(false);
        }

        public void LoadMainEventUI(object sender, EventArgs e)
        {
            _newGameDisplay.gameObject.SetActive(false);
            _startDisplay.gameObject.SetActive(false);
            _mainEventDisplay.gameObject.SetActive(true);
            _preparationDisplay.gameObject.SetActive(false);
            _schmoozeDisplay.gameObject.SetActive(false);
            _patronsDisplay.gameObject.SetActive(false);
            _artPiecesDisplay.gameObject.SetActive(false);
            _closingDisplay.gameObject.SetActive(false);
            _endDisplay.gameObject.SetActive(false);
            _dialogueDisplay.gameObject.SetActive(false);
            _finalDisplay.gameObject.SetActive(false);
        }

        public void LoadClosingUI(object sender, EventArgs e)
        {
            _newGameDisplay.gameObject.SetActive(false);
            _startDisplay.gameObject.SetActive(false);
            _mainEventDisplay.gameObject.SetActive(false);
            _preparationDisplay.gameObject.SetActive(false);
            _schmoozeDisplay.gameObject.SetActive(false);
            _patronsDisplay.gameObject.SetActive(true);
            _artPiecesDisplay.gameObject.SetActive(true);
            _closingDisplay.gameObject.SetActive(true);
            _endDisplay.gameObject.SetActive(false);
            _dialogueDisplay.gameObject.SetActive(false);
            _finalDisplay.gameObject.SetActive(false);
        }

        public void LoadEndUI(object sender, EventArgs e)
        {
            _newGameDisplay.gameObject.SetActive(false);
            _startDisplay.gameObject.SetActive(false);
            _mainEventDisplay.gameObject.SetActive(false);
            _preparationDisplay.gameObject.SetActive(false);
            _schmoozeDisplay.gameObject.SetActive(false);
            _patronsDisplay.gameObject.SetActive(false);
            _artPiecesDisplay.gameObject.SetActive(false);
            _closingDisplay.gameObject.SetActive(false);
            _endDisplay.gameObject.SetActive(true);
            _dialogueDisplay.gameObject.SetActive(false);
            _finalDisplay.gameObject.SetActive(false);
        }

        public void LoadFinalUI(object sender, EventArgs e)
        {
            _newGameDisplay.gameObject.SetActive(false);
            _startDisplay.gameObject.SetActive(false);
            _mainEventDisplay.gameObject.SetActive(false);
            _preparationDisplay.gameObject.SetActive(false);
            _schmoozeDisplay.gameObject.SetActive(false);
            _patronsDisplay.gameObject.SetActive(false);
            _artPiecesDisplay.gameObject.SetActive(false);
            _closingDisplay.gameObject.SetActive(false);
            _endDisplay.gameObject.SetActive(false);
            _dialogueDisplay.gameObject.SetActive(false);
            _finalDisplay.gameObject.SetActive(true);
        }
    }
}