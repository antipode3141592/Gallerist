using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Art
    {
        public string Name { get; private set; }
        public string Description { get; private set; }
        public string ArtistId { get; private set; }
        public List<ITrait> AestheticTraits { get; private set; } 
        public List<ITrait> EmotiveTraits { get; private set; }

        public Art(string name, string description, string artistId, List<ITrait> aestheticQualities, List<ITrait> emotiveQualities)
        {
            
            Name = name;
            Description = description;
            ArtistId = artistId;
            AestheticTraits = aestheticQualities;
            EmotiveTraits = emotiveQualities;
        }
    }
}