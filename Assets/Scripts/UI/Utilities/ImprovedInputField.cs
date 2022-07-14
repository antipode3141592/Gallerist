using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class ImprovedInputField : MonoBehaviour
    {
        [SerializeField] TMP_InputField inputField;

        public void OnInputChanged()
        {
            if (inputField.text.EndsWith('\r'))
            {
                Debug.Log("tab logged!");
            }
        }
    }
}