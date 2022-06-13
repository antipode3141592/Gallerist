using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class PatronTrait : ITrait
    {
        public TraitType Type { get; set; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsKnown { get; set; }
    }
}