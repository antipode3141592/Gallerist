using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gallerist
{
    public class SchmoozeDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI ActionCounterText;
        SchmoozeController schmoozeController;
        void Awake()
        {
            schmoozeController = FindObjectOfType<SchmoozeController>();
            schmoozeController.ActionTaken += schmoozeActionTaken;
            UpdateActionCounter();
        }

        private void schmoozeActionTaken(object sender, EventArgs e)
        {
            UpdateActionCounter();

        }

        private void UpdateActionCounter()
        {
            string actions = $"Schmoozing:  {schmoozeController.ActionsTaken} of {schmoozeController.MaximumActions} actions taken.";
            Debug.Log(actions);
            ActionCounterText.text = actions;
        }
    }
}