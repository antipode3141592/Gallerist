using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Gallerist.UI;

namespace Gallerist
{
    public class GameManager : MonoBehaviour
    {
        [SerializeField] ArtCard artCard;
        [SerializeField] PatronCard patronCard;

        public Artist Artist { get; set; }
        public List<Art> ArtPieces { get; set; }
        public List<Patron> Patrons { get; set; }
        public List<Sprite> PatronPortaits { get; set; }
        public List<Sprite> ArtSprites { get; set; }
        public List<string> AestheticTraits { get; set; }
        public List<string> EmotiveTraits { get; set; }
        public List<string> FirstNames { get; set; }
        public List<string> LastNames { get; set; }

        PatronsDisplay _patronsDisplay;
        ArtPiecesDisplay _artPiecesDisplay;

        private void Awake()
        {
            _patronsDisplay = FindObjectOfType<PatronsDisplay>();
            _artPiecesDisplay = FindObjectOfType<ArtPiecesDisplay>();
            ArtPieces = new List<Art>();
            Patrons = new List<Patron>();
            PatronPortaits = new List<Sprite>();
            ArtSprites = new List<Sprite>();
            AestheticTraits = new ();
            EmotiveTraits = new ();
            FirstNames = new ();
            LastNames = new ();
        }

        void Start()
        {
            //load trait lists
            var _aestheticWords = Resources.Load<TextAsset>("Gallerist - Aesthetics");
            var _emotiveWords = Resources.Load<TextAsset>("Gallerist - Emotive");
            var _firstNames = Resources.Load<TextAsset>("Gallerist - FirstNames");
            var _lastNames = Resources.Load<TextAsset>("Gallerist - LastNames");
            var _portraits = Resources.LoadAll<Sprite>("PatronSprites").ToList<Sprite>();
            var _artSprites = Resources.LoadAll<Sprite>("ArtSprites/Gallerist - Art").ToList<Sprite>();

            AestheticTraits.AddRange(_aestheticWords.text.Split(',', '\n').ToList());
            EmotiveTraits.AddRange(_emotiveWords.text.Split(',', '\n').ToList());
            FirstNames.AddRange(_firstNames.text.Split(',', '\n').ToList());
            LastNames.AddRange(_lastNames.text.Split(',', '\n').ToList());
            PatronPortaits.AddRange(_portraits.Where<Sprite>(x=> x.name.Contains("_0")));
            ArtSprites.AddRange(_artSprites);

            Debug.Log($"AT count : {AestheticTraits.Count}, ET count : {EmotiveTraits.Count}");
            Debug.Log($"FirstNames count: {FirstNames.Count}");
            Debug.Log($"LastNames count: {LastNames.Count}");
            Debug.Log($"ArtSprites count: {ArtSprites.Count}");
            Debug.Log($"PatronPortraits count: {PatronPortaits.Count}");

            GenerateArtist();

            GenerateArts(10);
            
            DebugArt();

            GeneratePatrons(20);

            DebugPatron();

            _patronsDisplay.SetPatrons();
            _artPiecesDisplay.SetThumbnails();
        }

        void GenerateArtist()
        {
            Artist = new Artist(
                name: GenerateRandomArtistName(), 
                favoredAestheticTraits: GenerateAestheticTraits(3,typeof(ArtistTrait)),
                favoredEmotiveTraits: GenerateEmotiveTraits(3,typeof(ArtistTrait)),
                portrait: GeneratePortrait()
                );
        }

        void GenerateArts(int total)
        {
            for(int i = 0; i < total; i++)
            {
                GenerateArt();
            }
        }

        void GenerateArt()
        {
            //create piece of art
            var newArt = new Art(
                name: GenerateArtName(), 
                description: GenerateArtDescription(), 
                artistName: Artist.Name, 
                aestheticQualities: GenerateAestheticTraits(3, typeof(ArtTrait)),
                emotiveQualities: GenerateEmotiveTraits(3, typeof(ArtTrait)),
                image: GenerateArtImage()
                );
            //stats are based on Artist favoredTraits (ex: artist specializing in landscapes will tend to create landscapes)

            //add art to ArtPieces list
            ArtPieces.Add(newArt);
        }

        string GenerateArtName()
        {
            return ($"RandomName_{UnityEngine.Random.Range(0,1000)}");
        }

        string GenerateArtDescription()
        {
            return ($"RandomDescription_{UnityEngine.Random.Range(0, 1000)}");
        }

        void GeneratePatrons(int total)
        {
            for (int i = 0; i < total; i++)
                GeneratePatron();
        }

        void GeneratePatron()
        {
            var newPatron = new Patron(
                name: GenerateRandomPatronName(),
                portrait: GeneratePortrait(),
                isSubscriber: false, 
                aestheticTraits: GenerateAestheticTraits(5, typeof(PatronTrait)), 
                emotiveTraits: GenerateEmotiveTraits(5, typeof(PatronTrait)),
                acquisitions: new List<string>(), 
                aestheticThreshold: UnityEngine.Random.Range(8,12), 
                emotiveThreshold: UnityEngine.Random.Range(8, 12));
            //  add a check for duplicates before adding to Patrons list
            Patrons.Add(newPatron);
        }

