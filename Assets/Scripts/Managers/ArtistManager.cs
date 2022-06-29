using UnityEngine;

namespace Gallerist
{
    public class ArtistManager : MonoBehaviour
    {
        GameManager gameManager;
        NameDataSource nameDataSource;
        SpriteDataSource spriteDataSource;
        TraitDataSource traitDataSource;

        public Artist Artist { get; set; }

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
                Artist = null;
                GenerateArtist();
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