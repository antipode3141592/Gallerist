using System;
using UnityEngine;

namespace Gallerist
{
    public class GameManager : MonoBehaviour
    {
        public string GalleryName;
        public string GalleristName;
        public Sprite GalleristPortrait;

        SchmoozeController schmoozeController;    
        GameStatsController gameStatsController;

        GameStates currentGameState = GameStates.MainMenu;
        public event EventHandler<GameStates> GameStateChanged;

        [SerializeField] int totalMonths = 6;

        
        public int TotalMonths => totalMonths;

        

        private void Awake()
        {
            schmoozeController = FindObjectOfType<SchmoozeController>();
            gameStatsController = FindObjectOfType<GameStatsController>();
            schmoozeController.SchmoozingCompleted += OnSchmoozeComplete;
            
        }

        void Start()
        {
            ChangeGameState(GameStates.NewGame);
        }

        void ChangeGameState(GameStates targetState)
        {
            if (currentGameState == targetState) return;
            currentGameState = targetState;
            GameStateChanged?.Invoke(this, targetState);
        }

        void OnSchmoozeComplete(object sender, EventArgs e)
        {
            if (currentGameState == GameStates.Schmooze1)
            {
                ChangeGameState(GameStates.MainEvent);
            }
            else if (currentGameState == GameStates.Schmooze2)
            {
                ChangeGameState(GameStates.Closing);
            }
        }

        public void CompleteNewGame()
        {
            ChangeGameState(GameStates.Start);
        }

        public void CompleteStart()
        {
            ChangeGameState(GameStates.Preparation);
        }

        public void CompletePreparations()
        {
            ChangeGameState(GameStates.Schmooze1);
        }

        public void CompleteMainEvent()
        {
            schmoozeController.ResetActionCounter();
            //remov
            ChangeGameState(GameStates.Schmooze2);
        }

        public void CompleteEvaluation()
        {
            ChangeGameState(GameStates.End);
        }

        public void CompleteEnd()
        {
            gameStatsController.Stats.CurrentMonth++;
            //cleanup
            ChangeGameState(GameStates.Start);
        }
    }
}