using UnityEngine;

namespace Gallerist.UI
{
    public class PreparationDisplay : MonoBehaviour
    {
        GameManager gameManager;
        [SerializeField] ArtistCard artistCard;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        public void UpdateDisplay()
        {
            if (artistCard == null) return;
            artistCard.LoadArtistCardData(gameManager.Artist);
        }
    }
}