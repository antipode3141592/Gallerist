using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class ArtManager : MonoBehaviour, IObjectManager<Art>
    {
        GameStateMachine gameStateMachine;
        ArtistManager artistManager;
        NameDataSource nameDataSource;
        SpriteDataSource spriteDataSource;
        TraitDataSource traitDataSource;

        public List<Art> CurrentObjects { get; } = new List<Art>();

        public List<Art> PastObjects { get; } = new List<Art>();

        public HashSet<string> AllCurrentTraitNames = new();
        
        Art currentObject;

        public Art CurrentObject {
            get => currentObject;
            set 
            { 
                currentObject = value;
                SelectedObjectChanged?.Invoke(this, EventArgs.Empty);
            }
        }

        public event EventHandler ObjectsGenerated;
        public event EventHandler SelectedObjectChanged;

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            artistManager = FindObjectOfType<ArtistManager>();
            nameDataSource = FindObjectOfType<NameDataSource>();
            spriteDataSource = FindObjectOfType<SpriteDataSource>();
            traitDataSource = FindObjectOfType<TraitDataSource>();
        }

        public void GenerateArtpieces(int total)
        {
            CurrentObjects.Clear();
            if (Debug.isDebugBuild)
                Debug.Log($"Generating {total} new art pieces");
            for (int i = 0; i < total; i++)
            {
                GenerateArt();
            }
            ObjectsGenerated?.Invoke(this, EventArgs.Empty);
        }

        void GenerateArt()
        {
            ITrait aesthetic = null;
            ITrait emotive = null;
            artistManager.GetRandomTraits(out aesthetic, out emotive);


            //create piece of art
            var newArt = new Art(
                name: nameDataSource.GenerateArtName(),
                description: nameDataSource.GenerateArtDescription(),
                artistName: artistManager.Artist.Name,
                aestheticQualities: traitDataSource.GenerateAestheticTraits(3, typeof(ArtTrait), requiredTraits: new() { aesthetic.Name }, artistManager.Artist.Experience),
                emotiveQualities: traitDataSource.GenerateEmotiveTraits(3, typeof(ArtTrait), requiredTraits: new() { emotive.Name }, artistManager.Artist.Experience),
                image: spriteDataSource.GenerateArtImage()
                );
            //stats are based on Artist favoredTraits (ex: artist specializing in landscapes will tend to create landscapes)

            //add art to ArtPieces list
            CurrentObjects.Add(newArt);
        }

        public Art GetObjectAt(int index)
        {
            throw new NotImplementedException();
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

        public HashSet<string> GetAllTraitNames()
        {
            HashSet<string> traitNames = new();
            foreach (var art in CurrentObjects)
            {
                foreach (var trait in art.AestheticTraits)
                    traitNames.Add(trait.Name);
                foreach (var trait in art.EmotiveTraits)
                    traitNames.Add(trait.Name);
            }
            return traitNames;
        }

        public void GetRandomTraits(out ITrait aesthetic, out ITrait emotive)
        {
            var randomObject = CurrentObjects[Random.Range(0, CurrentObjects.Count)];
            aesthetic = randomObject.AestheticTraits[Random.Range(0, randomObject.AestheticTraits.Count)];
            emotive = randomObject.EmotiveTraits[Random.Range(0, randomObject.EmotiveTraits.Count)];
        }

        public void SetCurrentObject(Art art)
        {
            CurrentObject = art;
            AllCurrentTraitNames = GetAllCurrentTraitNames();
        }
    }
}