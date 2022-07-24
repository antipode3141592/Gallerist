using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class PreparationItem : MonoBehaviour
    {
        [SerializeField] Image highlightImage;
        [SerializeField] TextMeshProUGUI NameText;
        [SerializeField] TextMeshProUGUI ModifiersText;
        public Button button;

        public string Name => NameText.text;

        public event EventHandler ItemClicked;
        void Awake()
        {
            button.onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            ItemClicked?.Invoke(this, EventArgs.Empty);
        }

        public void Highlight(bool showHighlight)
        {
            highlightImage.color = showHighlight ? Color.green : Color.clear;
        }

        public void SetItem(string name, string modifiers)
        {
            NameText.text = name;
            ModifiersText.text = modifiers;
            Highlight(false);
        }

    }
}