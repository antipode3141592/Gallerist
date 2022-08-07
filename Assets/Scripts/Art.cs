using Gallerist.Events;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class Art : IThumbnail, IModifyTrait
    {
        public string Name { get; private set; }
        public Sprite Image { get; private set; }
        public string Description { get; private set; }
        public string ArtistName { get; private set; }
        public bool IsSold { get; set; } = false;
        public int PrintsSold { get; set; } = 0;

        public List<ITrait> AestheticTraits { get; private set; }
        public List<ITrait> EmotiveTraits { get; private set; }

        public event EventHandler<TraitModified> TraitModified;

        public Art(string name, string description, string artistName, List<ITrait> aestheticQualities, List<ITrait> emotiveQualities, Sprite image)
        {

            Name = name;
            Description = description;
            ArtistName = artistName;
            AestheticTraits = aestheticQualities;
            EmotiveTraits = emotiveQualities;
            Image = image;
        }

        public bool ModifyTrait(string traitName, int modifier)
        {
            ITrait aestheticTrait = AestheticTraits.Find(x => x.Name == traitName);
            if (aestheticTrait is not null)
            {
                aestheticTrait.Value += modifier;
                TraitModified?.Invoke(this, new TraitModified(aestheticTrait.Name, modifier));
                return true;
            }
            ITrait emotiveTrait = EmotiveTraits.Find(x => x.Name == traitName);
            if (emotiveTrait is not null)
            {
                emotiveTrait.Value += modifier;
                TraitModified?.Invoke(this, new TraitModified(emotiveTrait.Name, modifier));
                return true;
            }
            return false;
        }
    }
}