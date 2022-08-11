using System.Collections.Generic;

namespace Gallerist.Data
{
    public class TraitLevelDescriptions
    {
        public static Dictionary<int, string> LevelDescriptions = new()
        {
            { -5, "Despise" },
            { -4, "Hate" },
            { -3, "Loathe" },
            { -2, "Reject" },
            { -1, "Dislike" },
            { 0, "Ignore" },
            { 1, "Like" },
            { 2, "Enjoy" },
            { 3, "Cherish" },
            { 4, "Adore" },
            { 5, "Love" }
        };

        public static string GetDescription(int level)
        {
            if (LevelDescriptions.ContainsKey(level))
                return LevelDescriptions[level];
            return "";
        }
    }
}