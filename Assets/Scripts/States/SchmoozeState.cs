using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class SchmoozeState : IState
    {
        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public bool IsComplete = false;
        public int SchmoozeCounter = 0;

        public int ElapsedTime = 0;
        public int TotalTime = 60;

        public void OnEnter()
        {
            IsComplete = false;
            ElapsedTime = 0;
            TotalTime = 60;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            ElapsedTime = 0;
            SchmoozeCounter++;
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }
    }
}