using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class StartDisplay : MonoBehaviour
    {
        GameManager gameManager;

        [SerializeField] TextMeshProUGUI DescriptionText;
        [SerializeField] TextMeshProUGUI SummaryText;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        void OnEnable()
        {
            SetDescription();

            SetSummary();
        }

        public void CompleteStart()
        {

            gameManager.CompleteStart();
        }

        void SetDescription()
        {
            string output = "";
            switch (gameManager.CurrentMonth)
            {
                case 1:
                    output = $"{gameManager.GalleryName} is nearing its grand opening.  Time to finalize preparations for the first opening night!";
                    break;
                case 2:
                    output = $"{gameManager.GalleryName} is becoming more well known in the community and you expect this month's show should have a larger turnout.";
                    break;
                case 3:
                    break;
                case 4:
                    break;
                case 5:
                    break;
                case 6:
                    break;
                default:
                    break;

            }
            DescriptionText.text = output;
        }

        void SetSummary()
        {
            SummaryText.text = $"{gameManager.CurrentMonth} of {gameManager.TotalMonths}";
        }
    }
}