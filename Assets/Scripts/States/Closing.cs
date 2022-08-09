using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class Closing : IState
    {
        SalesController salesController;

        public bool IsComplete = false;
        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public int Evaluations = 0;
        public int TotalSalesAttempts = 5;

        public Closing(SalesController salesController, int totalEvaluations = 5)
        {
            TotalSalesAttempts = totalEvaluations;
            this.salesController = salesController;
        }

        public void OnEnter()
        {
            Evaluations = 0;
            IsComplete = false;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            salesController.CheckSalesForAllPatrons();
        }

        public void Tick()
        {

        }
    }
}