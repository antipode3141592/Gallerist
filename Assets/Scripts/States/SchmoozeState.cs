using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class SchmoozeState : IState
    {
        EvaluationController evaluationController;
        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public bool IsComplete = false;
        public int SchmoozeCounter = 0;

        public int ElapsedTime = 0;
        public int TotalTime = 60;

        public SchmoozeState(EvaluationController evaluationController)
        {
            this.evaluationController = evaluationController;
        }

        public void OnEnter()
        {
            IsComplete = false;
            ElapsedTime = 0;
            TotalTime = 60;
            StateEntered?.Invoke(this, EventArgs.Empty);
        }

        public void OnExit()
        {
            evaluationController.OnSchmoozeEnd();
            ElapsedTime = 0;
            SchmoozeCounter++;
            if (SchmoozeCounter >= 2)
                SchmoozeCounter = 0;
            StateExited?.Invoke(this, EventArgs.Empty);
        }

        public void Tick()
        {

        }
    }
}