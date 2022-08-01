using System;

namespace Gallerist
{
    public interface ITrait
    {
        public Type Type { get; }

        public TraitType TraitType { get; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsKnown { get; set; }
    }
}