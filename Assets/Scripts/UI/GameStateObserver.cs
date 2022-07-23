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
            for (int i = 0; i < stateIcons.Count; i++)
            {
                stateIcons[i].Image.color = stateIcons[i].StateName.ToLower() == e.ToLower() ? Color.blue : Color.grey;
            }
        }
    }
}