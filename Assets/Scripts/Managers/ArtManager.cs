using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class ArtManager : MonoBehaviour, IObjectManager<Art>
    {
        GameManager gameManager;
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
            gameManager = FindObjectOfType<GameManager>();
            artistManager = FindObjectOfType<ArtistManager>();
            nameDataSource = FindObjectOfType<NameDataSource>();
            spriteDataSource = FindObjectOfType<SpriteDataSource>();
            traitDataSource = FindObjectOfType<TraitDataSource>();

            gameManager.GameStateChanged += OnGameStateChange;
        }

        private void OnGameStateChange(object sender, GameStates e)
        {
            if (e == GameStates.Start)
            {
                CurrentObjects.Clear();
                GenerateArts(10);
            }
        }

        void GenerateArts(int total)
        {
            for (int i = 0; i < total; i++)
            {
                GenerateArt();
            }
            ObjectsGenerated?.Invoke(this, EventArgs.Empty);
        }

        void GenerateArt()
        {
            //create piece of art
            var newArt = new Art(
                name: nameDataSource.GenerateArtName(),
                description: nameDataSource.GenerateArtDescription(),
                artistName: artistManager.Artist.Name,
                aestheticQualities: traitDataSource.GenerateAestheticTraits(3, typeof(ArtTrait)),
                emotiveQualities: traitDataSource.GenerateEmotiveTraits(3, typeof(ArtTrait)),
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
    }
}