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

        public int TotalSchmoozingTime;    //Schmooze for sixty minutes
        public int ChatTime;
        public int IntroductionTime;
        public int NudgeTime;

        public int TotalMonths;
        public int StartingRenown;

        public static int ArtPieceGenerationCalc(Artist artist)
        {
            int x = 8 + artist.Experience * 2;
            return Random.Range(x , x + artist.Experience + 2);
        }

    }
}