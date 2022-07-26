using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class Patron: IThumbnail
    {
        public Patron(string name, Sprite portrait, bool isSubscriber, List<ITrait> aestheticTraits, List<ITrait> emotiveTraits, List<Art> acquisitions, int aestheticThreshold, int emotiveThreshold)
        {
            Name = name;
            Image = portrait;
            IsSubscriber = isSubscriber;
            Acquisitions = acquisitions;
            AestheticTraits = aestheticTraits;
            EmotiveTraits = emotiveTraits;
            PerceptionRange = Random.Range(-1, 2);
            AestheticThreshold = aestheticThreshold;
            EmotiveThreshold = emotiveThreshold;
            BoredomThreshold = Random.Range(3,7);
        }

        public Sprite Image { get; private set; }
        public string Name { get; private set; }
        public bool IsSubscriber { get; set; }
        public List<Art> Acquisitions { get; private set; }
        public List<ITrait> AestheticTraits { get; private set; }
        public List<ITrait> EmotiveTraits { get; private set; }
        public int PerceptionRange { get; private set; }
        public int AestheticThreshold { get; set; }
        public int EmotiveThreshold { get; set; }
        public int BoredomThreshold { get; set; }
        public int Satisfaction { get; set; } = 0;


        public event EventHandler PreferencesUpdated;
        public event EventHandler<string> TraitRevealed;

        public EvaluationResultTypes EvaluateArt(Art art)
        {
            int aestheticTotal = PerceptionRange;
            int emotiveTotal = PerceptionRange;

            foreach(var trait in art.AestheticTraits)
            {
                aestheticTotal += AddTraitsIfMatched(trait, AestheticTraits);
            }
            foreach(var trait in art.EmotiveTraits)
            {
                emotiveTotal += AddTraitsIfMatched(trait, EmotiveTraits);
            }
            if (Debug.isDebugBuild)
                Debug.Log($"A: {aestheticTotal} At: {AestheticThreshold}, E: {emotiveTotal} Et: {EmotiveThreshold}");
            if (aestheticTotal >= AestheticThreshold && emotiveTotal >= EmotiveThreshold)
                return EvaluationResultTypes.Original;
            else if (aestheticTotal >= AestheticThreshold || emotiveTotal >= EmotiveThreshold)
                return EvaluationResultTypes.Print;
            else if (aestheticTotal > 0 && emotiveTotal > 0)
                return EvaluationResultTypes.Subscribe;
            return EvaluationResultTypes.None;
        }

        int AddTraitsIfMatched(ITrait artTrait, List<ITrait> patronTraits)
        {
            var patronTrait = patronTraits.Find(x => x.Name == artTrait.Name);
            if (patronTrait is not null)
                return patronTrait.Value + artTrait.Value;
            return 0;
        }

        public string RevealTrait()
        {
            List<ITrait> unknownTraits = new();
            unknownTraits = EmotiveTraits.FindAll(x => x.IsKnown == false);
            unknownTraits.AddRange(AestheticTraits.FindAll(x => x.IsKnown == false));
            if (unknownTraits.Count == 0)
                return "";
            int randomIndex = Random.Range(0, unknownTraits.Count);
            unknownTraits[randomIndex].IsKnown = true;

            //unknownTraits = EmotiveTraits.FindAll(x => x.IsKnown == false);
            //unknownTraits.AddRange(AestheticTraits.FindAll(x => x.IsKnown == false));
            //if (unknownTraits.Count == 0)
            //    if (Debug.isDebugBuild)
            //        Debug.Log($"Patron {Name} has no remaining unknown preferences!");

            PreferencesUpdated?.Invoke(this, EventArgs.Empty);
            TraitRevealed?.Invoke(this, $"{unknownTraits[randomIndex].Name}");
            if (Debug.isDebugBuild)
                Debug.Log($"{Name} has a preference for {unknownTraits[randomIndex].Name} at {unknownTraits[randomIndex].Value}");
            return unknownTraits[randomIndex].Name;
        }

        public void ModifyRandomTrait(int modifier)
        {
            ITrait _trait = Utilities.RandomBool() ?
                AestheticTraits[Random.Range(0,AestheticTraits.Count)] :
                EmotiveTraits[Random.Range(0,EmotiveTraits.Count)];
            if (Debug.isDebugBuild)
                Debug.Log($"randomly chosen trait: {_trait.Name}");
            ModifyTrait(_trait, modifier);
        }

        public void ModifyTrait(ITrait trait, int modifier)
        {
            var patronTrait = EmotiveTraits.FirstOrDefault(x => x.Name == trait.Name);
            if (patronTrait is not null)
            {
                if (Debug.isDebugBuild)
                    Debug.Log($"modifying {patronTrait.Name} {patronTrait.Value} by {modifier}");
                patronTrait.Value += modifier;
                PreferencesUpdated?.Invoke(this, EventArgs.Empty);
                return;
            }
            patronTrait = AestheticTraits.FirstOrDefault(x => x.Name == trait.Name);
            if (patronTrait is not null)
            {
                if (Debug.isDebugBuild)
                    Debug.Log($"modifying {patronTrait.Name} {patronTrait.Value} by {modifier}");
                patronTrait.Value += modifier;
                PreferencesUpdated?.Invoke(this, EventArgs.Empty);
                return;
            }
            if (Debug.isDebugBuild)
                Debug.Log($"{trait.Name} not found, no modifications made");
        }

        public void ModifyRandomMatchingTrait(List<ITrait> traits, int modifier)
        {
            int i = 0; 
            while(i < modifier)
            {
                ModifyTrait(traits[Random.Range(0, traits.Count)], 1);
                i++;
            }
            PreferencesUpdated?.Invoke(this, EventArgs.Empty);
        }

        public bool SetSubscription()
        {
            if (IsSubscriber)
                return false;
            IsSubscriber = true;
            for (int i = 0; i < 2; i++)
                RevealTrait();
            return true;
        }
    }
}