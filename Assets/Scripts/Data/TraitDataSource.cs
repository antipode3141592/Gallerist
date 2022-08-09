using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class TraitDataSource : MonoBehaviour, ITraitDataSource
    {
        public List<string> AestheticTraits { get; set; }
        public List<string> EmotiveTraits { get; set; }

        void Awake()
        {
            AestheticTraits = new();
            EmotiveTraits = new();
            //load trait lists
            var _aestheticWords = Resources.Load<TextAsset>("Gallerist - Aesthetics");
            var _emotiveWords = Resources.Load<TextAsset>("Gallerist - Emotive");

            AestheticTraits.AddRange(_aestheticWords.text.Split(',', '\n').ToList());
            EmotiveTraits.AddRange(_emotiveWords.text.Split(',', '\n').ToList());

            Debug.Log($"AT count : {AestheticTraits.Count}, ET count : {EmotiveTraits.Count}");
        }

        string GetRandomTraitName(List<string> traitList)
        {
            int randomIndex = Random.Range(0, traitList.Count);
            return traitList[randomIndex].Trim();
        }

        public List<ITrait> GenerateAestheticTraits(int totalTraits, Type traitType, List<string> requiredTraits = null, int bonus = 0)
        {
            List<ITrait> traits = new();
            List<string> traitNames = requiredTraits is null ? new() : requiredTraits;


            for (int i = 0; i < totalTraits; i++)
            {
                string traitName;
                int traitValue = 0;
                if (traitNames.ElementAtOrDefault(i) is null)
                {
                    do { traitName = GetRandomTraitName(AestheticTraits); }
                    while (traitNames.Contains(traitName));
                    traitNames.Add(traitName);
                }

                if (traitType == typeof(ArtTrait))
                {
                    traitValue = Random.Range(1 + bonus, 5);
                    traits.Add(new ArtTrait(traitNames[i], traitValue, true, TraitType.Aesthetic));
                }
                else if (traitType == typeof(ArtistTrait))
                {
                    traitValue = Random.Range(1 + bonus, 5);
                    traits.Add(new ArtistTrait(traitNames[i], traitValue, true, TraitType.Aesthetic));
                }
                else if (traitType == typeof(PatronTrait))
                {
                    //last trait is negative, others are positive
                    traitValue = i == totalTraits - 1 ? Random.Range(-5, -1) : Random.Range(1 + bonus, 5);
                    traits.Add(new PatronTrait(traitNames[i], traitValue, false, TraitType.Aesthetic));
                }
                else
                {
                    return null;
                }
            }
            return traits;
        }

        public List<ITrait> GenerateEmotiveTraits(int totalTraits, Type traitType, List<string> requiredTraits = null, int bonus = 0)
        {
            List<ITrait> traits = new();
            List<string> traitNames = requiredTraits is null ? new() : requiredTraits;
            for (int i = 0; i < totalTraits; i++)
            {
                string traitName;
                int traitValue = 0;
                if (traitNames.ElementAtOrDefault(i) is null)
                {
                    do { traitName = GetRandomTraitName(EmotiveTraits); }
                    while (traitNames.Contains(traitName));
                    traitNames.Add(traitName);
                }


                if (traitType == typeof(ArtTrait))
                {
                    traitValue = Random.Range(1, 5) + bonus;
                    traits.Add(new ArtTrait(traitNames[i], traitValue, true, TraitType.Emotive));
                }
                else if (traitType == typeof(ArtistTrait))
                {
                    traitValue = Random.Range(1, 5) + bonus;
                    traits.Add(new ArtistTrait(traitNames[i], traitValue, true, TraitType.Emotive));
                }
                else if (traitType == typeof(PatronTrait))
                {
                    //first trait is negative, others are positive
                    traitValue = i == totalTraits - 1 ? Random.Range(-5, -1) : Random.Range(1, 5) + bonus;
                    traits.Add(new PatronTrait(traitNames[i], traitValue, false, TraitType.Emotive));
                }
                else
                {
                    return null;
                }
            }
            return traits;
        }

        public List<string> GetRandomTraitNames(int totalTraits, TraitType traitType)
        {
            List<string> traitNames = new();
            for (int i = 0; i < totalTraits; i++)
            {
                string traitName;
                do
                {
                    traitName = GetRandomTraitName(
                   traitType == TraitType.Emotive ? EmotiveTraits : AestheticTraits);
                }
                while (traitNames.Contains(traitName));
                traitNames.Add(traitName);
            }
            return traitNames;
        }
    }
}