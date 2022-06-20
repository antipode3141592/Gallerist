using System;
using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class SchmoozeDisplay : MonoBehaviour
    {
        GameManager gameManager;
        [SerializeField] ArtistCard artistCard;
        [SerializeField] TextMeshProUGUI ActionCounterText;
        SchmoozeController schmoozeController;
        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            schmoozeController = FindObjectOfType<SchmoozeController>();
            schmoozeController.ActionTaken += schmoozeActionTaken;
            UpdateActionCounter();
        }

        private void schmoozeActionTaken(object sender, EventArgs e)
        {
            UpdateActionCounter();

        }

        private void UpdateActionCounter()
        {
            
            string actions = $"Schmoozing:  {schmoozeController.ActionsTaken} of {schmoozeController.MaximumActions} actions taken.";
            Debug.Log(actions);
            ActionCounterText.text = actions;
        }

        public void UpdateArtistCard()
        {
            artistCard.LoadArtistCardData(gameManager.Artist);
        }
    }
}