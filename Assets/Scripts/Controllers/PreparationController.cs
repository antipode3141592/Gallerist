using Gallerist.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gallerist
{
    public class PreparationController : MonoBehaviour
    {
        GameStateMachine gameStateMachine;
        TraitDataSource traitDataSource;
        AmbientMusicDataSource ambientMusicDataSource;
        FoodAndDrinkDataSource foodAndDrinkDataSource;
        CenterpieceDataSource centerpieceDataSource;

        ArtManager artManager;

        public List<AmbientMusic> AmbientMusics { get; } = new();
        public List<FoodAndDrink> FoodAndDrinks { get; } = new();
        public List<Centerpiece> Centerpieces { get; } = new();

        AmbientMusic currentAmbientMusic;
        FoodAndDrink currentFoodAndDrink;
        Centerpiece currentCenterpiece;

        bool AmbientMusicSelected;
        bool FoodAndDrinkSelected;
        bool CenterpieceSelected;

        bool AllSelected => AmbientMusicSelected && FoodAndDrinkSelected && CenterpieceSelected;

        public int OptionsPerMonth = 3;

        public event EventHandler OptionsCreated;
        public event EventHandler AllOptionsSelected;

        void Awake()
        {
            gameStateMachine = FindObjectOfType<GameStateMachine>();
            artManager = FindObjectOfType<ArtManager>();
            traitDataSource = FindObjectOfType<TraitDataSource>();
            ambientMusicDataSource = FindObjectOfType<AmbientMusicDataSource>();
            foodAndDrinkDataSource = FindObjectOfType<FoodAndDrinkDataSource>();
            centerpieceDataSource = FindObjectOfType<CenterpieceDataSource>();
        }

        public void OnPreparationEntered()
        {
            AmbientMusicSelected = false;
            FoodAndDrinkSelected = false;
            CenterpieceSelected = false;
            CreateOptions();
        }

        public void SetAmbientMusic(string musicName)
        {
            var _currentMusic = AmbientMusics.Find(x => x.Name == musicName);
            if (_currentMusic is null) return;
            currentAmbientMusic = _currentMusic;
            AmbientMusicSelected = true;
            if (Debug.isDebugBuild)
                Debug.Log($"Ambient Music Selected : {currentAmbientMusic.Name}");
            
            if (AllSelected)
                AllOptionsSelected?.Invoke(this, EventArgs.Empty);
        }

        public void SetFoodAndDrink(string foodName)
        {
            var _currentFood = FoodAndDrinks.Find(x => x.Name == foodName);
            if (_currentFood is null) return;
            currentFoodAndDrink = _currentFood;
            FoodAndDrinkSelected = true;
            if (Debug.isDebugBuild)
                Debug.Log($"Current Food and Drink Selected : {currentFoodAndDrink.Name}");
            if (AllSelected)
                AllOptionsSelected?.Invoke(this, EventArgs.Empty);
        }

        public void SetCenterpiece(string eventName)
        {
            var _currentMainEvent = Centerpieces.Find(x => x.Name == eventName);
            if (_currentMainEvent is null) return;
            currentCenterpiece = _currentMainEvent;
            CenterpieceSelected = true;
            if (Debug.isDebugBuild)
                Debug.Log($"Current Main Event: {currentCenterpiece.Name}");
            if (AllSelected)
                AllOptionsSelected?.Invoke(this, EventArgs.Empty);
        }

        public void CreateOptions()
        {
            AmbientMusics.Clear();
            FoodAndDrinks.Clear();
            Centerpieces.Clear();

            List<string> traitNames = artManager.GetAllTraitNames().ToList();
            Bag traitBag = new Bag(traitNames.Count);

            for (int i = 0; i < OptionsPerMonth; i++)
            {
                traitBag.ResetBag();

                AmbientMusics.Add(
                    new AmbientMusic(
                        name: ambientMusicDataSource.GetRandomItem(),
                        description: "",
                        idsToModify: new List<string> { 
                            traitNames[traitBag.DrawFromBag()],
                            traitNames[traitBag.DrawFromBag()],
                            traitNames[traitBag.DrawFromBag()]}
                        )
                    );
                FoodAndDrinks.Add(
                    new FoodAndDrink(
                        name: foodAndDrinkDataSource.GetRandomItem(),
                        description: "",
                        idsToModify: new List<string> {
                            traitNames[traitBag.DrawFromBag()],
                            traitNames[traitBag.DrawFromBag()],
                            traitNames[traitBag.DrawFromBag()]}
                        )
                    );
                Centerpieces.Add(
                    new Centerpiece(
                        name: centerpieceDataSource.GetRandomItem(),
                        description: "",
                        idsToModify: new List<string> {
                            traitNames[traitBag.DrawFromBag()],
                            traitNames[traitBag.DrawFromBag()],
                            traitNames[traitBag.DrawFromBag()]}
                        )
                    );
            }
            OptionsCreated?.Invoke(this, EventArgs.Empty);
        }

        public void PreparationsComplete()
        {
            gameStateMachine.Preparation.IsComplete = true;
        }
    }
}