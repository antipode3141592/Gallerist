using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace Gallerist.UI
{
    public class EndDisplay : MonoBehaviour
    {
        GameManager gameManager;

        [SerializeField] TextMeshProUGUI SummaryResultText;
        [SerializeField] TextMeshProUGUI NightDescriptionText;
        [SerializeField] TextMeshProUGUI OverallEnjoymentText;
        [SerializeField] TextMeshProUGUI OriginalsSoldText;
        [SerializeField] TextMeshProUGUI PrintsSoldText;
        [SerializeField] TextMeshProUGUI NewSubscribers;

        void Awake()
        {
            gameManager = FindObjectOfType<GameManager>();
        }

        public void SummarizeNight()
        {
            int originalsSold = gameManager.OriginalsSold;
            int printsSold = gameManager.PrintsSold;
            if (originalsSold == gameManager.MaximumEvaluations)
            {
                SummaryResultText.text = $"The night was a roaring success!";
                
            }
            else if (originalsSold > 0)
            {
                SummaryResultText.text = $"The night was a great success!";
            }
            else if (originalsSold == 0 && printsSold > 0)
            {
                SummaryResultText.text = $"The night was a moderate success.";
            }
            else
            {
                SummaryResultText.text = $"The night was a dissapointment.";
            }

            //
            NightDescriptionText.text = $"The assembled crowd found {gameManager.Artist.Name}'s work mesmerizing.  [buyer1] had a deep connection to [art]'s sense of [shared postive emotive trait] while also being a [positive adjective] example of [shared postive aesthetic trait] and decided to purchase the original.";


            OriginalsSoldText.text = $"Originals Sold: {originalsSold}";
            PrintsSoldText.text = $"Prints Sold: {printsSold}";
        }
    }
}