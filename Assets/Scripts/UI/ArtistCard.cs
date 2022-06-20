using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class ArtistCard : MonoBehaviour
    {
        [SerializeField] List<TraitDisplay> aestheticTraits;
        [SerializeField] List<TraitDisplay> emotiveTraits;

        [SerializeField] TextMeshProUGUI nameText;
        [SerializeField] Image portraitImage;

        [SerializeField] Image aestheticTraitsBackground;
        [SerializeField] Image emotiveTraitsBackground;

        public void LoadArtistCardData(Artist artist)
        {
            nameText.text = artist.Name;
            portraitImage.sprite = artist.Portrait;

            for (int i = 0; i < artist.FavoredAestheticTraits.Count; i++)
            {
                var trait = artist.FavoredAestheticTraits[i];
                string traitText = trait.IsKnown ? $"{trait.Name} {trait.Value}" : $"(unknown)";
                aestheticTraits[i].UpdateText(traitText);
            }
            for (int i = 0; i < artist.FavoredEmotiveTraits.Count; i++)
            {
                var trait = artist.FavoredEmotiveTraits[i];
                string traitText = trait.IsKnown ? $"{trait.Name} {trait.Value}" : $"(unknown)";
                emotiveTraits[i].UpdateText(traitText);
            }
        }
    }
}