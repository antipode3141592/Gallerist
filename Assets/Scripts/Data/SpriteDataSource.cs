using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gallerist
{
    public class SpriteDataSource : MonoBehaviour
    {
        public List<Sprite> PatronPortaits { get; set; }
        public List<Sprite> ArtSprites { get; set; }
        Bag ArtSpriteBag;
        Bag PatronPortraitsBag;

        void Awake()
        {
            PatronPortaits = new List<Sprite>();
            ArtSprites = new List<Sprite>();
            

            var _portraits = Resources.LoadAll<Sprite>("PatronSprites").ToList<Sprite>();
            var _artSprites = Resources.LoadAll<Sprite>("ArtSprites/Gallerist - Art").ToList<Sprite>();

            PatronPortaits.AddRange(_portraits.Where<Sprite>(x => x.name.Contains("_0")));
            ArtSprites.AddRange(_artSprites);

            ArtSpriteBag = new Bag(ArtSprites.Count);
            PatronPortraitsBag = new Bag(PatronPortaits.Count);

            Debug.Log($"ArtSprites count: {ArtSprites.Count}");
            Debug.Log($"PatronPortraits count: {PatronPortaits.Count}");
        }

        public Sprite GeneratePortrait()
        {
            return PatronPortaits[PatronPortraitsBag.DrawFromBag()];
        }

        public Sprite GenerateArtImage()
        {
            return ArtSprites[ArtSpriteBag.DrawFromBag()];
        }
    }
}