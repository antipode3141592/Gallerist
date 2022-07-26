using System;

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
        public int Value { get; set; }
        public bool IsKnown { get; set; }
    }
}