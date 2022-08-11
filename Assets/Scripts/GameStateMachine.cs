using FiniteStateMachine;
using Gallerist.Data;
using Gallerist.States;
using System;
using System.Collections;
using UnityEngine;

namespace Gallerist
{
    public class GameStateMachine : MonoBehaviour
    {
        [SerializeField] GameSettings gameSettings;

        ArtManager artManager;
        ArtistManager artistManager;
        PatronManager patronManager;
        GameStatsController gameStatsController;
        PreparationController preparationController;
        SalesController salesController;
        SchmoozeController schmoozeController;

        StateMachine _stateMachine;

        NewGame newGame;
        START startState;
        Preparation preparation;
        SchmoozeState schmooze;
        MainEvent mainEvent;
        Closing closing;
        END end;
        Final final;

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
            artistManager = FindObjectOfType<ArtistManager>();
            artManager = FindObjectOfType<ArtManager>();
            patronManager = FindObjectOfType<PatronManager>();

            gameStatsController = FindObjectOfType<GameStatsController>();
            preparationController = FindObjectOfType<PreparationController>();
            salesController = FindObjectOfType<SalesController>();
            schmoozeController = FindObjectOfType<SchmoozeController>();

            _stateMachine = new StateMachine();

            newGame = new();
            startState = new START(artManager, artistManager, patronManager, gameStatsController, gameSettings);
            preparation = new(preparationController, artManager);
            schmooze = new(patronManager, schmoozeController, gameSettings);
            mainEvent = new(patronManager, gameStatsController, salesController);
            closing = new(salesController);
            end = new(gameStatsController, artistManager, patronManager, artManager);
            final = new(gameStatsController);

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
            Func<bool> Schmooze1Complete() => () => schmooze.ElapsedTime >= gameSettings.TotalMonths && schmooze.SchmoozeCounter == 0 && schmooze.IsComplete;
            Func<bool> MainEventComplete() => () => mainEvent.IsComplete;
            Func<bool> Schmooze2Complete() => () => schmooze.ElapsedTime >= schmooze.TotalTime && schmooze.SchmoozeCounter >= 1 && schmooze.IsComplete;
            Func<bool> ClosingComplete() => () => closing.Evaluations >= closing.TotalSalesAttempts;
            Func<bool> NextMonth() => () => end.IsComplete && gameStatsController.Stats.CurrentMonth < gameSettings.TotalMonths;
            Func<bool> YearComplete() => () => end.IsComplete && gameStatsController.Stats.CurrentMonth >= gameSettings.TotalMonths;
        }

        IEnumerator Start()
        {
            yield return null;
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