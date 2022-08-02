namespace Gallerist.Data
{
    public class ArtAcquisition
    {
        public ArtAcquisition(Art art, bool isOriginal, int purchaseDate)
        {
            Art = art;
            IsOriginal = isOriginal;
            PurchaseDate = purchaseDate;
        }

        public Art Art { get; set; }
        public bool IsOriginal { get; set; }
        public int PurchaseDate { get; set; }
    }
}
