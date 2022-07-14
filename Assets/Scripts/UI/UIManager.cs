using Gallerist.UI;
using UnityEngine;

namespace Gallerist
{
    public class UIManager : MonoBehaviour
    {
        GameManager gameManager;

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

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.GameStateChanged += OnGameStateChanged;
        }

        void OnGameStateChanged(object sender, GameStates e)
        {
            Debug.Log($"OnGameStateChangedCalled.  target state: {e}");
            switch (e)
            {
                case GameStates.NewGame:
                    LoadNewGameUI();
                    break;
                case GameStates.Start:
                    LoadStartUI();
                    break;
                case GameStates.Preparation:
                    LoadPreparationUI();
                    break;
                case GameStates.Schmooze1:
                    LoadSchmoozeUI();
                    break;
                case GameStates.MainEvent:
                    LoadMainEventUI();
                    break;
                case GameStates.Schmooze2:
                    LoadSchmoozeUI();
                    break;
                case GameStates.Closing:
                    LoadClosingUI();
                    break;
                case GameStates.End:
                    LoadEndUI();
                    break;
                default:
                    Debug.LogWarning($"invalid game state!");
                    break;
            }
        }

        public void LoadNewGameUI()
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
        }

        public void LoadStartUI()
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
        }
        public void LoadPreparationUI()
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
            _artPiecesDisplay.SetThumbnails();
            _preparationDisplay.UpdateDisplay();
            _dialogueDisplay.gameObject.SetActive(false);
        }

        public void LoadSchmoozeUI()
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

            //_patronsDisplay.SetPatrons();
            _schmoozeDisplay.UpdateArtistCard();
        }

        public void LoadMainEventUI()
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
        }

        public void LoadClosingUI()
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
        }

        public void LoadEndUI()
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

            _endDisplay.SummarizeNight();
        }
    }
}