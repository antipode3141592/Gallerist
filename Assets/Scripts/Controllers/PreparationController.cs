using Gallerist.Data;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class PreparationController : MonoBehaviour
    {
        GameManager gameManager;
        TraitDataSource traitDataSource;
        AmbientMusicDataSource ambientMusicDataSource;
        FoodAndDrinkDataSource foodAndDrinkDataSource;
        MainEventDataSource mainEventDataSource;

        public List<AmbientMusic> AmbientMusics { get; } = new();
        public List<FoodAndDrink> FoodAndDrinks { get; } = new();
        public List<MainEvent> MainEvents { get; } = new();

        AmbientMusic currentAmbientMusic;
        FoodAndDrink currentFoodAndDrink;
        MainEvent currentMainEvent;

        public int OptionsPerMonth = 3;

        int _optionsSelected = 0;

        public event EventHandler OptionsCreated;
        public event EventHandler AllOptionsSelected;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
            traitDataSource = FindObjectOfType<TraitDataSource>();
            ambientMusicDataSource = FindObjectOfType<AmbientMusicDataSource>();
            foodAndDrinkDataSource = FindObjectOfType<FoodAndDrinkDataSource>();
            mainEventDataSource = FindObjectOfType<MainEventDataSource>();

            gameManager.GameStateChanged += OnGameStateChange;
        }

        void OnGameStateChange(object sender, GameStates e)
        {
            
            if (e == GameStates.Start)
            {
                CreateOptions();
                _optionsSelected = 0;
            }
        }

        public void SetAmbientMusic(string musicName)
        {
            var _currentMusic = AmbientMusics.Find(x => x.Name == musicName);
            if (_currentMusic is null) return;
            currentAmbientMusic = _currentMusic;
            if (Debug.isDebugBuild)
                Debug.Log($"Ambient Music Selected : {currentAmbientMusic.Name}");
            _optionsSelected++;
            if (_optionsSelected >= 3)
                AllOptionsSelected?.Invoke(this, EventArgs.Empty);
        }

        public void SetFoodAndDrink(string foodName)
        {
            var _currentFood = FoodAndDrinks.Find(x => x.Name == foodName);
            if (_currentFood is null) return;
            currentFoodAndDrink = _currentFood;
            if (Debug.isDebugBuild)
                Debug.Log($"Current Food and Drink Selected : {currentFoodAndDrink.Name}");
            _optionsSelected++;
            if (_optionsSelected >= 3)
                AllOptionsSelected?.Invoke(this, EventArgs.Empty);
        }

        public void SetMainEvent(string eventName)
        {
            var _currentMainEvent = MainEvents.Find(x => x.Name == eventName);
            if (_currentMainEvent is null) return;
            currentMainEvent = _currentMainEvent;
            if (Debug.isDebugBuild)
                Debug.Log($"Current Main Event: {currentMainEvent.Name}");
            _optionsSelected++;
            if (_optionsSelected >= 3)
                AllOptionsSelected?.Invoke(this, EventArgs.Empty);
        }

        public void CreateOptions()
        {
            AmbientMusics.Clear();
            FoodAndDrinks.Clear();
            MainEvents.Clear();
            for (int i = 0; i < OptionsPerMonth; i++)
            {
                AmbientMusics.Add(
                    new AmbientMusic(
                        name: ambientMusicDataSource.GetRandomItem(),
                        description: "",
                        idsToModify: traitDataSource.GetRandomTraitNames(5, TraitType.Aesthetic)
                        )
                    );
                FoodAndDrinks.Add(
                    new FoodAndDrink(
                        name: foodAndDrinkDataSource.GetRandomItem(),
                        description: "",
                        idsToModify: traitDataSource.GetRandomTraitNames(5, TraitType.Emotive)
                        )
                    );
                MainEvents.Add(
                    new MainEvent(
                        name: mainEventDataSource.GetRandomItem(),
                        description: "",
                        idsToModify: traitDataSource.GetRandomTraitNames(5, TraitType.Emotive)
                        )
                    );
            }
            OptionsCreated?.Invoke(this, EventArgs.Empty);
        }

        public void PreparationsComplete()
        {
            gameManager.CompletePreparations();
        }
    }

    public class AmbientMusic : IModifier
    {
        public string Name;
        public string Description;
        public TraitType TypeToModify => TraitType.Aesthetic;
        public List<string> IdsToModify { get; }
        public string Modifiers => $"({string.Join(',', IdsToModify)})";

        public AmbientMusic(string name, string description, List<string> idsToModify)
        {
            Name = name;
            Description = description;
            IdsToModify = idsToModify;
        }
    }

    public class FoodAndDrink : IModifier
    {
        public string Name;
        public string Description;
        public TraitType TypeToModify => TraitType.Emotive;
        public List<string> IdsToModify { get; }
        public string Modifiers => $"({string.Join(',', IdsToModify)})";

        public FoodAndDrink(string name, string description, List<string> idsToModify)
        {
            Name = name;
            Description = description;
            IdsToModify = idsToModify;
        }
    }

    public class MainEvent : IModifier
    {
        public string Name;
        public string Description;
        public TraitType TypeToModify => TraitType.Emotive;
        public List<string> IdsToModify { get; }
        public string Modifiers => $"({string.Join(',', IdsToModify)})";

        public MainEvent(string name, string description, List<string> idsToModify)
        {
            Name = name;
            Description = description;
            IdsToModify = idsToModify;
        }
    }
}