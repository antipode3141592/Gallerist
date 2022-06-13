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
        }
                
        void Update()
        {
            //Debug.Log($"Aesthetic: {GetRandomTrait(AestheticTraits)}, Emotive: {GetRandomTrait(EmotiveTraits)}");
            
        }


        void GenerateArtist(string name, string id, IList<ITrait> favoredTraits)
        {
            Artist = new Artist(name: name, id: id, favoredTraits: favoredTraits);
        }

        void GenerateArt()
        {
            //create piece of art
            var newArt = new Art(name: GenerateArtName(), description: GenerateArtDescription(), artistId: Artist.Id, qualities: GenerateQualities(3));
            //stats are based on Artist favoredTraits (ex: artist specializing in landscapes will tend to create landscapes)

            //add art to ArtPieces list
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
            int randomIndex = Random.Range(0, traitList.Count);
            return traitList[randomIndex];
        }

        IList<ITrait> GenerateQualities(int totalQuality)
        {
            return null;
        }
    }
}