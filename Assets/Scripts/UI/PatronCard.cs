using Gallerist.Data;
using Gallerist.Events;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class PatronCard : MonoBehaviour
    {
        PatronManager patronManager;
        SalesController salesController;

        [SerializeField] List<TraitDisplay> aestheticTraits;
        [SerializeField] List<TraitDisplay> emotiveTraits;

        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] TextMeshProUGUI subscriberText;
        [SerializeField] Image portraitImage;

        [SerializeField] GameObject ArtAcquisitionsGroup;
        [SerializeField] Image ArtAcquisitionsBackground;
        List<AcquiredArtEntry> acquiredArtEntries = new();
        [SerializeField] AcquiredArtEntry acquiredArtEntryPrefab;

        [SerializeField] TextMeshProUGUI HasMetArtistText;
        [SerializeField] TextMeshProUGUI AllTraitsRevealedText;
        [SerializeField] TextMeshProUGUI SatisfactionLevelText;
        [SerializeField] TextMeshProUGUI BuyingLevelText;


        [SerializeField] Image aestheticTraitsBackground;
        [SerializeField] Image emotiveTraitsBackground;

        //debug fields
        [SerializeField] TextMeshProUGUI perceptionRangeText;
        [SerializeField] Button revealTraitsButton;
        bool revealToggle = false;

        void Awake()
        {
            patronManager = FindObjectOfType<PatronManager>();
            salesController = FindObjectOfType<SalesController>();
            if (!Debug.isDebugBuild)
                revealTraitsButton.gameObject.SetActive(false);
            ArtAcquisitionsGroup.SetActive(false);
        }

        void Start()
        {
            salesController.SalesResultUpdated += OnSalesResultsUpdated;
            patronManager.ObjectTraitModified += OnObjectTraitModified;
            salesController.SalesResultUpdated += OnEvaluationResultUpdated;
        }

        void OnSalesResultsUpdated(object sender, string e)
        {
            DisplayAcquiredArt();
        }

        void OnObjectTraitModified(object sender, TraitModified e)
        {
            Patron patron = sender as Patron;
            if (patron.Name == nameText.text)
                HighlightTrait(e.TraitName, e.Modifier);
        }

        void OnEvaluationResultUpdated(object sender, string e)
        {
            DisplayAcquiredArt();
        }

        public void LoadPatronCardData()
        {
            Patron patron = patronManager.CurrentObject;
            nameText.text = patron.Name;
            subscriberText.text = patron.IsSubscriber ? $"Mailing List Subscriber" : "";
            portraitImage.sprite = patron.Image;
            if (Debug.isDebugBuild)
                perceptionRangeText.text = $"Perception: {patron.PerceptionRange}";
            DisplayAcquiredArt();
            DisplayKnownTraits();

            SatisfactionLevelText.text = $"";
            BuyingLevelText.text = $"";

            HasMetArtistText.text = patron.HasMetArtist ? $"Has Met Artist" : "";
            AllTraitsRevealedText.text = patron.AllTraitsKnown ? "All Traits Known!" : "";
        }

        public void DisplayAcquiredArt()
        {
            Patron patron = patronManager.CurrentObject;
            if (patron.Acquisitions.Count == 0)
            {
                ArtAcquisitionsGroup.SetActive(false);
                return;
            }
            ArtAcquisitionsGroup.SetActive(true);
            for(int i = 0; i < acquiredArtEntries.Count; i++)
            {
                Destroy(acquiredArtEntries[i].gameObject);
            }
            acquiredArtEntries.Clear();
            foreach(var art in patron.Acquisitions)
            {
                var artEntry = Instantiate<AcquiredArtEntry>(acquiredArtEntryPrefab, ArtAcquisitionsBackground.transform);
                artEntry.SetDisplay(art);
                acquiredArtEntries.Add(artEntry);
            }
        }

        public void ToggleTraitsView()
        {
            revealToggle = !revealToggle;
            DisplayKnownTraits();
        }

        void DisplayKnownTraits()
        {
            Patron patron = patronManager.CurrentObject;
            for (int i = 0; i < patron.AestheticTraits.Count; i++)
            {
                var trait = patron.AestheticTraits[i];
                aestheticTraits[i].UpdateText(trait, revealToggle || trait.IsKnown ? $"{TraitLevelDescriptions.GetDescription(trait.Value).ToLower()} {trait.Name} ({trait.Value:+#;-#;0})" : $"(unknown)");
            }
            for (int i = 0; i < patron.EmotiveTraits.Count; i++)
            {
                var trait = patron.EmotiveTraits[i];
                emotiveTraits[i].UpdateText(trait, revealToggle || trait.IsKnown ? $"{TraitLevelDescriptions.GetDescription(trait.Value).ToLower()} {trait.Name} ({trait.Value:+#;-#;0})" : $"(unknown)");
            }
        }

        void HighlightTrait(string traitName, int modifier)
        {
            foreach (var trait in aestheticTraits)
            {
                if (trait.TraitName == traitName)
                {
                    trait.HighlightTrait(modifier);
                    return;
                }
            }
            foreach (var trait in emotiveTraits)
            {
                if (trait.TraitName == traitName)
                {
                    trait.HighlightTrait(modifier);
                    return;
                }
            }
        }
    }
}