using System;

namespace Gallerist
{
    public class PatronTrait : ITrait
    {
        public PatronTrait(string name, bool isKnown)
        {
            Name = name;
            Value = UnityEngine.Random.Range(-5,5);
            IsKnown = isKnown;
        }

        public Type Type => typeof(PatronTrait);
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsKnown { get; set; }
    }
}