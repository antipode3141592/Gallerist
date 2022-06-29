using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class PatronManager : MonoBehaviour
    {
        NameDataSource nameDataSource;
        SpriteDataSource spriteDataSource;
        TraitDataSource traitDataSource;

        GameManager gameManager;

        public List<Patron> Patrons { get; set; }

        public Patron SelectedPatron { get; set; }

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            nameDataSource = FindObjectOfType<NameDataSource>();
            spriteDataSource = FindObjectOfType<SpriteDataSource>();
            traitDataSource = FindObjectOfType<TraitDataSource>();
            Patrons = new List<Patron>();

            gameManager.GameStateChanged += OnGameStateChange;
        }

        private void OnGameStateChange(object sender, GameStates e)
        {
            if (e == GameStates.Start)
            {
                Patrons.Clear();
                GeneratePatrons(20);
            }
        }

        public void GeneratePatrons(int total)
        {
            for (int i = 0; i < total; i++)
                GeneratePatron();
        }

        public void GeneratePatron()
        {
            var newPatron = new Patron(
                name: nameDataSource.GenerateRandomPatronName(),
                portrait: spriteDataSource.GeneratePortrait(),
                isSubscriber: false,
                aestheticTraits: traitDataSource.GenerateAestheticTraits(5, typeof(PatronTrait)),
                emotiveTraits: traitDataSource.GenerateEmotiveTraits(5, typeof(PatronTrait)),
                acquisitions: new List<string>(),
                aestheticThreshold: Random.Range(8, 12),
                emotiveThreshold: Random.Range(8, 12));
            //  add a check for duplicates before adding to Patrons list
            Patrons.Add(newPatron);
        }

        public void RemoveBoredPatrons()
        {

        }
    }
}