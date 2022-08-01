using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Gallerist.Data;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class Schmooze
    {
        //reveal information about Patron
        public static List<ITrait> Chat(Patron patron)
        {
            List<ITrait> result = new List<ITrait>();
            //reveal 1-3 random unknown traits
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                result.Add(patron.RevealTrait());
            }
            return result;
        }

        public static string Introduce(Artist artist, Patron patron)
        {
            List<ITrait> matchingTraits = new();
            string resultsText = "";

            foreach(var trait in patron.AestheticTraits)
            {
                var _trait = artist.FavoredAestheticTraits.FirstOrDefault(x => x.Name == trait.Name);
                if (_trait is not null)
                    matchingTraits.Add(_trait);
            }
            foreach (var trait in patron.EmotiveTraits)
            {
                var _trait = artist.FavoredEmotiveTraits.FirstOrDefault(x => x.Name == trait.Name);
                if (_trait is not null)
                    matchingTraits.Add(_trait);
            }

            if (Debug.isDebugBuild)
                Debug.Log($"There were {matchingTraits.Count} matching traits.");
            ITrait modifiedTrait;
            switch (matchingTraits.Count) {
                case 0:
                    patron.ModifyRandomTrait(-1);

                    resultsText = $"{artist.Name} and {patron.Name} have nothing in common and {patron.Name} becomes less interested in the contents show";
                    break;
                case 1:
                    modifiedTrait = patron.ModifyRandomMatchingTrait(matchingTraits, +1);
                    resultsText = $"{artist.Name} and {patron.Name} both have an interest in {modifiedTrait.Name} and their conversation has increased {patron.Name}\'s appreciation in works which have that quality.";
                    break;
                case 2:
                    modifiedTrait = patron.ModifyRandomMatchingTrait(matchingTraits, +2);
                    resultsText = $"{artist.Name} and {patron.Name} converse about their interests in {matchingTraits[0].Name} and {matchingTraits[1].Name}, which deepens {patron.Name}\'s preference in {modifiedTrait.Name} to {TraitLevelDescriptions.GetDescription(modifiedTrait.Value)}.";
                    break;
                case 3:
                    modifiedTrait = patron.ModifyRandomMatchingTrait(matchingTraits, +3);
                    resultsText = $"{artist.Name} and {patron.Name} have a lot in common and their conversation about {matchingTraits[0]}, {matchingTraits[1].Name}, and {matchingTraits[2].Name} qualities in art has a profound affect on {patron.Name} and they now {TraitLevelDescriptions.GetDescription(modifiedTrait.Value)} {modifiedTrait.Name}.";
                    break;
                default:
                    Debug.LogWarning($"case of matchingTraits == {matchingTraits} not found!");
                    break;
            }

            return resultsText;
        }

        public static void Nudge(Patron patron)
        {
            if (Utilities.RandomBool()) 
            {
                patron.EmotiveThreshold--;
            } else
            {
                patron.AestheticThreshold--;            }

        }
    }
}