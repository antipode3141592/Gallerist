using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Artist
    {
        public string Name { get; private set; }
        public string Id { get; private set; }

        public IList<ITrait> FavoredAestheticTraits { get; private set; }
        public IList<ITrait> FavoredEmotiveTraits { get; private set; }

        public Artist(string name, string id, IList<ITrait> favoredAestheticTraits, IList<ITrait> favoredEmotiveTraits)
        {
            Name = name;
            Id = id;
            FavoredAestheticTraits = favoredAestheticTraits;
            FavoredEmotiveTraits = favoredEmotiveTraits;
        }
    }
}