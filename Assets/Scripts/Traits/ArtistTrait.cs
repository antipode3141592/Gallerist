using System;

namespace Gallerist
{
    public class ArtistTrait : ITrait
    {
        public ArtistTrait(string name, bool isKnown, TraitType traitType)
        {
            TraitType = traitType;
            Name = name;
            Value = UnityEngine.Random.Range(1, 5);
            IsKnown = isKnown;
        }

        public Type Type => typeof(ArtistTrait);
        public TraitType TraitType { get; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsKnown { get; set; }
    }
}