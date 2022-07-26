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
            portraitImage.sprite = artist.Image;

            for (int i = 0; i < artist.FavoredAestheticTraits.Count; i++)
            {
                var trait = artist.FavoredAestheticTraits[i];
                aestheticTraits[i].UpdateText(trait);
            }
            for (int i = 0; i < artist.FavoredEmotiveTraits.Count; i++)
            {
                var trait = artist.FavoredEmotiveTraits[i];
                emotiveTraits[i].UpdateText(trait);
            }
        }
    }
}