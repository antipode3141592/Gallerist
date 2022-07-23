using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class START : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public bool IsComplete = false;
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