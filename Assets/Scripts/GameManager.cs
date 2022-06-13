using System;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gallerist
{
    public class GameManager : MonoBehaviour
    {
        public Artist Artist { get; set; }
        public List<Art> ArtPieces { get; set; }
        public List<string> AestheticTraits { get; set; }
        public List<string> EmotiveTraits { get; set; }

        private void Awake()
        {
            ArtPieces = new List<Art>();
            AestheticTraits = new List<string>();
            EmotiveTraits = new List<string>();
        }

        void Start()
        {
            //load trait lists
            var _aestheticWords = Resources.Load<TextAsset>("Gallerist - Aesthetics");
            var _emotiveWords = Resources.Load<TextAsset>("Gallerist - Emotive");



            AestheticTraits.AddRange(_aestheticWords.text.Split(',','\n').ToList());
            EmotiveTraits.AddRange(_emotiveWords.text.Split(',','\n').ToList());

            Debug.Log($"AT count : {AestheticTraits.Count}, ET count : {EmotiveTraits.Count}");

            GenerateArtist("Rand O. Morrigan", "Rand", new List<ITrait>());
            GenerateArt();
            foreach (var art in ArtPieces) {
                Debug.Log($"Art:  {art.Name} , {art.Description}");
                foreach (var trait in art.Traits)
                {
                    Debug.Log($"    {trait.Name} {trait.Value}");
                }
            }
        }
                
        void Update()
        {
            
            
        }


        void GenerateArtist(string name, string id, IList<ITrait> favoredTraits)
        {
            Artist = new Artist(name: name, id: id, favoredTraits: favoredTraits);
        }

        void GenerateArt()
        {
            //create piece of art
            var newArt = new Art(name: GenerateArtName(), description: GenerateArtDescription(), artistId: Artist.Id, qualities: GenerateTraits(3, typeof(ArtTrait)));
            //stats are based on Artist favoredTraits (ex: artist specializing in landscapes will tend to create landscapes)

            //add art to ArtPieces list
            ArtPieces.Add(newArt);
        }

        string GenerateArtName()
        {
            return ("RandomName");
        }

        string GenerateArtDescription()
        {
            return ("RandomDescription");
        }

        string GetRandomTrait(List<string> traitList)
        {
            int randomIndex = UnityEngine.Random.Range(0, traitList.Count);
            return traitList[randomIndex];
        }

        IList<ITrait> GenerateTraits(int totalTraits, Type traitType)
        {
            List<ITrait> traits = new List<ITrait>();
            for (int i = 0; i < totalTraits; i++)
            {
                if (traitType == typeof(ArtTrait))
                {
                    Debug.Log($"ArtTrait Requested...");
                    traits.Add(new ArtTrait(GetRandomTrait(AestheticTraits), true));
                    traits.Add(new ArtTrait(GetRandomTrait(EmotiveTraits), true));
                }
            }
            
            return traits;
        }
    }
}