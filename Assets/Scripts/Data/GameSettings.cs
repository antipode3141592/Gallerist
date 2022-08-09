using UnityEngine;

namespace Gallerist.Data
{
    [CreateAssetMenu(menuName = "GameSettings")]
    public class GameSettings : ScriptableObject
    {
        public int PatronThresholdLowerValue;
        public int PatronThresholdUpperValue;

        public int PatronBoredomLower;
        public int PatronBoredomUpper;

        public static int ArtPieceGenerationCalc(Artist artist)
        {
            int x = 8 + artist.Experience * 2;
            return Random.Range(x , x + artist.Experience + 2);
        }

    }
}