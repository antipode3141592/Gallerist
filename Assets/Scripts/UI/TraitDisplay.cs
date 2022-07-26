using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class TraitDisplay : MonoBehaviour
    {
        [SerializeField] float fadeTime = 1.5f;
        [SerializeField] TextMeshProUGUI displayText;
        [SerializeField] Image backgroundImage;
        ITrait currentTrait;
        public string TraitName => currentTrait.Name;

        void Awake()
        {
            backgroundImage.color = Color.clear;
        }

        void OnDisable()
        {
            backgroundImage.color = Color.clear;
        }

        public void UpdateText(ITrait trait, bool reveal = false)
        {
            currentTrait = trait;
            displayText.text = reveal || trait.IsKnown ? $"{trait.Name} {trait.Value}" : $"(unknown)";
        }

        public void HighlightTrait(int value)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"Highlighting trait {TraitName} which was modified by {value}");
            backgroundImage.color = value > 0 ? Color.green : Color.red;
            StartCoroutine(FadeHighlight());
        }

        IEnumerator FadeHighlight()
        {
            yield return new WaitForSeconds(fadeTime);
            backgroundImage.color = Color.clear;
        }
    }
}