using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Gallerist.UI
{
    public class GameStateObserver : MonoBehaviour
    {
        GameManager gameManager;
        [SerializeField] TextMeshProUGUI stateText;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.GameStateChanged += UpdateGameStateText;
        }

        private void UpdateGameStateText(object sender, GameStates e)
        {
            switch (e)
            {
                case GameStates.Start:
                    stateText.text = "Start";
                    break;
                case GameStates.Preparation:
                    stateText.text = "Preparation";
                    break;
                case GameStates.Schmooze1:
                    stateText.text = "First Schmooze Round";
                    break;
                case GameStates.MainEvent:
                    stateText.text = "Main Event";
                    break;
                case GameStates.Schmooze2:
                    stateText.text = "Second Schmooze Round";
                    break;
                case GameStates.Closing:
                    stateText.text = "Closing";
                    break;
                case GameStates.End:
                    stateText.text = "End";
                    break;
                default:
                    stateText.text = string.Empty;
                    break;
            }
        }
    }
}