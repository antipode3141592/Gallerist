using System.Collections.Generic;

namespace Gallerist.Data
{
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
}