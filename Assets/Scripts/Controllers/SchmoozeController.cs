using Gallerist.States;
using System;
using UnityEngine;

namespace Gallerist
{
    public class SchmoozeController : MonoBehaviour
    {
        GameStateMachine _gameStateMachine;
        ArtistManager _artistManager;
        PatronManager _patronManager; 

        public int ElapsedTime { get; set; }
        public int TotalSchmoozingTime => 60;    //Schmooze for sixty minutes
        public int ChatTime => 5;
        public int IntroductionTime => 10;
        public int NudgeTime => 15;

        public event EventHandler PatronUpdated;
        public event EventHandler SchmoozingCompleted;
        public event EventHandler ActionTaken;
        public event EventHandler<bool> EnableChat;
        public event EventHandler<bool> EnableNudge;
        public event EventHandler<bool> EnableIntroduction;

        SchmoozeState SchmoozeState;

        void Awake()
        {
            _gameStateMachine = FindObjectOfType<GameStateMachine>();
            SchmoozeState = _gameStateMachine.Schmooze;
            _artistManager = FindObjectOfType<ArtistManager>();
            _patronManager = FindObjectOfType<PatronManager>();
        }

        public void Chat()
        {
            Schmooze.Chat(_patronManager.SelectedObject);
            SchmoozeState.ElapsedTime += ChatTime;
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, EventArgs.Empty);
            CheckActionEnable();
        }

        public void Introduce()
        {
            Schmooze.Introduce(_artistManager.Artist, _patronManager.SelectedObject);
            SchmoozeState.ElapsedTime += IntroductionTime;            
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, EventArgs.Empty);
            CheckActionEnable();
        }

        public void Nudge()
        {
            Schmooze.Nudge(_patronManager.SelectedObject);
            SchmoozeState.ElapsedTime += NudgeTime;
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, EventArgs.Empty);
            CheckActionEnable();
        }

        void CheckActionEnable()
        {
            int remainingTime = SchmoozeState.TotalTime - SchmoozeState.ElapsedTime;
            EnableChat?.Invoke(this, remainingTime >= ChatTime);
            EnableIntroduction?.Invoke(this, remainingTime >= IntroductionTime);
            EnableNudge?.Invoke(this, remainingTime >= NudgeTime);
        }
    }
}