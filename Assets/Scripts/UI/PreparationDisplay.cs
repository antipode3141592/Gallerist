using System;
using UnityEngine;
using System.Linq;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class PreparationDisplay : MonoBehaviour
    {
        ArtistManager artistManager;
        PreparationController preparationController;
        [SerializeField] ArtistCard artistCard;

        [SerializeField] PreparationItems ambientMusicItems;
        [SerializeField] PreparationItems foodAndDrinkItems;
        [SerializeField] PreparationItems mainEventItems;

        [SerializeField] Button continueButton;
        

        void Awake()
        {
            artistManager = FindObjectOfType<ArtistManager>();
            preparationController = FindObjectOfType<PreparationController>();

            preparationController.AllOptionsSelected += EnableContinue;

            ambientMusicItems.SelectedItemChanged += AmbientMusicSelected;
            foodAndDrinkItems.SelectedItemChanged += FoodAndDrinkSelected;
            mainEventItems.SelectedItemChanged += MainEventSelected;

        }

        void OnEnable()
        {
            continueButton.interactable = false;
            for (int i = 0; i < preparationController.OptionsPerMonth; i++)
            {
                if (preparationController.AmbientMusics.Count > i)
                    ambientMusicItems.PreparationItemsList[i].SetItem(preparationController.AmbientMusics[i].Name,
                    preparationController.AmbientMusics[i].Modifiers);
                if (preparationController.FoodAndDrinks.Count > i)
                    foodAndDrinkItems.PreparationItemsList[i].SetItem(preparationController.FoodAndDrinks[i].Name,
                    preparationController.FoodAndDrinks[i].Modifiers);
                if (preparationController.MainEvents.Count > i)
                    mainEventItems.PreparationItemsList[i].SetItem(preparationController.MainEvents[i].Name,
                    preparationController.MainEvents[i].Modifiers);
            }
        }

        void EnableContinue(object sender, EventArgs e)
        {
            continueButton.interactable = true;
        }

        void MainEventSelected(object sender, EventArgs e)
        {
            preparationController.SetMainEvent(mainEventItems.SelectedItem.Name);
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