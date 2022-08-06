using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Artist: IThumbnail
    {
        public string Name { get; private set; }
        public Sprite Image { get; private set; }

        public List<ITrait> FavoredAestheticTraits { get; private set; }
        public List<ITrait> FavoredEmotiveTraits { get; private set; }

        public int Experience { get; set; } = 0;

        public Artist(string name, List<ITrait> favoredAestheticTraits, List<ITrait> favoredEmotiveTraits, Sprite portrait)
        {
            Name = name;
            FavoredAestheticTraits = favoredAestheticTraits;
            FavoredEmotiveTraits = favoredEmotiveTraits;
            Image = portrait;
        }
    }
}