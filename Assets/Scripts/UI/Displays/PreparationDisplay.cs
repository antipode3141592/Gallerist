using System;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class PreparationDisplay : Display
    {
        ArtistManager artistManager;
        PreparationController preparationController;
        [SerializeField] ArtistCard artistCard;
        ArtPiecesDisplay artPiecesDisplay;

        [SerializeField] PreparationItems ambientMusicItems;
        [SerializeField] PreparationItems foodAndDrinkItems;
        [SerializeField] PreparationItems mainEventItems;

        [SerializeField] Button continueButton;


        protected override void Awake()
        {
            base.Awake();
            artistManager = FindObjectOfType<ArtistManager>();
            preparationController = FindObjectOfType<PreparationController>();
            artPiecesDisplay = GetComponentInChildren<ArtPiecesDisplay>();

            preparationController.AllOptionsSelected += EnableContinue;

            ambientMusicItems.SelectedItemChanged += AmbientMusicSelected;
            foodAndDrinkItems.SelectedItemChanged += FoodAndDrinkSelected;
            mainEventItems.SelectedItemChanged += MainEventSelected;

        }

        public override void Show()
        {
            base.Show();
            continueButton.interactable = false;
            for (int i = 0; i < preparationController.OptionsPerMonth; i++)
            {
                if (preparationController.AmbientMusics.Count > i)
                    ambientMusicItems.PreparationItemsList[i].SetItem(preparationController.AmbientMusics[i].Name,
                    preparationController.AmbientMusics[i].Modifiers);
                if (preparationController.FoodAndDrinks.Count > i)
                    foodAndDrinkItems.PreparationItemsList[i].SetItem(preparationController.FoodAndDrinks[i].Name,
                    preparationController.FoodAndDrinks[i].Modifiers);
                if (preparationController.Centerpieces.Count > i)
                    mainEventItems.PreparationItemsList[i].SetItem(preparationController.Centerpieces[i].Name,
                    preparationController.Centerpieces[i].Modifiers);
            }
            artPiecesDisplay.Pagination.SelectPage(0);
            ambientMusicItems.PreparationItemsList[0].button.Select();
        }

        void EnableContinue(object sender, EventArgs e)
        {
            continueButton.interactable = true;
            continueButton.Select();
        }

        void MainEventSelected(object sender, EventArgs e)
        {
            preparationController.SetCenterpiece(mainEventItems.SelectedItem.Name);
        }

        void FoodAndDrinkSelected(object sender, EventArgs e)
        {
            preparationController.SetFoodAndDrink(foodAndDrinkItems.SelectedItem.Name);
        }

        void AmbientMusicSelected(object sender, EventArgs e)
        {
            preparationController.SetAmbientMusic(ambientMusicItems.SelectedItem.Name);
        }

        public void UpdateDisplay()
        {
            if (artistCard == null) return;
            artistCard.LoadArtistCardData(artistManager.Artist);
        }

        public void PreparationComplete()
        {
            preparationController.PreparationsComplete();
        }
    }
}