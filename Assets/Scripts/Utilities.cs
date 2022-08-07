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