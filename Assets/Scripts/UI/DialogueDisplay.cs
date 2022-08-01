using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist.UI
{
    public class DialogueDisplay : Display
    {
        [SerializeField] List<DialogueChoice> dialogueChoices;

        protected override void Awake()
        {
            base.Awake();
            foreach(DialogueChoice choice in dialogueChoices)
            {
                choice.OnDialogueSelect += DialogueSelected;
            }
        }

        void DialogueSelected(object sender, EventArgs e)
        {
            foreach(DialogueChoice choice in dialogueChoices)
            {
                if (sender as DialogueChoice == choice) continue;
                choice.DialogueUnSelected();
            }

        }
    }
}