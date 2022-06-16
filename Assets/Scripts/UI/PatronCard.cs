using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class PatronCard : MonoBehaviour
    {
        [SerializeField] TraitDisplay traitDisplayPrefab;

        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] Toggle isSubscriberToggle;  //noninteractable 
        [SerializeField] Image portraitImage;

        [SerializeField] Image aestheticTraitsBackground;
        [SerializeField] Image emotiveTraitsBackground;

        public void LoadPatronCardData(Patron patron)
        {
            nameText.text = patron.Name;
            isSubscriberToggle.isOn = patron.IsSubscriber;
            portraitImage.sprite = patron.Portrait;

            foreach (var trait in patron.Traits)
            {
                var traitDisplay = Instantiate<TraitDisplay>(traitDisplayPrefab);
                string traitText = trait.IsKnown ? $"{trait.Name} {trait.Value}" : $"(unknown)";
                traitDisplay.UpdateText(traitText);
                if (trait.TraitType == TraitType.Aesthetic)
                {
                    traitDisplay.transform.SetParent(aestheticTraitsBackground.transform);
                    traitDisplay.transform.localPosition = Vector3.zero;
                }
                else if (trait.TraitType == TraitType.Emotive)
                {
                    traitDisplay.transform.SetParent(emotiveTraitsBackground.transform);
                    traitDisplay.transform.localPosition = Vector3.zero;
                }
                else
                {
                    Destroy(traitDisplay);
                }
            }
        }
    }
}