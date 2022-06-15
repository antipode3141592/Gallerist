using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

namespace Gallerist.UI
{
    public class TraitDisplay : MonoBehaviour
    {
        [SerializeField] TextMeshProUGUI displayText;

        public void UpdateText(string text)
        {
            displayText.text = text;
        }
    }
}