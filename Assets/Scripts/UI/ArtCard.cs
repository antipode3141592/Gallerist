using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gallerist.UI
{
    public class ArtCard : MonoBehaviour
    {
        [SerializeField] TraitDisplay traitDisplayPrefab;

        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] TextMeshProUGUI artistText;
        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] Image artImage;

        [SerializeField] Image aestheticTraitsBackground;
        [SerializeField] Image emotiveTraitsBackground;

        public void LoadArtCardData(Art art)
        {
            titleText.text = art.Name;
            artistText.text = art.ArtistId;
            descriptionText.text = art.Description;

            foreach(var trait in art.Traits)
            {
                var traitDisplay = Instantiate<TraitDisplay>(traitDisplayPrefab);
                string traitText = trait.IsKnown ? $"{trait.Name} {trait.Value}" : $"(unknown)";
                traitDisplay.UpdateText(traitText);
                if (trait.TraitType == TraitType.Aesthetic)
                {
                    traitDisplay.transform.SetParent(aestheticTraitsBackground.transform);
                    traitDisplay.transform.localPosition = Vector3.zero;
                } else if (trait.TraitType == TraitType.Emotive)
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