using FiniteStateMachine;
using Gallerist.Data;
using System;

namespace Gallerist.States
{
    public class SchmoozeState : IState
    {
        SchmoozeController _schmoozeController;
        PatronManager _patronManager;
        GameSettings _gameSettings;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public bool IsComplete = false;
        public int SchmoozeCounter = 0;

        public int ElapsedTime = 0;
        public int TotalTime = 60;

        public event EventHandler<bool> EnableChat;
        public event EventHandler<bool> EnableNudge;
        public event EventHandler<bool> EnableIntroduction;

        public event EventHandler EnableContinue;

        public event EventHandler ActionTaken;
        public event EventHandler<string> ActionComplete;

        public SchmoozeState(PatronManager patronManager, SchmoozeController schmoozeController, GameSettings gameSettings)
        {
            _schmoozeController = schmoozeController;
            _schmoozeController.ActionComplete += OnControllerActionComplete;
            _patronManager = patronManager;
            _patronManager.SelectedObjectChanged += CurrentPatronChanged;
            _gameSettings = gameSettings;
        }

        void OnControllerActionComplete(object sender, string e)
        {
            ActionComplete?.Invoke(this, e);
        }

        void CurrentPatronChanged(object sender, EventArgs e)
        {
            CheckActionEnable();
        }

        public void OnEnter()
        {
            IsComplete = false;
            ElapsedTime = 0;
            TotalTime = _gameSettings.TotalSchmoozingTime;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            ElapsedTime = 0;
            SchmoozeCounter++;
            if (SchmoozeCounter >= 2)
                SchmoozeCounter = 0;
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {
            if (ElapsedTime >= TotalTime)
                EnableContinue?.Invoke(this, EventArgs.Empty);
        }

        public void EndSchmoozing()
        {
            IsComplete = true;
        }

        void CheckActionEnable()
        {
            int remainingTime = TotalTime - ElapsedTime;
            EnableChat?.Invoke(this, remainingTime >= _gameSettings.ChatTime
                && !_patronManager.CurrentObject.AllTraitsKnown);
            EnableIntroduction?.Invoke(this, remainingTime >= _gameSettings.IntroductionTime
                && !_patronManager.CurrentObject.HasMetArtist);
            EnableNudge?.Invoke(this, remainingTime >= _gameSettings.NudgeTime);
        }

        public void Chat()
        {
            ElapsedTime += _gameSettings.ChatTime;
            ActionTaken?.Invoke(this, EventArgs.Empty);
            _schmoozeController.Chat();
            CheckActionEnable();

        }

        public void Nudge()
        {
            ElapsedTime += _gameSettings.NudgeTime;
            ActionTaken?.Invoke(this, EventArgs.Empty);
            _schmoozeController.Nudge();
            CheckActionEnable();
        }

        public void Introduce()
        {
            ElapsedTime += _gameSettings.IntroductionTime;
            ActionTaken?.Invoke(this, EventArgs.Empty);
            _schmoozeController.Introduce();
            CheckActionEnable();
        }
    }
}