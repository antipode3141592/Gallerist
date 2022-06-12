using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Artist
    {
        public string Name { get; private set; }
        public string Id { get; private set; }

        public IList<ITrait> FavoredTraits { get; private set; }

        public Artist(string name, string id, IList<ITrait> favoredTraits)
        {
            Name = name;
            Id = id;
            FavoredTraits = favoredTraits;
        }
    }
}