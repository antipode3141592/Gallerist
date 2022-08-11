using System.Collections.Generic;

namespace Gallerist.Data
{
    public class RenownLevelDescriptions
    {

        //"A [adf] 
        public static Dictionary<int, string> LevelDescriptions = new()
        {
            { -5, "Relic" },
            { -4, "Has-Been" },
            { -3, "Irrelevant" },
            { -2, "Boring" },
            { -1, "Unfashionable" },
            { 0, "Newcomer" },
            { 1, "Promising" },
            { 2, "Reputable" },
            { 3, "Fashionable" },
            { 4, "Trend-Setting" },
            { 5, "Legendary" }
        };

        public static string GetDescription(int level)
        {
            if (LevelDescriptions.ContainsKey(level))
                return LevelDescriptions[level];
            return "";
        }
    }
}