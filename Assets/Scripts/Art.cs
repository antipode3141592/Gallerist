using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Art
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ArtistName { get; private set; }
        public List<ITrait> AestheticTraits { get; private set; } 
        public List<ITrait> EmotiveTraits { get; private set; }
        public Sprite Image { get; set; }

        public Art(string name, string description, string artistName, List<ITrait> aestheticQualities, List<ITrait> emotiveQualities, Sprite image)
        {
            
            Name = name;
            Description = description;
            ArtistName = artistName;
            AestheticTraits = aestheticQualities;
            EmotiveTraits = emotiveQualities;
            Image = image;
        }
    }
}