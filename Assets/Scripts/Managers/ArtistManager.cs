using Gallerist.Data;
using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class ArtistManager : MonoBehaviour
    {
        GameStatsController gameStatsController;
        NameDataSource nameDataSource;
        SpriteDataSource spriteDataSource;
        TraitDataSource traitDataSource;

        public Artist Artist { get; set; } = null;

        public HashSet<string> ArtistTraits = new();

        public List<Artist> PreviousArtists { get; } = new List<Artist>();

        public event EventHandler ArtistGenerated;

        void Awake()
        {
            gameStatsController = FindObjectOfType<GameStatsController>();
            nameDataSource = FindObjectOfType<NameDataSource>();
            spriteDataSource = FindObjectOfType<SpriteDataSource>();
            traitDataSource = FindObjectOfType<TraitDataSource>();
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
            ITrait aesthetic = null;
            ITrait emotive = null;
            if (PreviousArtists.Count > 0)
            {
                GetRandomTraits(PreviousArtists[PreviousArtists.Count - 1], out aesthetic, out emotive);
            }
            Artist = new Artist(
                name: nameDataSource.GenerateRandomArtistName(),
                bio: ArtistBios.Bios["default"],
                favoredAestheticTraits: traitDataSource.GenerateAestheticTraits(3, typeof(ArtistTrait), requiredTraits: aesthetic is not null ? new() { aesthetic.Name } : null),
                favoredEmotiveTraits: traitDataSource.GenerateEmotiveTraits(3, typeof(ArtistTrait), requiredTraits: emotive is not null ? new() { emotive.Name } : null),
                portrait: spriteDataSource.GeneratePortrait(),
                experience: gameStatsController.Stats.TotalRenown
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

        void GetRandomTraits(Artist artist, out ITrait aesthetic, out ITrait emotive)
        {
            aesthetic = artist.FavoredAestheticTraits[Random.Range(0, Artist.FavoredAestheticTraits.Count)];
            emotive = artist.FavoredEmotiveTraits[Random.Range(0, Artist.FavoredEmotiveTraits.Count)];
        }
    }
}