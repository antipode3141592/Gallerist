using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class Patron
    {
        public Patron(string name, Sprite portrait, bool isSubscriber, List<ITrait> aestheticTraits, List<ITrait> emotiveTraits, List<string> acquisitions, int aestheticThreshold, int emotiveThreshold)
        {
            Name = name;
            Portrait = portrait;
            IsSubscriber = isSubscriber;
            Acquisitions = acquisitions;
            AestheticTraits = aestheticTraits;
            EmotiveTraits = emotiveTraits;
            PerceptionRange = SetPerceptionRange();
            AestheticThreshold = aestheticThreshold;
            EmotiveThreshold = emotiveThreshold;
        }

        public Sprite Portrait { get; private set; }
        public string Name { get; private set; }
        public bool IsSubscriber { get; private set; }
        public List<string> Acquisitions { get; private set; }
        public List<ITrait> AestheticTraits { get; private set; }
        public List<ITrait> EmotiveTraits { get; private set; }
        public int PerceptionRange { get; private set; }
        public int AestheticThreshold { get; set; }
        public int EmotiveThreshold { get; set; }

        public event EventHandler PreferencesUpdated;

        int SetPerceptionRange()
        {
            int n = (int)Random.Range(-2,3);    //int [-2,2]
            return n;
        }

        public EvaluationResultTypes EvaluateArt(Art art)
        {
            int aestheticTotal = 0;
            int emotiveTotal = 0;

            foreach(var trait in art.AestheticTraits)
            {
                aestheticTotal += trait.Value + PatronAestheticTraitValue(trait) + PerceptionRange;
            }
            foreach(var trait in art.EmotiveTraits)
            {
                emotiveTotal += trait.Value + PatronEmotiveTraitValue(trait) + PerceptionRange;
            }

            Debug.Log($"A: {aestheticTotal} At: {AestheticThreshold}, E: {emotiveTotal} Et: {EmotiveThreshold}");
            if (aestheticTotal >= AestheticThreshold && emotiveTotal >= EmotiveThreshold)
                return EvaluationResultTypes.Original;
            else if (aestheticTotal >= AestheticThreshold || emotiveTotal >= EmotiveThreshold)
                return EvaluationResultTypes.Print;
            return EvaluationResultTypes.None;
        }

        int PatronAestheticTraitValue(ITrait trait)
        {
            var patronTrait = AestheticTraits.FirstOrDefault(x => x.Name == trait.Name);
            if (patronTrait is not null)
                return patronTrait.Value;
            return 0;
        }

        int PatronEmotiveTraitValue(ITrait trait)
        {
            var patronTrait = EmotiveTraits.FirstOrDefault(x => x.Name == trait.Name);
            if (patronTrait is not null)
                return patronTrait.Value;
            return 0;
        }

        public void RevealTraits(int traitsToReveal)
        {
            if (traitsToReveal == 0) return;
            List<ITrait> unknownTraits = new();
            for (int i = 0; i < traitsToReveal; i++)
            {
                unknownTraits = EmotiveTraits.FindAll(x => x.IsKnown == false);
                unknownTraits.AddRange(AestheticTraits.FindAll(x => x.IsKnown == false));
                if (unknownTraits.Count == 0)
                    return;
                unknownTraits[Random.Range(0, unknownTraits.Count)].IsKnown = true;
            }

            unknownTraits = EmotiveTraits.FindAll(x => x.IsKnown == false);
            unknownTraits.AddRange(AestheticTraits.FindAll(x => x.IsKnown == false));
            if (unknownTraits.Count == 0)
                Debug.Log($"Patron {Name} has no remaining unknown preferences!");

            PreferencesUpdated?.Invoke(this, EventArgs.Empty);
        }

        public void ModifyTrait(ITrait trait, int modifier)
        {
            var patronTrait = EmotiveTraits.FirstOrDefault(x => x.Name == trait.Name);
            if (patronTrait is not null)
            {
                patronTrait.Value += modifier;
                return;
            }
            patronTrait = AestheticTraits.FirstOrDefault(x => x.Name == trait.Name);
            if (patronTrait is not null)
            {
                patronTrait.Value += modifier;
                return;
            }
        }
    }
}