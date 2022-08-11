using System.Collections.Generic;
using UnityEngine;

namespace Gallerist.Data
{
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
}