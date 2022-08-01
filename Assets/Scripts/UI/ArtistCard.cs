using Gallerist.Data;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class ArtistCard : MonoBehaviour
    {
        ArtManager artManager;
        ArtistManager artistManager;
        PatronManager patronManager;
        GameStateMachine gameStateMachine;

        [SerializeField] List<TraitDisplay> aestheticTraits;
        [SerializeField] List<TraitDisplay> emotiveTraits;



        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] Image portraitImage;

        [SerializeField] Image aestheticTraitsBackground;
        [SerializeField] Image emotiveTraitsBackground;

        HashSet<string> comparisonSet = new();

        void Awake()
        {
            artManager = FindObjectOfType<ArtManager>();
            artistManager = FindObjectOfType<ArtistManager>();
            patronManager = FindObjectOfType<PatronManager>();
            gameStateMachine = FindObjectOfType<GameStateMachine>();

            
        }

        void Start()
        {
            artManager.SelectedObjectChanged += OnSelectedArtChanged;
            patronManager.SelectedObjectChanged += OnSelectedPatronChanged;

            gameStateMachine.Preparation.StateEntered += PreparationEntered;
            gameStateMachine.Schmooze.StateEntered += SchmoozeEntered;
        }

        private void SchmoozeEntered(object sender, EventArgs e)
        {
            comparisonSet = patronManager.AllCurrentTraitNames;
        }

        private void PreparationEntered(object sender, EventArgs e)
        {
            comparisonSet = artManager.AllCurrentTraitNames;
        }

        void OnSelectedPatronChanged(object sender, EventArgs e)
        {
            LoadArtistCardData(artistManager.Artist);
        }

        private void OnSelectedArtChanged(object sender, EventArgs e)
        {
            LoadArtistCardData(artistManager.Artist);
        }

        public void LoadArtistCardData(Artist artist)
        {
            nameText.text = artist.Name;
            portraitImage.sprite = artist.Image;

            for (int i = 0; i < artist.FavoredAestheticTraits.Count; i++)
            {
                var trait = artist.FavoredAestheticTraits[i];

                aestheticTraits[i].UpdateText(trait, 
                    $"{TraitLevelDescriptions.GetDescription(trait.Value)} {trait.Name}");
            }
            for (int i = 0; i < artist.FavoredEmotiveTraits.Count; i++)
            {
                var trait = artist.FavoredEmotiveTraits[i];
                emotiveTraits[i].UpdateText(trait,
                    $"{TraitLevelDescriptions.GetDescription(trait.Value)} {trait.Name}");
            }
        }
    }
}