using System.Collections.Generic;
using System.Linq;
using UnityEngine;

namespace Gallerist
{
    public class NameDataSource : MonoBehaviour
    {
        public List<string> FirstNames { get; set; }
        public List<string> LastNames { get; set; }

        void Awake()
        {
            FirstNames = new();
            LastNames = new();
            var _firstNames = Resources.Load<TextAsset>("Gallerist - FirstNames");
            var _lastNames = Resources.Load<TextAsset>("Gallerist - LastNames");
            FirstNames.AddRange(_firstNames.text.Split(',', '\n').ToList());
            LastNames.AddRange(_lastNames.text.Split(',', '\n').ToList());
            Debug.Log($"FirstNames count: {FirstNames.Count}");
            Debug.Log($"LastNames count: {LastNames.Count}");
        }

        public string GenerateRandomPatronName()
        {
            int randomIndex = Random.Range(0, FirstNames.Count);
            return FirstNames[randomIndex].Trim() + " " + RandomLastNameLetter();
        }

        public string GenerateRandomArtistName()
        {
            int randomIndex1 = Random.Range(0, FirstNames.Count);
            int randomIndex2 = Random.Range(0, LastNames.Count);
            return FirstNames[randomIndex1].Trim() + " " + RandomLastNameLetter() + " " + LastNames[randomIndex2].Trim();
        }

        public string RandomLastNameLetter()
        {
            int randomIndex = Random.Range(65, 90); //https://www.dotnetperls.com/ascii-table
            return $"{(char)randomIndex}.";
        }

        public string GenerateArtName()
        {
            return ($"RandomName_{UnityEngine.Random.Range(0, 1000)}");
        }

        public string GenerateArtDescription()
        {
            return ($"RandomDescription_{UnityEngine.Random.Range(0, 1000)}");
        }
    }
}