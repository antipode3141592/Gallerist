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
        [SerializeField] Image highlightImage;

        ITrait currentTrait;
        public string TraitName => currentTrait.Name;

        void Awake()
        {
            highlightImage.color = Color.clear;
            backgroundImage.color = Color.grey;
        }

        void OnDisable()
        {
            highlightImage.color = Color.clear;
            backgroundImage.color = Color.grey;
        }

        public void UpdateText(ITrait trait, bool reveal = false, bool isShared = false)
        {
            currentTrait = trait;
            displayText.text = reveal || trait.IsKnown ? $"{trait.Name} {trait.Value}" : $"(unknown)";
            FocusTrait(isShared);
        }

        public void FocusTrait(bool showFocus)
        {
            backgroundImage.color = showFocus ? Color.white : Color.grey;
            StartCoroutine(FadeFocus());
        }

        public void HighlightTrait(int value)
        {
            if (Debug.isDebugBuild)
                Debug.Log($"Highlighting trait {TraitName} which was modified by {value}");
            highlightImage.color = value > 0 ? Color.green : Color.red;
            StartCoroutine(FadeHighlight());
        }

        IEnumerator FadeHighlight()
        {
            yield return new WaitForSeconds(fadeTime);
            highlightImage.color = Color.clear;
        }

        IEnumerator FadeFocus()
        {
            yield return new WaitForSeconds(fadeTime);
            backgroundImage.color = Color.grey;
        }


    }
}