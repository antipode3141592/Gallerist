using System.Collections.Generic;

namespace Gallerist.Data
{
    public class TraitLevelDescriptions: ILevelDescription
    {
        public static Dictionary<int, string> LevelDescriptions = new()
        {
            { -5, "Despise" },
            { -4, "Hate" },
            { -3, "Loathe" },
            { -2, "Reject" },
            { -1, "Dislike" },
            { 0, "Am ambivalent toward" },
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

    public class ArtTraitLevelDescriptions: ILevelDescription
    {
        public static Dictionary<int, string> LevelDescriptions = new()
        {
            { 1, "Exhibits" },
            { 2, "Emanates" },
            { 3, "Exudes" },
            { 4, "Manifests" },
            { 5, "Embodies" }
        };

        public static string GetDescription(int level)
        {
            if (LevelDescriptions.ContainsKey(level))
                return LevelDescriptions[level];
            return "";
        }
    }

    public class RenownLevelDescriptions: ILevelDescription
    {
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

    public interface ILevelDescription
    {
        public static Dictionary<int, string> LevelDescriptions;
    }
}