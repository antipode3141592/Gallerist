using Gallerist.UI;
using System;
using UnityEngine;

namespace Gallerist
{
    public class SchmoozeController : MonoBehaviour
    {
        GameManager _gameManager;
        PatronCard _patronCard;

        public int ActionsTaken { get; private set; }
        public int MaximumActions => 12;    //Schmooze for one hour, each action is five minutes


        public event EventHandler ActionTaken;
        public event EventHandler PatronUpdated;
        public event EventHandler SchmoozingCompleted;


        void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _patronCard = FindObjectOfType<PatronCard>();
            ActionTaken += CheckEndofSchmooze;
            ResetActionCounter();
        }
        public void ResetActionCounter()
        {
            ActionsTaken = 0;
            ActionTaken?.Invoke(this, EventArgs.Empty);
        }

        void CheckEndofSchmooze(object sender, EventArgs e)
        {
            if (ActionsTaken >= MaximumActions)
                SchmoozingCompleted?.Invoke(this, EventArgs.Empty);
        }

        public void Chat()
        {
            Schmooze.Chat(_patronCard.SelectedPatron);
            ActionsTaken++;
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, EventArgs.Empty);
        }

        public void Introduce()
        {
            Schmooze.Introduce(_gameManager.Artist, _patronCard.SelectedPatron);
            ActionsTaken++;
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, EventArgs.Empty);
        }

        public void Nudge()
        {

        }


    }
}