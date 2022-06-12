using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public interface ITrait
    {
        public int Value { get; set; }
        public TraitType Type { get; set; }
        public bool IsKnown { get; set; }
    }
}