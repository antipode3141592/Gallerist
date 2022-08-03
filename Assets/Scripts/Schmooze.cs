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
                ITrait _trait = patron.RevealTrait();
                if (_trait is not null)
                    result.Add(_trait);
                else
                    return null;
            }
            return result;
        }

        public static ResultsArgs Introduce(Artist artist, Patron patron)
        {
            List<ITrait> matchingTraits = new();
            string resultsText = "";
            string summaryText = $"";
            bool distaste = false;

            foreach (var trait in patron.AestheticTraits)
            {
                var _trait = artist.FavoredAestheticTraits.FirstOrDefault(x => x.Name == trait.Name);
                if (_trait is not null)
                {
                    distaste = trait.Value < 0;
                    matchingTraits.Add(trait);
                }
            }
            foreach (var trait in patron.EmotiveTraits)
            {
                var _trait = artist.FavoredEmotiveTraits.FirstOrDefault(x => x.Name == trait.Name);
                if (_trait is not null)
                {
                    distaste = trait.Value < 0;
                    matchingTraits.Add(trait);
                }
            }

            if (Debug.isDebugBuild)
                Debug.Log($"There were {matchingTraits.Count} matching traits.");
            ITrait modifiedTrait;

            if (distaste && matchingTraits.Count >= 1)
            {
                modifiedTrait = patron.ModifyRandomMatchingTrait(matchingTraits, -3);
                resultsText = $"{patron.Name} has an aversion to {modifiedTrait.Name} and their conversation with {artist.Name} has decreased {patron.FirstName}\'s opinion of {modifiedTrait.Name} to {TraitLevelDescriptions.GetDescription(modifiedTrait.Value)}.";
                summaryText = $"{modifiedTrait.Name} decreased";
            }
            else if (matchingTraits.Count == 0) {
                modifiedTrait = patron.ModifyRandomTrait(-1);
                resultsText = $"{artist.Name} and {patron.Name} have nothing in common and {patron.Name} becomes less interested in the contents show";
                summaryText = $"{modifiedTrait.Name} decreased";
            }
            else if (matchingTraits.Count == 1) {
                modifiedTrait = patron.ModifyRandomMatchingTrait(matchingTraits, +1);
                resultsText = $"{artist.Name} and {patron.Name} both have an interest in {modifiedTrait.Name} and their conversation has increased {patron.FirstName}\'s appreciation in works which have that quality.";
                summaryText = $"{modifiedTrait.Name} somewhat increased";
            }
            else if (matchingTraits.Count == 2)
            {
                modifiedTrait = patron.ModifyRandomMatchingTrait(matchingTraits, +2);
                resultsText = $"{artist.Name} and {patron.Name} converse about their interests in {matchingTraits[0].Name} and {matchingTraits[1].Name}, which deepens {patron.FirstName}\'s preference in {modifiedTrait.Name} to {TraitLevelDescriptions.GetDescription(modifiedTrait.Value)}.";
                summaryText = $"{modifiedTrait.Name} increased";
            }
            else if (matchingTraits.Count >= 3)
            { 
                modifiedTrait = patron.ModifyRandomMatchingTrait(matchingTraits, +3);
                resultsText = $"{artist.Name} and {patron.Name} have a lot in common and their conversation about {matchingTraits[0]}, {matchingTraits[1].Name}, and {matchingTraits[2].Name} qualities in art has a profound affect on {patron.FirstName} and they now {TraitLevelDescriptions.GetDescription(modifiedTrait.Value)} {modifiedTrait.Name}.";
                summaryText = $"{modifiedTrait.Name} greatly increased";
            }
            else 
            { 
                Debug.LogWarning($"case of matchingTraits == {matchingTraits} not found!");
            }
            

            return new ResultsArgs(resultsText, summaryText);
        }

        public static ResultsArgs Nudge(Patron patron)
        {
            if (Utilities.RandomBool()) 
            {
                patron.EmotiveThreshold--;
                return new ResultsArgs($"{patron.Name} is now more open to purchasing art with a wider range of emotive qualities.", $"-1 Emotive Threshold");
            } else
            {
                patron.AestheticThreshold--;
                return new ResultsArgs($"{patron.Name} is now more open to purchasing art with a wider range of aesthetic qualities.", $"-1 Aesthetic Threshold");
            }
            
        }
    }
}