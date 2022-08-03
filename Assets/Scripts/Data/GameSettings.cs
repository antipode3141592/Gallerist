using UnityEngine;

[CreateAssetMenu(menuName = "GameSettings")]
public class GameSettings : ScriptableObject
{
    public int PatronThresholdLowerValue;
    public int PatronThresholdUpperValue;

    public int PatronBoredomLower;
    public int PatronBoredomUpper;
}
