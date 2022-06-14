using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Patron
    {
        public Patron(string name, bool isSubscriber, IList<ITrait> preferences, IList<string> acquisitions, int aestheticThreshold, int emotiveThreshold)
        {
            Name = name;
            IsSubscriber = isSubscriber;
            Acquisitions = acquisitions;
            Preferences = preferences;
            PerceptionRange = SetPerceptionRange();
            AestheticThreshold = aestheticThreshold;
            EmotiveThreshold = emotiveThreshold;
        }

        public string Name { get; private set; }
        public string Id { get; private set; }
        public bool IsSubscriber { get; private set; }
        public IList<string> Acquisitions { get; private set; }
        public IList<ITrait> Preferences { get; private set; }
        public int PerceptionRange { get; private set; }
        public int AestheticThreshold { get; set; }
        public int EmotiveThreshold { get; set; }

        int SetPerceptionRange()
        {
            int n = (int)Random.Range(1,3);
            return n;
        }

        public bool EvaluateArt(Art art)
        {
            int aestheticTotal = 0;
            int emotiveTotal = 0;

            foreach(var trait in art.Traits)
            {

            }

            return false;
        }
    }
}