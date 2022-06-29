using System;
using UnityEngine;

namespace Gallerist
{
    public class GameManager : MonoBehaviour
    {
        SchmoozeController schmoozeController;    

        GameStates currentGameState = GameStates.End;
        public event EventHandler<GameStates> GameStateChanged;
        

        private void Awake()
        {
            schmoozeController = FindObjectOfType<SchmoozeController>();
            schmoozeController.SchmoozingCompleted += OnSchmoozeComplete;
        }

        void Start()
        {
            ChangeGameState(GameStates.Start);
        }

        void ChangeGameState(GameStates targetState)
        {
            if (currentGameState == targetState) return;
            currentGameState = targetState;
            GameStateChanged?.Invoke(this, targetState);
        }

        private void OnSchmoozeComplete(object sender, EventArgs e)
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
            ChangeGameState(GameStates.Schmooze2);
        }

        public void CompleteEvaluation()
        {
            ChangeGameState(GameStates.End);
        }

        public void CompleteEnd()
        {
            //return to start
        }
    }
}