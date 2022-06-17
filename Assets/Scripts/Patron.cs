using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

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

        int SetPerceptionRange()
        {
            int n = (int)Random.Range(-2,2);    //int [-2,2]
            return n;
        }

        public bool EvaluateArt(Art art)
        {
            int aestheticTotal = 0;
            int emotiveTotal = 0;

            foreach(var trait in art.AestheticTraits)
            {
                aestheticTotal += trait.Value + PatronAestheticTraitValue(trait) + PerceptionRange;
            }
            foreach(var trait in art.EmotiveTraits)
            {
                aestheticTotal += trait.Value + PatronEmotiveTraitValue(trait) + PerceptionRange;
            }

            Debug.Log($"A: {aestheticTotal} At: {AestheticThreshold}, E: {emotiveTotal} Et: {EmotiveThreshold}");
            if (aestheticTotal >= AestheticThreshold && emotiveTotal >= EmotiveThreshold)
                return true;
            return false;
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
    }
}