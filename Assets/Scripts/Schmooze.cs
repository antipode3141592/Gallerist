using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class Schmooze
    {
        //reveal information about Patron
        public static void Chat(Patron patron)
        {
            //reveal 1-3 random unknown traits
            for (int i = 0; i < Random.Range(1, 4); i++)
            {
                patron.RevealTrait();
            }
        }

        public static void Introduce(Artist artist, Patron patron)
        {
            List<ITrait> matchingTraits = new();

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
            switch (matchingTraits.Count) {
                case 0:
                    patron.ModifyRandomTrait(-1);
                    Debug.Log($"{artist.Name} and {patron.Name} have nothing in common and {patron.Name} becomes less interested in the contents show");
                    break;
                case 1:
                    patron.ModifyRandomMatchingTrait(matchingTraits, +1);
                    Debug.Log($"{artist.Name} and {patron.Name} have a little in common and {patron.Name} now seems more interested in the show.");
                    break;
                case 2:
                    patron.ModifyRandomMatchingTrait(matchingTraits, +2);
                    Debug.Log($"{artist.Name} and {patron.Name} have a few interests in common and {patron.Name} now seems much more interested in the show.");
                    break;
                case 3:
                    patron.ModifyRandomMatchingTrait(matchingTraits, +3);
                    Debug.Log($"{artist.Name} and {patron.Name} have a little in common and {patron.Name} now seems more interested in the show.");
                    break;
                default:
                    Debug.LogWarning($"case of matchingTraits == {matchingTraits} not found!");
                    break;
            }
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