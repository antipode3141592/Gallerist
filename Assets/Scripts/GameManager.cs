using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Gallerist.UI;
using TMPro;

namespace Gallerist
{
    public class GameManager : MonoBehaviour
    {

        [SerializeField] ArtCard artCard;
        [SerializeField] PatronCard patronCard;
        [SerializeField] TextMeshProUGUI evaluationsText;
        [SerializeField] TextMeshProUGUI evaluationResultsText;

        [SerializeField] int maximumEvaluations = 5;

        [SerializeField] GameObject ArtPiecesPanel;
        [SerializeField] GameObject PatronsPanel;

        int totalEvaluations = 0;
        int originalsSold = 0;
        int printsSold = 0;

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

        GameStates currentGameState = GameStates.Start;
        public event EventHandler<GameStates> GameStateChanged;

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
            PatronPortaits.AddRange(_portraits.Where<Sprite>(x => x.name.Contains("_0")));
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
            UpdateEvaluationText();
            currentGameState = GameStates.Preparation;
            GameStateChanged?.Invoke(this, GameStates.Preparation);
        }

        //game states

        //Start


        //closing
        public void Evaluate()
        {
            totalEvaluations++;
            //current Patron selection evaluates current Art selection
            var result = patronCard.SelectedPatron.EvaluateArt(artCard.SelectedArt);
            switch (result)
            {
                case EvaluationResultTypes.Original:
                    Debug.Log($"Patron {patronCard.SelectedPatron.Name} loves {artCard.SelectedArt.Name} and will buy the original!");
                    originalsSold++;
                    break;
                case EvaluationResultTypes.Print:
                    Debug.Log($"Patron {patronCard.SelectedPatron.Name} likes {artCard.SelectedArt.Name} and will buy a print!");
                    printsSold++;
                    break;
                case EvaluationResultTypes.None:
                    Debug.Log($"Patron {patronCard.SelectedPatron.Name} is not particularly drawn to {artCard.SelectedArt.Name}, but will take a business card.");
                    break;
            }
            UpdateEvaluationText();
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
            List<string> traitNames = new();

            for (int i = 0; i < totalTraits; i++)
            {
                string traitName;
                do { traitName = GetRandomTraitName(AestheticTraits); }
                while (traitNames.Contains(traitName));
                traitNames.Add(traitName);

                if (traitType == typeof(ArtTrait))
                {
                    traits.Add(new ArtTrait(traitName, true, TraitType.Aesthetic));
                } else if (traitType == typeof(ArtistTrait)){
                    traits.Add(new ArtistTrait(traitName, true, TraitType.Aesthetic));
                }
                else if (traitType == typeof(PatronTrait))
                {
                    traits.Add(new PatronTrait(traitName, false, TraitType.Aesthetic));
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
            List<string> traitNames = new();
            for (int i = 0; i < totalTraits; i++)
            {
                string traitName;
                do { traitName = GetRandomTraitName(EmotiveTraits); }
                while (traitNames.Contains(traitName));
                traitNames.Add(traitName);

                if (traitType == typeof(ArtTrait))
                {
                    traits.Add(new ArtTrait(traitName, true, TraitType.Emotive));
                }
                else if (traitType == typeof(ArtistTrait))
                {
                    traits.Add(new ArtistTrait(traitName, true, TraitType.Emotive));
                }
                else if (traitType == typeof(PatronTrait))
                {
                    traits.Add(new PatronTrait(traitName, false, TraitType.Emotive));
                }
                else
                {
                    return null;
                }
            }
            return traits;
        }

        //TODO convert text update to observer model
        void UpdateEvaluationText()
        {
            evaluationsText.text = $"{totalEvaluations} of {maximumEvaluations} Evaluations";
            evaluationResultsText.text = $"Originals Sold: {originalsSold} , Prints Sold: {printsSold}";
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