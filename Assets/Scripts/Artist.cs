using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gallerist
{
    public class Artist
    {
        public string Name { get; private set; }
        public Sprite Portrait { get; private set; }

        public List<ITrait> FavoredAestheticTraits { get; private set; }
        public List<ITrait> FavoredEmotiveTraits { get; private set; }

        public Artist(string name, List<ITrait> favoredAestheticTraits, List<ITrait> favoredEmotiveTraits, Sprite portrait)
        {
            Name = name;
            FavoredAestheticTraits = favoredAestheticTraits;
            FavoredEmotiveTraits = favoredEmotiveTraits;
            Portrait = portrait;
        }
    }
}