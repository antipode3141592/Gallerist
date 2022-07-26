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

        public Art SelectedObject { get; set; }

        public event EventHandler ObjectsGenerated;

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            artistManager = FindObjectOfType<ArtistManager>();
            nameDataSource = FindObjectOfType<NameDataSource>();
            spriteDataSource = FindObjectOfType<SpriteDataSource>();
            traitDataSource = FindObjectOfType<TraitDataSource>();

            gameStateMachine.StartState.StateEntered += OnGameStarted;
        }

        void OnGameStarted(object sender, EventArgs e)
        {
            CurrentObjects.Clear();
            GenerateArtpieces(10);
        }

        void GenerateArtpieces(int total)
        {
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
                aestheticQualities: traitDataSource.GenerateAestheticTraits(3, typeof(ArtTrait), requiredTraits: new() { aesthetic.Name }),
                emotiveQualities: traitDataSource.GenerateEmotiveTraits(3, typeof(ArtTrait), requiredTraits: new() { emotive.Name}),
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

        public void GetRandomTraits(out ITrait aesthetic, out ITrait emotive)
        {
            var randomObject = CurrentObjects[Random.Range(0, CurrentObjects.Count)];
            aesthetic = randomObject.AestheticTraits[Random.Range(0, randomObject.AestheticTraits.Count)];
            emotive = randomObject.EmotiveTraits[Random.Range(0, randomObject.EmotiveTraits.Count)];
        }
    }
}