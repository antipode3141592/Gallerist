using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Patron
    {
        public Patron(string name, Sprite portrait, bool isSubscriber, List<ITrait> traits, List<string> acquisitions, int aestheticThreshold, int emotiveThreshold)
        {
            Name = name;
            Portrait = portrait;
            IsSubscriber = isSubscriber;
            Acquisitions = acquisitions;
            Traits = traits;
            PerceptionRange = SetPerceptionRange();
            AestheticThreshold = aestheticThreshold;
            EmotiveThreshold = emotiveThreshold;
        }

        public Sprite Portrait { get; private set; }
        public string Name { get; private set; }
        public bool IsSubscriber { get; private set; }
        public List<string> Acquisitions { get; private set; }
        public List<ITrait> Traits { get; private set; }
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

            foreach(var trait in art.Traits)
            {
                int subtotal = trait.Value + PatronTraitValue(trait) + PerceptionRange;
                if (trait.TraitType == TraitType.Aesthetic)
                {
                    aestheticTotal += subtotal;
                }
                else if (trait.TraitType == TraitType.Emotive)
                {
                    emotiveTotal += subtotal;
                }
            }
            Debug.Log($"A: {aestheticTotal} At: {AestheticThreshold}, E: {emotiveTotal} Et: {EmotiveThreshold}");
            if (aestheticTotal >= AestheticThreshold && emotiveTotal >= EmotiveThreshold)
                return true;
            return false;
        }

        int PatronTraitValue(ITrait trait)
        {
            var patronTrait = Traits.FirstOrDefault(x => x.Name == trait.Name);
            if (patronTrait is not null)
                return patronTrait.Value;
            return 0;
        }
    }
}