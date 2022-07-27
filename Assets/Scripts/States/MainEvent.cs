using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class MainEvent : IState
    {
        PatronManager patronManager;

        public bool IsComplete;

        public MainEvent(PatronManager patronManager)
        {
            this.patronManager = patronManager;
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
            patronManager.OnMainEventEntered();
            StateEntered?.Invoke(this, EventArgs.Empty);
            IsComplete = false;
        }

        public void OnExit()
        {
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }
    }
}