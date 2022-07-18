using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class MainEventDisplay : MonoBehaviour
    {
        [SerializeField] Image mainEvent;
        [SerializeField] Image foodAndDrink;

        PreparationController _preparationController;

        void Awake()
        {
            _preparationController = FindObjectOfType<PreparationController>();
        }

        void OnEnable()
        {
            
        }
    }
}