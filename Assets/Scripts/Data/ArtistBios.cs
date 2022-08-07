using Random = UnityEngine.Random;
using System.Collections.Generic;
using System.Linq;

namespace Gallerist.Data
{
    public class ArtistBios
    {
        public static Dictionary<string, string> Bios = new()
        {
            { "default", "Lorem ipsum dolor sit amet, consectetur adipiscing elit, sed do eiusmod tempor incididunt ut labore et dolore magna aliqua. Ut enim ad minim veniam, quis nostrud." },
            { "artist1", "Hailing from the small town of [SmallTown], [State], [ArtistName] aims to reimagine the way [HighestAestheticTrait] arts are perceived in [TownName]."},
            { "artist2", "From an early age, [ArtistName] was inclined to evoke [HighestEmotiveTrait] with their art.  "},
            { "artist3", "" },
            { "artist4", ""}
        };

        public static string GetRandomBio() 
        {
            List<string> keys = Bios.Keys.ToList();
            return Bios[key: keys[Random.Range(0,keys.Count)]];
        }
    }
}