using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist
{
    public class DialogueChoice : MonoBehaviour
    {
        [SerializeField] Image background;
        [SerializeField] Button button;
        [SerializeField] TextMeshProUGUI text;

        public event EventHandler OnDialogueSelect;

        void Awake()
        {
            button.onClick.AddListener(DialogueSelected);
        }

        void OnDestroy()
        {
            button.onClick.RemoveAllListeners();
        }

        public void SetText(string dialogueText)
        {
            text.text = dialogueText;
        }

        public void DialogueSelected()
        {
            background.color = Color.green;
            OnDialogueSelect?.Invoke(this, EventArgs.Empty);
        }

        public void DialogueUnSelected()
        {
            background.color = Color.clear;
        }
    }
}