using System;
using UnityEngine;

namespace Gallerist
{
    public class SchmoozeController : MonoBehaviour
    {
        GameManager _gameManager;
        ArtistManager _artistManager;
        PatronManager _patronManager; 

        public int ActionsTaken { get; private set; }
        public int MaximumActions => 12;    //Schmooze for one hour, each action is five minutes


        public event EventHandler ActionTaken;
        public event EventHandler PatronUpdated;
        public event EventHandler SchmoozingCompleted;


        void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _artistManager = FindObjectOfType<ArtistManager>();
            _patronManager = FindObjectOfType<PatronManager>();
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
            Schmooze.Chat(_patronManager.SelectedPatron);
            ActionsTaken++;
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, EventArgs.Empty);
        }

        public void Introduce()
        {
            Schmooze.Introduce(_artistManager.Artist, _patronManager.SelectedPatron);
            ActionsTaken++;
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, EventArgs.Empty);
        }

        public void Nudge()
        {

        }


    }
}