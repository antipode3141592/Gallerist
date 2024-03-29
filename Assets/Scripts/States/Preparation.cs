using FiniteStateMachine;
using System;

namespace Gallerist.States
{
    public class Preparation : IState
    {
        ArtManager _artManager;
        PreparationController _preparationController;

        public event EventHandler StateEntered;


        public event EventHandler StateExited;
        public bool IsComplete = false;

        public Preparation(PreparationController preparationController, ArtManager artManager)
        {
            _preparationController = preparationController;
            _artManager = artManager;

            _artManager.SelectedObjectChanged += OnSelectedArtChanged;
            _preparationController.PreparationComplete += OnPreparationComplete;
        }

        void OnPreparationComplete(object sender, EventArgs e)
        {
            IsComplete = true;
        }

        void OnSelectedArtChanged(object sender, EventArgs e)
        {
            
        }

        public void OnEnter()
        {
            _preparationController.OnPreparationEntered();
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