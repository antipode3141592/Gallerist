using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class UIGuage : MonoBehaviour
    {
        [SerializeField] Image GuageBackground;
        [SerializeField] Image GuageForeground;
        [SerializeField] int GuageMin;
        [SerializeField] int GuageMax;
    }
}