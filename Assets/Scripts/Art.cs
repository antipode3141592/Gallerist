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
        public IList<ITrait> Qualities { get; private set; } 

        public Art(string name, string description, string artistId, IList<ITrait> qualities)
        {
            
            Name = name;
            Description = description;
            ArtistId = artistId;
            Qualities = qualities;
        }
    }
}