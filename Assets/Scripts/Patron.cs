using Gallerist.Data;
using Gallerist.Events;
using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class Patron : IThumbnail, IModifyTrait
    {
        public Patron(string firstName, string lastInitial, Sprite portrait, bool isSubscriber, List<ITrait> aestheticTraits, List<ITrait> emotiveTraits, List<ArtAcquisition> acquisitions, int aestheticThreshold, int emotiveThreshold, int boredomThreshold)
        {
            FirstName = firstName;
            LastInitial = lastInitial;
            Image = portrait;
            IsSubscriber = isSubscriber;
            Acquisitions = acquisitions;
            AestheticTraits = aestheticTraits;
            EmotiveTraits = emotiveTraits;
            PerceptionRange = Random.Range(-1, 2);
            AestheticThreshold = aestheticThreshold;
            EmotiveThreshold = emotiveThreshold;
            BoredomThreshold = boredomThreshold;
        }

        public Sprite Image { get; private set; }
        public string Name => $"{FirstName} {LastInitial}";
        public string FirstName { get; private set; }
        public string LastInitial { get; private set; }
        public bool IsSubscriber { get; set; }
        public List<ArtAcquisition> Acquisitions { get; private set; }
        public List<ITrait> AestheticTraits { get; private set; }
        public List<ITrait> EmotiveTraits { get; private set; }
        public int PerceptionRange { get; private set; }
        public int AestheticThreshold { get; set; }
        public int EmotiveThreshold { get; set; }
        public int BoredomThreshold { get; set; }
        public int Satisfaction { get; set; } = 0;
        public bool AllTraitsKnown { get; set; } = false;
        public bool HasMetArtist { get; set; } = false;

        public event EventHandler PreferencesUpdated;
        public event EventHandler<string> TraitRevealed;
        public event EventHandler<TraitModified> TraitModified;

        public EvaluationResults EvaluateArt(Art art, int bonus = 0)
        {
            int aestheticTotal = PerceptionRange + bonus;
            int emotiveTotal = PerceptionRange + bonus;

            EvaluationResults results = new();

            foreach (var trait in art.AestheticTraits)
            {
                aestheticTotal += AddTraitsIfMatched(trait, AestheticTraits);
            }
            foreach (var trait in art.EmotiveTraits)
            {
                emotiveTotal += AddTraitsIfMatched(trait, EmotiveTraits);
            }
            results.SatisfactionRating = Mathf.CeilToInt((aestheticTotal + emotiveTotal) / 2f);

            //if (Debug.isDebugBuild)
            //    Debug.Log($"A: {aestheticTotal}/{AestheticThreshold}, E: {emotiveTotal}/{EmotiveThreshold}, S: {results.SatisfactionRating}");
            if (aestheticTotal >= AestheticThreshold && emotiveTotal >= EmotiveThreshold)
                results.ResultType = EvaluationResultTypes.Original;
            else if (aestheticTotal >= AestheticThreshold / 2 && emotiveTotal >= EmotiveThreshold / 2)
                results.ResultType = EvaluationResultTypes.Print;
            else if (aestheticTotal >= BoredomThreshold && emotiveTotal >= BoredomThreshold)
                results.ResultType = EvaluationResultTypes.Subscribe;
            else
                results.ResultType = EvaluationResultTypes.None;
            return results;
        }

        int AddTraitsIfMatched(ITrait artTrait, List<ITrait> patronTraits)
        {
            var patronTrait = patronTraits.Find(x => x.Name == artTrait.Name);
            if (patronTrait is not null)
                return patronTrait.Value + artTrait.Value;
            return 0;
        }

        public ITrait RevealTrait()
        {
            if (AllTraitsKnown)
                return null;
            List<ITrait> unknownTraits = new();
            unknownTraits = EmotiveTraits.FindAll(x => x.IsKnown == false);
            unknownTraits.AddRange(AestheticTraits.FindAll(x => x.IsKnown == false));
            if (unknownTraits.Count == 0)
            {
                AllTraitsKnown = true;
                return null;
            }
            int randomIndex = Random.Range(0, unknownTraits.Count);
            unknownTraits[randomIndex].IsKnown = true;

            PreferencesUpdated?.Invoke(this, EventArgs.Empty);
            TraitRevealed?.Invoke(this, $"{unknownTraits[randomIndex].Name}");
            //if (Debug.isDebugBuild)
            //    Debug.Log($"{Name} has a preference for {unknownTraits[randomIndex].Name} at {unknownTraits[randomIndex].Value}");
            CheckAllTraitsRevealed();
            return unknownTraits[randomIndex];
        }

        public bool CheckAllTraitsRevealed()
        {
            List<ITrait> unknownTraits = new();
            unknownTraits = EmotiveTraits.FindAll(x => x.IsKnown == false);
            unknownTraits.AddRange(AestheticTraits.FindAll(x => x.IsKnown == false));
            if (unknownTraits.Count > 0)
                return false;
            AllTraitsKnown = true;
            return true;
        }

        public ITrait ModifyRandomTrait(int modifier)
        {
            ITrait _trait = Utilities.RandomBool() ?
                AestheticTraits[Random.Range(0, AestheticTraits.Count)] :
                EmotiveTraits[Random.Range(0, EmotiveTraits.Count)];
            //if (Debug.isDebugBuild)
            //    Debug.Log($"randomly chosen trait: {_trait.Name}");
            ModifyTrait(_trait.Name, modifier);
            return _trait;
        }

        public bool ModifyTrait(string traitName, int modifier)
        {
            var patronTrait = EmotiveTraits.FirstOrDefault(x => x.Name == traitName);
            if (patronTrait is not null)
            {
                //if (Debug.isDebugBuild)
                //    Debug.Log($"modifying {patronTrait.Name} {patronTrait.Value} by {modifier}");
                patronTrait.Value += modifier;
                PreferencesUpdated?.Invoke(this, EventArgs.Empty);
                TraitModified?.Invoke(this, new TraitModified(traitName, modifier));
                return true;
            }
            patronTrait = AestheticTraits.FirstOrDefault(x => x.Name == traitName);
            if (patronTrait is not null)
            {
                //if (Debug.isDebugBuild)
                //    Debug.Log($"modifying {patronTrait.Name} {patronTrait.Value} by {modifier}");
                patronTrait.Value += modifier;
                PreferencesUpdated?.Invoke(this, EventArgs.Empty);
                TraitModified?.Invoke(this, new TraitModified(traitName, modifier));
                return true;
            }
            if (Debug.isDebugBuild)
                Debug.Log($"{traitName} not found, no modifications made");
            return false;
        }

        public ITrait ModifyRandomMatchingTrait(List<ITrait> traits, int modifier)
        {
            var trait = traits[Random.Range(0, traits.Count)];
            ModifyTrait(trait.Name, modifier);
            PreferencesUpdated?.Invoke(this, EventArgs.Empty);
            return trait;
        }

        public bool SetSubscription()
        {
            if (IsSubscriber)
                return false;
            IsSubscriber = true;
            for (int i = 0; i < 2; i++)
                RevealTrait();
            return true;
        }

        public ResultsArgs BuyArt(Art artToBuy, bool isOriginal, GameStats gameStats)
        {
            ArtAcquisition _acquiredArt = Acquisitions.Find(x => x.Art.Name == artToBuy.Name);
            //check if owned
            if (_acquiredArt is not null)
            {
                //if original owned
                if (_acquiredArt.IsOriginal)
                {
                    return new ResultsArgs($"{Name} all ready owns the original of \"{_acquiredArt.Art.Name}\".", "(No sale)");
                }

                if (!_acquiredArt.IsOriginal && !isOriginal)
                {
                    return new ResultsArgs($"{Name} all ready owns a print of \"{_acquiredArt.Art.Name}\".", "(No sale)");
                }

                //if print owned
                if (!artToBuy.IsSold && !_acquiredArt.IsOriginal && isOriginal)
                {
                    Acquisitions.Add(new ArtAcquisition(artToBuy, isOriginal, gameStats.CurrentMonth));
                    RevealMatchingTraits(artToBuy);
                    artToBuy.IsSold = true;
                    gameStats.OriginalsThisMonth++;
                    return new ResultsArgs($"Noting that {Name} picked up a print of \"{_acquiredArt.Art.Name}\", it did not take much convincing for them to buy the original!", $"(+1 Original Sale)");
                }

                return new ResultsArgs($"\"I don't really need a new copy of {_acquiredArt.Art.Name}\".", $"(no sale)");
            }
            //if not previously owned, add to list of acquisitions
            Acquisitions.Add(new ArtAcquisition(artToBuy, isOriginal, gameStats.CurrentMonth));
            if (isOriginal && !artToBuy.IsSold)
            {
                artToBuy.IsSold = true;
                gameStats.OriginalsThisMonth++;
                RevealMatchingTraits(artToBuy);
                return new ResultsArgs(
                    description: $"{Name} loves {artToBuy.Name} and has decided to buy the orignal!",
                    summary: $"(+1 Original Sale)");
            }
            artToBuy.PrintsSold++;
            gameStats.PrintsThisMonth++;
            return new ResultsArgs(
                description: $"{Name} likes {artToBuy.Name} and will buy a print!",
                summary: $"(+1 Print Sale)");

        }

        void RevealMatchingTraits(Art art)
        {
            foreach (var trait in art.EmotiveTraits)
            {
                var patronTrait = EmotiveTraits.Find(x => x.Name == trait.Name);
                if (patronTrait is not null)
                    patronTrait.IsKnown = true;
            }    
            foreach (var trait in art.AestheticTraits)
            {
                var patronTrait = AestheticTraits.Find(x => x.Name == trait.Name);
                if (patronTrait is not null)
                    patronTrait.IsKnown = true;
            }
        }
    }
}