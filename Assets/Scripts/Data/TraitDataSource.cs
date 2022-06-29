using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Gallerist
{
    public class TraitDataSource : MonoBehaviour
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

        public List<ITrait> GenerateAestheticTraits(int totalTraits, Type traitType)
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
                }
                else if (traitType == typeof(ArtistTrait))
                {
                    traits.Add(new ArtistTrait(traitName, true, TraitType.Aesthetic));
                }
                else if (traitType == typeof(PatronTrait))
                {
                    traits.Add(new PatronTrait(traitName, false, TraitType.Aesthetic));
                }
                else
                {
                    return null;
                }
            }
            return traits;
        }

        public List<ITrait> GenerateEmotiveTraits(int totalTraits, Type traitType)
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
    }
}