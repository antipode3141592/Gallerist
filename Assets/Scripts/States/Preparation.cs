using FiniteStateMachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist.States
{
    public class Preparation : IState
    {

        public event EventHandler StateEntered;
        public event EventHandler StateExited;
        public bool IsComplete = false;

        public Preparation()
        {

        }

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