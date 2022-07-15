using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class TimeTracker : MonoBehaviour
    {
        [SerializeField] Image Background;
        [SerializeField] Image Foreground;
        [SerializeField] TextMeshProUGUI CounterText;

        int _totalTime;

        public void Reset(int totalTime)
        {
            _totalTime = totalTime;
            UpdateCounterText(0);
            UpdateForeground(0);
        }

        void UpdateCounterText(int elapsedTime)
        {
            CounterText.text = $"{_totalTime - elapsedTime:D2}:00";
        }

        public void UpdateForeground(int elapsedTime)
        {
            float fillPercentage = (float)elapsedTime / (float)_totalTime;
            Foreground.rectTransform.localScale = new Vector3(x: fillPercentage, y: 1f, z: 1f);
            UpdateCounterText(elapsedTime);
        }
    }
}