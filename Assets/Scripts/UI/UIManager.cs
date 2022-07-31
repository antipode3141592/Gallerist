using Gallerist.UI;
using System;
using System.Collections.Generic;
using UnityEngine;
using Display = Gallerist.UI.Display;

namespace Gallerist
{
    public class UIManager : MonoBehaviour
    {
        GameStateMachine gameStateMachine;

        [SerializeField] List<Display> displayList;

        [SerializeField] DialogueDisplay _dialogueDisplay;

        Dictionary<Type, Display> _displays = new();

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

            foreach (var display in displayList)
            {
                _displays.Add(display.GetType(), display);
            }

            _dialogueDisplay.gameObject.SetActive(false);
        }

        void LoadDisplay(Type displayType)
        {
            foreach (var display in _displays)
            {
                if (display.Key == displayType)
                    display.Value.Show();
                else
                    display.Value.Hide();
            }
        }

        public void LoadNewGameUI(object sender, EventArgs e)
        {
            LoadDisplay(typeof(NewGameDisplay));
        }

        public void LoadStartUI(object sender, EventArgs e)
        {
            LoadDisplay(typeof(StartDisplay));
        }
        public void LoadPreparationUI(object sender, EventArgs e)
        {
            LoadDisplay(typeof(PreparationDisplay));
        }

        public void LoadSchmoozeUI(object sender, EventArgs e)
        {
            LoadDisplay(typeof(SchmoozeDisplay));
        }

        public void LoadMainEventUI(object sender, EventArgs e)
        {
            LoadDisplay(typeof(MainEventDisplay));
        }

        public void LoadClosingUI(object sender, EventArgs e)
        {
            LoadDisplay(typeof(ClosingDisplay));
        }

        public void LoadEndUI(object sender, EventArgs e)
        {
            LoadDisplay(typeof(EndDisplay));
        }

        public void LoadFinalUI(object sender, EventArgs e)
        {
            LoadDisplay(typeof(FinalDisplay));
        }
    }
}