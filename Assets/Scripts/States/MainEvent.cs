using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class MainEvent : IState
    {
        public bool IsComplete;



        public MainEvent()
        {
        }

        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
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