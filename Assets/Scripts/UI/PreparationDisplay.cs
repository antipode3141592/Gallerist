using UnityEngine;

namespace Gallerist.UI
{
    public class PreparationDisplay : MonoBehaviour
    {
        GameManager gameManager;
        ArtistManager artistManager;
        PreparationController preparationController;
        [SerializeField] ArtistCard artistCard;


        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            artistManager = FindObjectOfType<ArtistManager>();
            preparationController = FindObjectOfType<PreparationController>();
        }

        public void UpdateDisplay()
        {
            if (artistCard == null) return;
            artistCard.LoadArtistCardData(artistManager.Artist);
        }
    }
}