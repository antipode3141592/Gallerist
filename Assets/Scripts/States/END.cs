using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class END : IState
    {
        public bool IsComplete = false;
        public event EventHandler StateEntered;
        public event EventHandler StateExited;

        public void OnEnter()
        {
            IsComplete = false;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {

        }

        public void Tick()
        {

        }
    }
}