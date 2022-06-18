using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class PatronCard : MonoBehaviour
    {
        [SerializeField] List<TraitDisplay> aestheticTraits;
        [SerializeField] List<TraitDisplay> emotiveTraits;

        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] Toggle isSubscriberToggle;  //noninteractable 
        [SerializeField] Image portraitImage;

        [SerializeField] Image aestheticTraitsBackground;
        [SerializeField] Image emotiveTraitsBackground;

        public Patron SelectedPatron;

        public void LoadPatronCardData(Patron patron)
        {
            SelectedPatron = patron;
            nameText.text = patron.Name;
            isSubscriberToggle.isOn = patron.IsSubscriber;
            portraitImage.sprite = patron.Portrait;

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