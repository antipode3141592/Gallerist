using System;
using UnityEngine;

namespace Gallerist
{
    public class SchmoozeController : MonoBehaviour
    {
        GameManager _gameManager;
        ArtistManager _artistManager;
        PatronManager _patronManager; 

        public int ElapsedTime { get; set; }
        public int TotalSchmoozingTime => 60;    //Schmooze for sixty minutes
        public int ChatTime => 5;
        public int IntroductionTime => 10;
        public int NudgeTime => 5;

        public event EventHandler<int> ActionTaken;
        public event EventHandler PatronUpdated;
        public event EventHandler SchmoozingCompleted;
        public event EventHandler<int> SchmoozingStarted;
        public event EventHandler<bool> EnableChat;
        public event EventHandler<bool> EnableNudge;
        public event EventHandler<bool> EnableIntroduction;

        void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _artistManager = FindObjectOfType<ArtistManager>();
            _patronManager = FindObjectOfType<PatronManager>();
            ActionTaken += CheckEndofSchmooze;
            _gameManager.GameStateChanged += OnGameStateChanged;
        }

        private void OnGameStateChanged(object sender, GameStates e)
        {
            switch (e)
            {
                case GameStates.Schmooze1:
                    ResetActionCounter();
                    SchmoozingStarted?.Invoke(this, TotalSchmoozingTime);
                    break;
                case GameStates.Schmooze2:
                    ResetActionCounter();
                    SchmoozingStarted?.Invoke(this, TotalSchmoozingTime);
                    break;
                default:
                    break;
            }
        }

        public void ResetActionCounter()
        {
            ElapsedTime = 0;
        }

        void CheckEndofSchmooze(object sender, int e)
        {
            if (ElapsedTime >= TotalSchmoozingTime)
                SchmoozingCompleted?.Invoke(this, EventArgs.Empty);
        }

        public void Chat()
        {
            Schmooze.Chat(_patronManager.SelectedObject);
            ElapsedTime += ChatTime;
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, ElapsedTime);

            EnableChat?.Invoke(this, TotalSchmoozingTime - ElapsedTime >= ChatTime);
            EnableIntroduction?.Invoke(this, TotalSchmoozingTime - ElapsedTime >= IntroductionTime);
            EnableNudge?.Invoke(this, TotalSchmoozingTime - ElapsedTime >= NudgeTime);
        }

        public void Introduce()
        {
            Schmooze.Introduce(_artistManager.Artist, _patronManager.SelectedObject);
            ElapsedTime += IntroductionTime;
            PatronUpdated?.Invoke(this, EventArgs.Empty);
            ActionTaken?.Invoke(this, ElapsedTime);

            EnableChat?.Invoke(this, TotalSchmoozingTime - ElapsedTime >= ChatTime);
            EnableIntroduction?.Invoke(this, TotalSchmoozingTime - ElapsedTime >= IntroductionTime);
            EnableNudge?.Invoke(this, TotalSchmoozingTime - ElapsedTime >= NudgeTime);
        }

        public void Nudge()
        {
            ElapsedTime += NudgeTime;

            EnableChat?.Invoke(this, TotalSchmoozingTime - ElapsedTime >= ChatTime);
            EnableIntroduction?.Invoke(this, TotalSchmoozingTime - ElapsedTime >= IntroductionTime);
            EnableNudge?.Invoke(this, TotalSchmoozingTime - ElapsedTime >= NudgeTime);
        }
    }
}