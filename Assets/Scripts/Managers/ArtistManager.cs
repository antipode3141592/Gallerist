using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class ArtistManager : MonoBehaviour
    {
        GameManager gameManager;
        NameDataSource nameDataSource;
        SpriteDataSource spriteDataSource;
        TraitDataSource traitDataSource;

        public Artist Artist { get; set; } = null;

        public List<Artist> PreviousArtists { get; } = new List<Artist>();

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            nameDataSource = FindObjectOfType<NameDataSource>();
            spriteDataSource = FindObjectOfType<SpriteDataSource>();
            traitDataSource = FindObjectOfType<TraitDataSource>();
            gameManager.GameStateChanged += OnGameStateChange;
        }

        private void OnGameStateChange(object sender, GameStates e)
        {
            if (e == GameStates.Start)
            {
                if (Artist is not null)
                    PreviousArtists.Add(Artist);
                GenerateArtist();
                if (!Debug.isDebugBuild) return;
                Debug.Log($"Current Artist: {Artist.Name}");
                foreach (var artist in PreviousArtists)
                {
                    Debug.Log($"Previous Artist: {artist.Name}");
                }
            }
            
        }

        void GenerateArtist()
        {
            Artist = new Artist(
                name: nameDataSource.GenerateRandomArtistName(),
                favoredAestheticTraits: traitDataSource.GenerateAestheticTraits(3, typeof(ArtistTrait)),
                favoredEmotiveTraits: traitDataSource.GenerateEmotiveTraits(3, typeof(ArtistTrait)),
                portrait: spriteDataSource.GeneratePortrait()
                );
        }
    }
}