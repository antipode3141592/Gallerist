using System.Collections.Generic;

namespace Gallerist.Data
{
    public class PatronSatisfactionLevelDescription
    {
        public static Dictionary<int, string> LevelDescriptions = new()
        {
            { -2, $""},
            { -1, $""},
            { 0, $"Uninterested" },
            { 1, $"" },
            { 2, $"" },
            { 3, $"" },
            { 4, $"" },
            { 5, $"" }
        };

    }
}