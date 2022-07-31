using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class NewGameDisplay : Display
    {
        GameStateMachine gameStateMachine;
        GameStatsController gameStatsController;

        [SerializeField] TMP_InputField galleristNameInput;
        [SerializeField] TMP_InputField galleryNameInput;

        protected override void Awake()
        {
            base.Awake();
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            gameStatsController = FindObjectOfType<GameStatsController>();
        }

        private void Start()
        {
            galleristNameInput.Select();
        }

        public void StartGame()
        {
            gameStatsController.Stats.GalleristName = galleristNameInput.text;
            gameStatsController.Stats.GalleryName = galleryNameInput.text;
            gameStateMachine.NewGame.IsComplete = true;

            if (Debug.isDebugBuild)
                Debug.Log($"Storing Name: {gameStatsController.Stats.GalleristName}, Gallery {gameStatsController.Stats.GalleryName}");
        }
    }
}