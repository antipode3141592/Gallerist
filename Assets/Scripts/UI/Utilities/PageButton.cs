using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class PageButton: MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] TextMeshProUGUI _buttonText;
        [SerializeField] Image _buttonImage;
        int _index;
        public int Index => _index;

        public event EventHandler<int> OnClick;

        public void ImageClicked()
        {
            OnClick?.Invoke(this, _index);
        }

        public void SetButton(int index)
        {
            _index = index;
            _buttonText.text = $"{index + 1}";     //page '0' should display as '1'
        }

        public void HighlightBackground(bool highlight)
        {
            _buttonImage.color = highlight ? Color.green : Color.white;
        }
    }
}