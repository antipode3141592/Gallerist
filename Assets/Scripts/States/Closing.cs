using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class Closing : IState
    {
        public bool IsComplete = false;
        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public int Evaluations = 0;
        public int TotalSalesAttempts = 5;

        public Closing(int totalEvaluations = 5)
        {
            TotalSalesAttempts = totalEvaluations;
        }

        public void OnEnter()
        {
            Evaluations = 0;
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