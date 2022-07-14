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

        GameManager gameManager;
        ArtManager artManager;

        public List<Patron> Patrons { get; } = new();
        public List<Patron> PreviousPatrons { get; } = new();

        public List<Patron> CurrentObjects { get; } = new List<Patron>();
        public List<Patron> PastObjects { get; } = new List<Patron>();

        public Patron SelectedPatron { get; set; }

        public Patron SelectedObject { get; set; }
        

        List<Patron> boredPatrons = new List<Patron>();

        public event EventHandler PatronsGenerated;

        public event EventHandler ObjectsGenerated;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            artManager = FindObjectOfType<ArtManager>();
            nameDataSource = FindObjectOfType<NameDataSource>();
            spriteDataSource = FindObjectOfType<SpriteDataSource>();
            traitDataSource = FindObjectOfType<TraitDataSource>();

            gameManager.GameStateChanged += OnGameStateChange;
        }

        private void OnGameStateChange(object sender, GameStates e)
        {
            if (e == GameStates.Start)
            {
                PreviousPatrons.AddRange(CurrentObjects);
                CurrentObjects.Clear();
                GeneratePatrons(20);
            } else if (e == GameStates.Schmooze2)
            {
                int bored = RemoveBoredPatrons();
                if (Debug.isDebugBuild)
                    Debug.Log($"There are {bored} patrons that have left");
                GeneratePatrons(bored + Random.Range(0, 5));
            }
        }

        public void GeneratePatrons(int total)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"Generating {total} new patrons");
            for (int i = 0; i < total; i++)
                GeneratePatron();
            PatronsGenerated?.Invoke(this, EventArgs.Empty);
            ObjectsGenerated?.Invoke(this, EventArgs.Empty);
        }

        public void GeneratePatron()
        {
            var newPatron = new Patron(
                name: nameDataSource.GenerateRandomPatronName(),
                portrait: spriteDataSource.GeneratePortrait(),
                isSubscriber: false,
                aestheticTraits: traitDataSource.GenerateAestheticTraits(5, typeof(PatronTrait)),
                emotiveTraits: traitDataSource.GenerateEmotiveTraits(5, typeof(PatronTrait)),
                acquisitions: new (),
                aestheticThreshold: Random.Range(8, 13),
                emotiveThreshold: Random.Range(8, 13));
            //  add a check for duplicates before adding to Patrons list
            CurrentObjects.Add(newPatron);
        }

        int RemoveBoredPatrons()
        {
            int boredPatronCount = 0;
            foreach(var patron in CurrentObjects)
            {
                foreach(var art in artManager.ArtPieces)
                {
                    if (patron.EvaluateArt(art) == EvaluationResultTypes.None)
                    {
                        boredPatrons.Add(patron);
                        boredPatronCount++;
                    }    
                }
            }

            for (int i = 0; i < boredPatrons.Count; i++)
            {
                if (Patrons.Contains(boredPatrons[i]))
                    Patrons.Remove(boredPatrons[i]);
                boredPatrons[i] = null;
            }
            boredPatrons.Clear();
            return boredPatronCount;
        }

        public void SubscribeToMailingList(Patron patron)
        {
            patron.IsSubscriber = true;
        }

        public Patron GetObjectAt(int index)
        {
            return CurrentObjects[index];
        }
    }
}