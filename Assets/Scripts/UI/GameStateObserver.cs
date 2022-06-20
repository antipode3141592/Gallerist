using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class GameStateObserver : MonoBehaviour
    {
        GameManager gameManager;
        [SerializeField] List<Image> stateIcons;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            gameManager.GameStateChanged += UpdateGameStateText;
        }

        private void UpdateGameStateText(object sender, GameStates e)
        {
            for (int i = 0; i < stateIcons.Count; i++)
            {
                stateIcons[i].color = i == (int)e ? Color.blue : Color.grey;
            }
        }
    }
}