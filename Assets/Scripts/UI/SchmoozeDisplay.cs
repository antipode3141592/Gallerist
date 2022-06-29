using System;
using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class SchmoozeDisplay : MonoBehaviour
    {
        GameManager gameManager;
        ArtistManager artistManager;
        [SerializeField] ArtistCard artistCard;
        [SerializeField] TextMeshProUGUI ActionCounterText;
        SchmoozeController schmoozeController;
        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            artistManager = FindObjectOfType<ArtistManager>();
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
            artistCard.LoadArtistCardData(artistManager.Artist);
        }

        public void ShowDialogue()
        {

        }

        public void HideDialogue()
        {

        }
    }
}