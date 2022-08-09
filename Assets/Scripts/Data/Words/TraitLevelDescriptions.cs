using System.Collections.Generic;
using UnityEngine;

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

    public class ArtTraitLevelDescriptions
    {
        public static Dictionary<int, List<string>> LevelDescriptions = new()
        {
            { 1, new() { "Exhibits", "Incorporates" , "Demonstrates"} },
            { 2, new() { "Emanates", "Expresses", "Illustrates"} },
            { 3, new() { "Exudes", "Exemplifies", "Typifies" } },
            { 4, new() { "Manifests" , "Concretizes", "Realizes"} },
            { 5, new() { "Embodies" , "Reifies", "Epitomizes" } }
        };

        public static string GetDescription(int level)
        {
            if (LevelDescriptions.ContainsKey(level))
                return LevelDescriptions[level][Random.Range(0, LevelDescriptions[level].Count)];
            return "";
        }
    }

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

    public class ArtistExperienceLevelDescription
    {
        public static Dictionary<int, string> LevelDescriptions = new()
        {
            { 0, "Inexperienced"},
            { 1, "Capable"},
            { 2, "Experienced"},
            { 3, "Professional"},
            { 4, "Seasoned"},
            { 5, "Local Legend"}
        };

        public static string GetDesciption(int level)
        {
            if (LevelDescriptions.ContainsKey(level))
                return LevelDescriptions[level];
            return "";
        }

    }

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