using System;

namespace Gallerist
{
    public class ArtistTrait : ITrait
    {
        public Type Type => typeof(ArtistTrait);
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsKnown { get; set; }
    }
}