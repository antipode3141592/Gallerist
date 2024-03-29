using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class NewGame : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public bool IsComplete = false;

        public event EventHandler EnableContinue;
        

        public void OnEnter()
        {
            StateEntered?.Invoke(this, EventArgs.Empty);
            IsComplete = false;
        }

        public void OnExit()
        {
        }

        public void Tick()
        {

        }
    }
}