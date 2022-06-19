using UnityEngine;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class Schmooze
    {
        

        //reveal information about Patron
        public static void Chat(Patron patron)
        {
            patron.RevealTraits(Random.Range(1, 4));    //reveal 1-3 random unknown traits
            //update Patron Card
        }

        public static void Introduce(Artist artist, Patron patron)
        {
            int matchingTraits = 0;

            foreach(var trait in patron.AestheticTraits)
            {
                if (artist.FavoredAestheticTraits.Contains(trait))
                    matchingTraits++;
            }
            foreach (var trait in patron.EmotiveTraits)
            {
                if (artist.FavoredEmotiveTraits.Contains(trait))
                    matchingTraits++;
            }

            switch (matchingTraits) {
                case 0:
                    Debug.Log($"{artist.Name} and {patron.Name} have nothing in common and {patron.Name} becomes less interested in the contents show");
                    break;
                case 1:
                    Debug.Log($"{artist.Name} and {patron.Name} have a little in common and {patron.Name} now seems more interested in the show.");
                    break;
                case 2:
                    Debug.Log($"{artist.Name} and {patron.Name} have a few interests in common and {patron.Name} now seems much more interested in the show.");
                    break;
                case 3:
                    Debug.Log($"{artist.Name} and {patron.Name} have a little in common and {patron.Name} now seems more interested in the show.");
                    break;
                default:
                    Debug.LogWarning($"case of matchingTraits == {matchingTraits} not found!");
                    break;
            }
        }

        public static void Nudge(Patron patron)
        {
        }
    }
}