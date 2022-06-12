using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class GameManager : MonoBehaviour
    {
        public Artist Artist { get; set; }
        public IList<Art> ArtPieces { get; set; }

        private void Awake()
        {
            
        }

        void Start()
        {

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

            //stats are based on Artist favoredTraits (ex: artist specializing in landscapes will tend to create landscapes)

            //add art to ArtPieces list
        }
    }
}