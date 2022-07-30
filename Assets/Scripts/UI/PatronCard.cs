using Gallerist.Events;
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

            patronManager.ObjectTraitModified += OnObjectTraitModified;
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
            isSubscriberToggle.isOn = patron.IsSubscriber;
            portraitImage.sprite = patron.Image;
            if (Debug.isDebugBuild)
                perceptionRangeText.text = $"Perception: {patron.PerceptionRange}";
            DisplayAcquiredArt();
            DisplayKnownTraits();
        }

        public void DisplayAcquiredArt()
        {
            Patron patron = patronManager.CurrentObject;
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
                aestheticTraits[i].UpdateText(trait, revealToggle);
            }
            for (int i = 0; i < patron.EmotiveTraits.Count; i++)
            {
                var trait = patron.EmotiveTraits[i];
                emotiveTraits[i].UpdateText(trait, revealToggle);
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