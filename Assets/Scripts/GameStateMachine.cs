using FiniteStateMachine;
using Gallerist.States;
using System;
using UnityEngine;

namespace Gallerist
{
    public class GameStateMachine : MonoBehaviour
    {
        GameStatsController gameStatsController;

        StateMachine _stateMachine;

        NewGame newGame = new();
        START startState = new();
        Preparation preparation = new();
        SchmoozeState schmooze = new();
        MainEvent mainEvent = new();
        Closing closing = new();
        END end = new();
        Final final = new();

        public NewGame NewGame => newGame;
        public START StartState => startState;
        public Preparation Preparation => preparation;
        public SchmoozeState Schmooze => schmooze;
        public MainEvent MainEvent => mainEvent;
        public Closing Closing => closing;
        public END End => end;
        public Final Final => final;

        public StateMachine StateMachine => _stateMachine;

        public event EventHandler<string> OnStateChanged;

        void Awake()
        {
            gameStatsController = FindObjectOfType<GameStatsController>();

            _stateMachine = new StateMachine();

            _stateMachine.OnStateChange += OnStateChangeHandler;

            At(newGame, startState, NewGameComplete());
            At(startState, preparation, StartComplete());
            At(preparation, schmooze, PreparationComplete());
            At(schmooze, mainEvent, Schmooze1Complete());
            At(mainEvent, schmooze, MainEventComplete());
            At(schmooze, closing, Schmooze2Complete());
            At(closing, end, ClosingComplete());
            At(end, startState, NextMonth());
            At(end, final, YearComplete());

            void At(IState from, IState to, Func<bool> condition) => _stateMachine.AddTransition(from, to, condition);
            //void AtAny(IState to, Func<bool> condition) => _stateMachine.AddAnyTransition(to, condition);


            Func<bool> NewGameComplete() => () => newGame.IsComplete;
            Func<bool> StartComplete() => () => startState.IsComplete;
            Func<bool> PreparationComplete() => () => preparation.IsComplete;
            Func<bool> Schmooze1Complete() => () => schmooze.ElapsedTime >= schmooze.TotalTime && schmooze.SchmoozeCounter == 0;
            Func<bool> MainEventComplete() => () => mainEvent.IsComplete;
            Func<bool> Schmooze2Complete() => () => schmooze.ElapsedTime >= schmooze.TotalTime && schmooze.SchmoozeCounter >= 1;
            Func<bool> ClosingComplete() => () => closing.Evaluations >= closing.TotalEvaluations;
            Func<bool> NextMonth() => () => end.IsComplete;
            Func<bool> YearComplete() => () => gameStatsController.Stats.CurrentMonth > gameStatsController.BaseGameStats.TotalMonths;
        }

        void Start()
        {
            _stateMachine.SetState(newGame);
        }

        void OnStateChangeHandler(object sender, string e)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"State change to {e}");
            OnStateChanged?.Invoke(this, e);
        }

        void Update()
        {
            _stateMachine.Tick();
        }
    }
}