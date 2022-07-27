using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class Preparation : IState
    {
        PreparationController preparationController;

        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public bool IsComplete = false;

        public Preparation(PreparationController preparationController)
        {
            this.preparationController = preparationController;
        }

        public void OnEnter()
        {
            preparationController.OnPreparationEntered();
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