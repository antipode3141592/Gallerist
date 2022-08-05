namespace Gallerist
{
    public static class PluralHelpers
    {
        public static string PluralS(int value)
        {
            return value == 1 ? "" : "s";
        }

        public static string WasWere(int value)
        {
            return value <= 1 ? "was" : "were";
        }

        public static string HasHave(int value)
        {
            return value <= 1 ? "has" : "have";
        }
    }
}