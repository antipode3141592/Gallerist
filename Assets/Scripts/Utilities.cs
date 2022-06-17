using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public static class Utilities
    {
        public static bool RandomBool()
        {
            return Random.value < 0.5f;
        }
    }
}