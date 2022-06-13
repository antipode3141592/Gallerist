using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Patron
    {
        public Patron(string name, bool isSubscriber, IList<string> acquisitions, IList<ITrait> preferences)
        {
            Name = name;
            IsSubscriber = isSubscriber;
            Acquisitions = acquisitions;
            Preferences = preferences;
            PerceptionRange = SetPerceptionRange();
        }

        public string Name { get; private set; }
        public string Id { get; private set; }
        public bool IsSubscriber { get; private set; }
        public IList<string> Acquisitions { get; private set; }
        public IList<ITrait> Preferences { get; private set; }
        public int PerceptionRange { get; private set; }

        int SetPerceptionRange()
        {
            int n = (int)Random.Range(1,3);
            return n;
        }
    }
}