using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class ArtManager : MonoBehaviour
    {
        GameManager gameManager;
        ArtistManager artistManager;
        NameDataSource nameDataSource;
        SpriteDataSource spriteDataSource;
        TraitDataSource traitDataSource;
        public List<Art> ArtPieces { get; set; }

        public Art SelectedArt { get; set; }

        public event EventHandler ArtCreated;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            artistManager = FindObjectOfType<ArtistManager>();
            nameDataSource = FindObjectOfType<NameDataSource>();
            spriteDataSource = FindObjectOfType<SpriteDataSource>();
            traitDataSource = FindObjectOfType<TraitDataSource>();

            ArtPieces = new List<Art>();
            gameManager.GameStateChanged += OnGameStateChange;
        }

        private void OnGameStateChange(object sender, GameStates e)
        {
            if (e == GameStates.Start)
            {
                ArtPieces.Clear();
                GenerateArts(10);
            }
        }

        void GenerateArts(int total)
        {
            for (int i = 0; i < total; i++)
            {
                StartCoroutine(GenerateArt());
            }
        }

        IEnumerator GenerateArt()
        {
            if (artistManager.Artist == null)
            {
                Debug.Log("Artist is not yet ready...");
                yield return null;
            }
            Debug.Log("Begin generating art...");
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
            ArtPieces.Add(newArt);
            Debug.Log("Art Pieces generated!");
            ArtCreated?.Invoke(this, EventArgs.Empty);
        }
    }
}