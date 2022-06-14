using System;

namespace Gallerist
{
    public class ArtTrait : ITrait
    {
        public ArtTrait(string name, bool isKnown, TraitType traitType)
        {
            Name = name;
            Value = UnityEngine.Random.Range(1, 5);
            IsKnown = isKnown;
            TraitType = traitType;
        }

        public Type Type => typeof(ArtTrait);
        public TraitType TraitType { get; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsKnown { get; set; }
    }
}