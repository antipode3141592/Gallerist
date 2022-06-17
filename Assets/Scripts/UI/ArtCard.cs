using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gallerist.UI
{
    public class ArtCard : MonoBehaviour
    {
        [SerializeField] List<TraitDisplay> aestheticTraitDisplays;
        [SerializeField] List<TraitDisplay> emotiveTraitDisplays;

        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] TextMeshProUGUI artistText;
        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] Image artImage;

        [SerializeField] Image aestheticTraitsBackground;
        [SerializeField] Image emotiveTraitsBackground;

        public void LoadArtCardData(Art art)
        {
            titleText.text = art.Name;
            artistText.text = art.ArtistName;
            descriptionText.text = art.Description;

            artImage.sprite = art.Image;

            for (int i = 0; i < aestheticTraitDisplays.Count; i++)
            {
                var trait = art.AestheticTraits[i];
                string traitText = trait.IsKnown ? $"{trait.Name} {trait.Value}" : $"(unknown)";
                aestheticTraitDisplays[i].UpdateText(traitText);
            }
            for (int i = 0; i < emotiveTraitDisplays.Count; i++)
            {
                var trait = art.EmotiveTraits[i];
                string traitText = trait.IsKnown ? $"{trait.Name} {trait.Value}" : $"(unknown)";
                emotiveTraitDisplays[i].UpdateText(traitText);
            }
        }
    }
}