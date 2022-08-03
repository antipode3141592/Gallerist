using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Gallerist.Data;

namespace Gallerist.UI
{
    public class ArtCard : MonoBehaviour
    {
        ArtManager artManager;
        ArtistManager artistManager;

        [SerializeField] List<TraitDisplay> aestheticTraitDisplays;
        [SerializeField] List<TraitDisplay> emotiveTraitDisplays;

        [SerializeField] TextMeshProUGUI titleText;
        [SerializeField] TextMeshProUGUI artistText;
        [SerializeField] TextMeshProUGUI descriptionText;
        [SerializeField] Image artImage;
        [SerializeField] TextMeshProUGUI availabilityText;
        [SerializeField] TextMeshProUGUI printsSoldText;

        [SerializeField] Image aestheticTraitsBackground;
        [SerializeField] Image emotiveTraitsBackground;

        void Awake()
        {
            artManager = FindObjectOfType<ArtManager>();
            artistManager = FindObjectOfType<ArtistManager>();
        }

        public void LoadArtCardData()
        {
            Art art = artManager.CurrentObject;

            titleText.text = art.Name;
            artistText.text = art.ArtistName;
            descriptionText.text = art.Description;
            availabilityText.text = art.IsSold ? "Sold" : "Available";
            availabilityText.color = art.IsSold ? Color.red : Color.white;
            printsSoldText.text = art.PrintsSold > 0 ? $"Prints Sold: {art.PrintsSold}" : "";

            artImage.sprite = art.Image;

            for (int i = 0; i < aestheticTraitDisplays.Count; i++)
            {
                var trait = art.AestheticTraits[i];
                aestheticTraitDisplays[i].UpdateText(trait, 
                    trait.IsKnown ? $"{ArtTraitLevelDescriptions.GetDescription(trait.Value).ToLower()} {trait.Name} ({trait.Value:+#;-#;0})" : $"(unknown)");
            }
            for (int i = 0; i < emotiveTraitDisplays.Count; i++)
            {
                var trait = art.EmotiveTraits[i];
                emotiveTraitDisplays[i].UpdateText(trait, 
                    trait.IsKnown ? $"{ArtTraitLevelDescriptions.GetDescription(trait.Value).ToLower()} {trait.Name} ({trait.Value:+#;-#;0})" : $"(unknown)");
            }
        }
    }
}