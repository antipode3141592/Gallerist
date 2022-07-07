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

        public void SetDisplay(Art art)
        {
            thumbnail.sprite = art.Image;
            artistName.text = art.ArtistName;
            title.text = art.Name;
            
            originalPrintText.text = "";
        }
    }
}