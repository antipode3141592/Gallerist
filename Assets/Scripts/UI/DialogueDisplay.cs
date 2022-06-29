using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist
{
    public class DialogueDisplay : MonoBehaviour
    {
        [SerializeField] List<DialogueChoice> dialogueChoices;

        void Awake()
        {
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