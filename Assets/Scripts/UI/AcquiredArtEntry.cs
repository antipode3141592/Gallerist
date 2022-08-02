using Gallerist.Data;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class AcquiredArtEntry : MonoBehaviour
    {
        [SerializeField] Image thumbnail;
        [SerializeField] TextMeshProUGUI artistName;
        [SerializeField] TextMeshProUGUI title;
        [SerializeField] TextMeshProUGUI originalPrintText;
        [SerializeField] TextMeshProUGUI purchaseDate;

        public void SetDisplay(ArtAcquisition art)
        {
            thumbnail.sprite = art.Art.Image;
            artistName.text = art.Art.ArtistName;
            title.text = art.Art.Name;
            
            originalPrintText.text = art.IsOriginal ? "Original" : "Print";
            purchaseDate.text = $"Month {art.PurchaseDate + 1}";
        }
    }
}