using Gallerist.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class ArtistManager : MonoBehaviour
    {
        GameStateMachine gameStateMachine;
        NameDataSource nameDataSource;
        SpriteDataSource spriteDataSource;
        TraitDataSource traitDataSource;

        public Artist Artist { get; set; } = null;

        public HashSet<string> ArtistTraits = new();

        public List<Artist> PreviousArtists { get; } = new List<Artist>();

        public event EventHandler ArtistGenerated;

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            nameDataSource = FindObjectOfType<NameDataSource>();
            spriteDataSource = FindObjectOfType<SpriteDataSource>();
            traitDataSource = FindObjectOfType<TraitDataSource>();
            //gameStateMachine.StartState.StateEntered += OnNewGame;
        }

        public void NewArtist()
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
            ArtistGenerated?.Invoke(this, EventArgs.Empty);
        }

        void GenerateArtist()
        {
            ArtistTraits.Clear();
            Artist = new Artist(
                name: nameDataSource.GenerateRandomArtistName(),
                bio: ArtistBios.Bios["default"],
                favoredAestheticTraits: traitDataSource.GenerateAestheticTraits(3, typeof(ArtistTrait)),
                favoredEmotiveTraits: traitDataSource.GenerateEmotiveTraits(3, typeof(ArtistTrait)),
                portrait: spriteDataSource.GeneratePortrait()
                );
            ArtistTraits = GetAllTraits();
        }

        HashSet<string> GetAllTraits()
        {
            HashSet<string> retval = new();
            foreach (var trait in Artist.FavoredAestheticTraits)
                retval.Add(trait.Name);
            foreach (var trait in Artist.FavoredEmotiveTraits)
                retval.Add(trait.Name);
            return retval;
        }

        public void GetRandomTraits(out ITrait aesthetic, out ITrait emotive)
        {
            aesthetic = Artist.FavoredAestheticTraits[Random.Range(0, Artist.FavoredAestheticTraits.Count)];
            emotive = Artist.FavoredEmotiveTraits[Random.Range(0, Artist.FavoredEmotiveTraits.Count)];
        }
    }
}