using Gallerist.States;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist.UI
{
    public class GameStateObserver : MonoBehaviour
    {
        GameStateMachine gameStateMachine;
        [SerializeField] List<StateDisplay> stateIcons;

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            gameStateMachine.OnStateChanged += OnStateChanged;
        }

        void OnStateChanged(object sender, string e)
        {
            //if (Debug.isDebugBuild)
            //    Debug.Log($"GameStateObserver:  Evaluating {e.ToLower()} against {typeof(SchmoozeState).Name}");

            string mod = e.ToLower();
            if (mod == typeof(SchmoozeState).Name.ToLower())
            {
                mod += $"{gameStateMachine.Schmooze.SchmoozeCounter:d1}";
            }
            for (int i = 0; i < stateIcons.Count; i++)
            {
                stateIcons[i].Image.color = stateIcons[i].StateName.ToLower() == mod ? Color.blue : Color.grey;
            }
        }
    }
}