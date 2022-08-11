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

        [SerializeField] RectTransform UITransform;

        [SerializeField] List<Display> displayPrefabs;

        [SerializeField] List<Display> overlayDisplayList;

        Dictionary<Type, Display> _displays = new();
        Dictionary<Type, Display> _overlays = new();

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            foreach (var display in overlayDisplayList)
            {
                _overlays.Add(display.GetType(), display);
            }
        }

        void Start()
        {
            gameStateMachine.NewGame.StateEntered += LoadNewGameUI;
            gameStateMachine.StartState.StateEntered += LoadStartUI;
            gameStateMachine.Preparation.StateEntered += LoadPreparationUI;
            gameStateMachine.Schmooze.StateEntered += LoadSchmoozeUI;
            gameStateMachine.MainEvent.StateEntered += LoadMainEventUI;
            gameStateMachine.Closing.StateEntered += LoadClosingUI;
            gameStateMachine.End.StateEntered += LoadEndUI;
            gameStateMachine.Final.StateEntered += LoadFinalUI;
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

        void LoadOverlay(Type overlayType)
        {
            foreach (var display in _overlays)
            {
                if (display.Key == overlayType)
                    display.Value.Show();
                else
                    display.Value.Hide();
            }
        }

        public void LoadNewGameUI(object sender, EventArgs e)
        {
            CreateDisplays();
            LoadDisplay(typeof(NewGameDisplay));
        }

        void CreateDisplays()
        {
            foreach(Display prefab in displayPrefabs)
            {
                Display go = Instantiate<Display>(prefab, UITransform);
                _displays.Add(go.GetType(), go);
            }
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