        Sprite GeneratePortrait()
        {
            int randomIndex = UnityEngine.Random.Range(0, PatronPortaits.Count);
            return PatronPortaits[randomIndex];
        }

        Sprite GenerateArtImage()
        {
            int randomIndex = UnityEngine.Random.Range(0, ArtSprites.Count);
            return ArtSprites[randomIndex];
        }

        string GenerateRandomPatronName()
        {
            int randomIndex = UnityEngine.Random.Range(0, FirstNames.Count);
            return FirstNames[randomIndex].Trim() + " " + RandomLastNameLetter();
        }

        string GenerateRandomArtistName()
        {
            int randomIndex1 = UnityEngine.Random.Range(0, FirstNames.Count);
            int randomIndex2 = UnityEngine.Random.Range(0, LastNames.Count);
            return FirstNames[randomIndex1].Trim() + " " + RandomLastNameLetter() + " " + LastNames[randomIndex2].Trim();
        }

        string RandomLastNameLetter()
        {
            int randomIndex = UnityEngine.Random.Range(65, 90); //https://www.dotnetperls.com/ascii-table
            return $"{(char)randomIndex}.";
        }
        
        string GetRandomTraitName(List<string> traitList)
        {
            int randomIndex = UnityEngine.Random.Range(0, traitList.Count);
            return traitList[randomIndex].Trim();
        }

        List<ITrait> GenerateAestheticTraits(int totalTraits, Type traitType)
        {
            List<ITrait> traits = new();

            for (int i = 0; i < totalTraits; i++)
            {
                if (traitType == typeof(ArtTrait))
                {
                    traits.Add(new ArtTrait(GetRandomTraitName(AestheticTraits), true, TraitType.Aesthetic));
                } else if (traitType == typeof(ArtistTrait)){
                    traits.Add(new ArtistTrait(GetRandomTraitName(AestheticTraits), true, TraitType.Aesthetic));
                }
                else if (traitType == typeof(PatronTrait))
                {
                    traits.Add(new PatronTrait(GetRandomTraitName(AestheticTraits), Utilities.RandomBool(), TraitType.Aesthetic));
                } else
                {
                    return null;
                }
            }
            return traits;
        }

        List<ITrait> GenerateEmotiveTraits(int totalTraits, Type traitType)
        {
            List<ITrait> traits = new();
            for (int i = 0; i < totalTraits; i++)
            {
                if (traitType == typeof(ArtTrait))
                {
                    traits.Add(new ArtTrait(GetRandomTraitName(EmotiveTraits), true, TraitType.Emotive));
                }
                else if (traitType == typeof(ArtistTrait))
                {
                    traits.Add(new ArtistTrait(GetRandomTraitName(EmotiveTraits), true, TraitType.Emotive));
                }
                else if (traitType == typeof(PatronTrait))
                {
                    traits.Add(new PatronTrait(GetRandomTraitName(EmotiveTraits), Utilities.RandomBool(), TraitType.Emotive));
                }
                else
                {
                    return null;
                }
            }
            return traits;
        }

        #region Debugging
        void DebugArt()
        {
            foreach (var art in ArtPieces)
            {
                Debug.Log($"Art:  {art.Name} , {art.Description}");
                foreach (var trait in art.AestheticTraits)
                {
                    string _known = trait.IsKnown ? "Is Known" : "Is Not Known";
                    Debug.Log($"    [{trait.Type.Name}, {trait.TraitType}] {trait.Name} {trait.Value} ({_known})");
                }
                foreach (var trait in art.EmotiveTraits)
                {
                    string _known = trait.IsKnown ? "Is Known" : "Is Not Known";
                    Debug.Log($"    [{trait.Type.Name}, {trait.TraitType}] {trait.Name} {trait.Value} ({_known})");
                }
            }
        }

        void DebugArtist()
        {

        }

        void DebugPatron()
        {
            foreach (var patron in Patrons)
            {
                Debug.Log($"Patron:  {patron.Name}, Pr: {patron.PerceptionRange}, Sprite: {patron.Portrait.name}");
                foreach (var trait in patron.AestheticTraits)
                {
                    string _known = trait.IsKnown ? "Is Known" : "Is Not Known";
                    Debug.Log($"    [{trait.Type.Name}, {trait.TraitType}] {trait.Name} {trait.Value} ({_known})");
                }
                foreach (var trait in patron.EmotiveTraits)
                {
                    string _known = trait.IsKnown ? "Is Known" : "Is Not Known";
                    Debug.Log($"    [{trait.Type.Name}, {trait.TraitType}] {trait.Name} {trait.Value} ({_known})");
                }
            }
        }
        #endregion
    }
}