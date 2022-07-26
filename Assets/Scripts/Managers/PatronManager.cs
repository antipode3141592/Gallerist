using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class PatronManager : MonoBehaviour, IObjectManager<Patron>
    {
        NameDataSource nameDataSource;
        SpriteDataSource spriteDataSource;
        TraitDataSource traitDataSource;

        GameStateMachine gameStateMachine;
        GameStatsController gameStatsController;
        ArtistManager artistManager;
        ArtManager artManager;

        public List<Patron> CurrentObjects { get; } = new List<Patron>();
        public List<Patron> PastObjects { get; } = new List<Patron>();

        public Patron SelectedObject { get; set; }
        
        public List<Patron> ExitingPatrons { get; } = new List<Patron>();

        public event EventHandler ObjectsGenerated;

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            gameStatsController = FindObjectOfType<GameStatsController>();
            artistManager = FindObjectOfType<ArtistManager>();
            artManager = FindObjectOfType<ArtManager>();
            nameDataSource = FindObjectOfType<NameDataSource>();
            spriteDataSource = FindObjectOfType<SpriteDataSource>();
            traitDataSource = FindObjectOfType<TraitDataSource>();

            gameStateMachine.StartState.StateEntered += OnStartEntered;
            gameStateMachine.MainEvent.StateEntered += OnMainEventEntered;
        }

        void OnStartEntered(object sender, EventArgs e)
        {
            PastObjects.AddRange(CurrentObjects);
            CurrentObjects.Clear();
            GeneratePatrons(20);
        }

        void OnMainEventEntered(object sender, EventArgs e)
        {
            RemovePatrons();
            int bored = gameStatsController.Stats.BoredGuests;
            if (Debug.isDebugBuild)
                Debug.Log($"There are {bored} patrons that have left");
            int incoming = bored + Random.Range(0, 5);
            gameStatsController.Stats.MidPartyEntrances = incoming;
            GeneratePatrons(incoming);
        }

        void GeneratePatrons(int total)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"Generating {total} new patrons");
            for (int i = 0; i < total; i++)
                GeneratePatron();
            ObjectsGenerated?.Invoke(this, EventArgs.Empty);
        }

        public void GeneratePatron()
        {
            ITrait aesthetic = null;
            ITrait emotive = null;
            artManager.GetRandomTraits(out aesthetic, out emotive);

            var newPatron = new Patron(
                name: nameDataSource.GenerateRandomPatronName(),
                portrait: spriteDataSource.GeneratePortrait(),
                isSubscriber: false,
                aestheticTraits: traitDataSource.GenerateAestheticTraits(5, typeof(PatronTrait), new() { aesthetic.Name }),
                emotiveTraits: traitDataSource.GenerateEmotiveTraits(5, typeof(PatronTrait), new() { emotive.Name }),
                acquisitions: new (),
                aestheticThreshold: Random.Range(8, 13),
                emotiveThreshold: Random.Range(8, 13));
            //  add a check for duplicates before adding to Patrons list
            CurrentObjects.Add(newPatron);
        }

        void RemovePatrons()
        {
            gameStatsController.Stats.MidPartyExits = ExitingPatrons.Count;
            for (int i = 0; i < ExitingPatrons.Count; i++)
            {
                if (CurrentObjects.Contains(ExitingPatrons[i]))
                    CurrentObjects.Remove(ExitingPatrons[i]);
                ExitingPatrons[i] = null;
            }
            ExitingPatrons.Clear();
        }

        public Patron GetObjectAt(int index)
        {
            return CurrentObjects[index];
        }
    }
}