using FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist.States
{
    public class Final : IState
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