using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public interface ITrait
    {
        public Type Type { get; }
        public string Name { get; set; }
        public int Value { get; set; }
        public bool IsKnown { get; set; }
    }
}