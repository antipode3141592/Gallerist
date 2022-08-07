using Gallerist.Events;
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

        [SerializeField] GameSettings gameSettings;

        public List<Patron> CurrentObjects { get; } = new List<Patron>();
        public List<Patron> PastObjects { get; } = new List<Patron>();

        public HashSet<string> AllCurrentTraitNames = new();

        Patron currentObject;
        public Patron CurrentObject 
        { 
            get => currentObject;
            set
            {
                currentObject = value;
                SelectedObjectChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public List<Patron> ExitingPatrons { get; } = new List<Patron>();

        public event EventHandler ObjectsGenerated;
        public event EventHandler SelectedObjectChanged;
        
        public event EventHandler<TraitModified> ObjectTraitModified;

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            gameStatsController = FindObjectOfType<GameStatsController>();
            artistManager = FindObjectOfType<ArtistManager>();
            artManager = FindObjectOfType<ArtManager>();
            nameDataSource = FindObjectOfType<NameDataSource>();
            spriteDataSource = FindObjectOfType<SpriteDataSource>();
            traitDataSource = FindObjectOfType<TraitDataSource>();
        }

        void Start()
        {
            
        }

        public void NewPatrons(int total)
        {
            if (CurrentObjects.Count > 0)
            {
                PastObjects.AddRange(CurrentObjects);
                CurrentObjects.Clear();
            }
            GeneratePatrons(total);
        }

        public void OnMainEventEntered()
        {
            RemovePatrons();
            int bored = gameStatsController.Stats.BoredGuests;
            if (Debug.isDebugBuild)
                Debug.Log($"There are {bored} patrons that have left");
            int incoming = Random.Range(bored/2 , bored*2) + Random.Range(0, 5);
            gameStatsController.Stats.MidPartyEntrances = incoming;
            GeneratePatrons(incoming);
        }

        void GeneratePatrons(int total)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"Generating {total} new patrons");
            for (int i = 0; i < total; i++)
            {
                if (PastObjects.Count > 0 && ReturningPatronChance())
                    PatronReturns();
                else
                    GeneratePatron();
            }
            ObjectsGenerated?.Invoke(this, EventArgs.Empty);
        }

        bool ReturningPatronChance()
        {
            float random = .1f * (1 * gameStatsController.Stats.TotalRenown);
            return Random.value < random;
        }

        void PatronReturns()
        {
            var patron = PastObjects[Random.Range(0, PastObjects.Count)];
            if (Debug.isDebugBuild)
                Debug.Log($"{patron.Name} has returned to the gallery!");
            CurrentObjects.Add(patron);
            PastObjects.Remove(patron);

        }

        public void GeneratePatron()
        {
            ITrait aesthetic = null;
            ITrait emotive = null;
            artManager.GetRandomTraits(out aesthetic, out emotive);

            var newPatron = new Patron(
                firstName: nameDataSource.GenerateRandomFirstName(),
                lastInitial: nameDataSource.RandomLastNameLetter(),
                portrait: spriteDataSource.GeneratePortrait(),
                isSubscriber: false,
                aestheticTraits: traitDataSource.GenerateAestheticTraits(5, typeof(PatronTrait), new() { aesthetic.Name }),
                emotiveTraits: traitDataSource.GenerateEmotiveTraits(5, typeof(PatronTrait), new() { emotive.Name }),
                acquisitions: new(),
                aestheticThreshold: Random.Range(gameSettings.PatronThresholdLowerValue, gameSettings.PatronThresholdUpperValue),
                emotiveThreshold: Random.Range(gameSettings.PatronThresholdLowerValue, gameSettings.PatronThresholdUpperValue),
                boredomThreshold: Random.Range(gameSettings.PatronBoredomLower, gameSettings.PatronBoredomUpper));
            //  add a check for duplicates before adding to Patrons list
            newPatron.TraitModified += PatronTraitModified;
            if (ChanceOfInitialSubscription())
                newPatron.SetSubscription();
            CurrentObjects.Add(newPatron);
        }

        void PatronTraitModified(object sender, TraitModified e)
        {
            ObjectTraitModified?.Invoke(sender, e);
        }

        void RemovePatrons()
        {
            gameStatsController.Stats.MidPartyExits = ExitingPatrons.Count;
            for (int i = 0; i < ExitingPatrons.Count; i++)
            {
                if (CurrentObjects.Contains(ExitingPatrons[i]))
                {
                    ExitingPatrons[i].TraitModified -= PatronTraitModified;
                    CurrentObjects.Remove(ExitingPatrons[i]);
                }

                ExitingPatrons[i] = null;
            }
            ExitingPatrons.Clear();
        }

        public Patron GetObjectAt(int index)
        {
            return CurrentObjects[index];
        }

        public void SetCurrentObject(Patron patron)
        {
            CurrentObject = patron;
            AllCurrentTraitNames = GetAllCurrentTraitNames();

        }

        HashSet<string> GetAllCurrentTraitNames()
        {
            HashSet<string> traitNames = new();
            if (currentObject is not null)
            {
                foreach (var trait1 in currentObject.AestheticTraits)
                    if (!traitNames.Contains(trait1.Name))
                        traitNames.Add(trait1.Name);
                foreach (var trait2 in currentObject.EmotiveTraits)
                    if (!traitNames.Contains(trait2.Name))
                        traitNames.Add(trait2.Name);
            }
            return traitNames;
        }

        bool ChanceOfInitialSubscription()
        {
            int chance = Random.Range(gameStatsController.Stats.TotalRenown * 10, 100);
            return chance >= 50 ? true : false;

        }
    }
}