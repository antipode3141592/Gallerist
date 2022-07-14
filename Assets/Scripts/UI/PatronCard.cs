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
        EvaluationController evaluationController;

        [SerializeField] List<TraitDisplay> aestheticTraits;
        [SerializeField] List<TraitDisplay> emotiveTraits;

        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] Toggle isSubscriberToggle;  //noninteractable 
        [SerializeField] Image portraitImage;

        [SerializeField] Image ArtAcquisitionsBackground;
        List<AcquiredArtEntry> acquiredArtEntries;
        [SerializeField] AcquiredArtEntry acquiredArtEntryPrefab;

        [SerializeField] Image aestheticTraitsBackground;
        [SerializeField] Image emotiveTraitsBackground;

        //debug fields
        [SerializeField] TextMeshProUGUI perceptionRangeText;
        [SerializeField] Button revealTraitsButton;
        bool revealToggle = false;

        void Awake()
        {
            patronManager = FindObjectOfType<PatronManager>();
            evaluationController = FindObjectOfType<EvaluationController>();
            if (!Debug.isDebugBuild)
                revealTraitsButton.gameObject.SetActive(false);
            evaluationController.EvaluationResultUpdated += OnEvaluationResultUpdated;
            acquiredArtEntries = new();
        }

        void OnEvaluationResultUpdated(object sender, string e)
        {
            DisplayAcquiredArt();
        }

        public void LoadPatronCardData()
        {
            Patron patron = patronManager.SelectedObject;
            nameText.text = patron.Name;
            isSubscriberToggle.isOn = patron.IsSubscriber;
            portraitImage.sprite = patron.Image;
            if (Debug.isDebugBuild)
                perceptionRangeText.text = $"Perception: {patron.PerceptionRange}";
            DisplayAcquiredArt();
            DisplayKnownTraits();
        }

        public void DisplayAcquiredArt()
        {
            Patron patron = patronManager.SelectedObject;
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
            if (revealToggle)
            {
                DisplayAllTraits();
            }
            else
            {
                DisplayKnownTraits();
            }
        }

        void DisplayAllTraits()
        {
            Patron patron = patronManager.SelectedObject;
            for (int i = 0; i < patron.AestheticTraits.Count; i++)
            {
                var trait = patron.AestheticTraits[i];
                string traitText = $"{trait.Name} {trait.Value}";
                aestheticTraits[i].UpdateText(traitText);
            }
            for (int i = 0; i < patron.EmotiveTraits.Count; i++)
            {
                var trait = patron.EmotiveTraits[i];
                string traitText = $"{trait.Name} {trait.Value}";
                emotiveTraits[i].UpdateText(traitText);
            }
        }

        void DisplayKnownTraits()
        {
            Patron patron = patronManager.SelectedObject;
            for (int i = 0; i < patron.AestheticTraits.Count; i++)
            {
                var trait = patron.AestheticTraits[i];
                string traitText = trait.IsKnown ? $"{trait.Name} {trait.Value}" : $"(unknown)";
                aestheticTraits[i].UpdateText(traitText);
            }
            for (int i = 0; i < patron.EmotiveTraits.Count; i++)
            {
                var trait = patron.EmotiveTraits[i];
                string traitText = trait.IsKnown ? $"{trait.Name} {trait.Value}" : $"(unknown)";
                emotiveTraits[i].UpdateText(traitText);
            }
        }
    }
}