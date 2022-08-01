using System;
using UnityEngine;

namespace Gallerist
{
    public class ArtistTrait : ITrait
    {
        public ArtistTrait(string name,  int value, bool isKnown, TraitType traitType)
        {
            TraitType = traitType;
            Name = name;
            Value = value;
            IsKnown = isKnown;
        }

        public Type Type => typeof(ArtistTrait);
        public TraitType TraitType { get; }
        public string Name { get; set; }
        public int Value
        {
            get
            {
                return _value;
            }
            set
            {
                _value = Mathf.Clamp(value, 0, 5);
            }
        }
        int _value;
        public bool IsKnown { get; set; }
    }
}