using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Artist
    {
        public string Name { get; private set; }
        public Sprite Portrait { get; private set; }

        public IList<ITrait> FavoredAestheticTraits { get; private set; }
        public IList<ITrait> FavoredEmotiveTraits { get; private set; }

        public Artist(string name, IList<ITrait> favoredAestheticTraits, IList<ITrait> favoredEmotiveTraits, Sprite portrait)
        {
            Name = name;
            FavoredAestheticTraits = favoredAestheticTraits;
            FavoredEmotiveTraits = favoredEmotiveTraits;
            Portrait = portrait;
        }
    }
}