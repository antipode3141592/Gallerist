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

        public void SetButton(int index, int pageSize)
        {
            _index = index;
            _buttonText.text = $"{index * pageSize +1}-{(index + 1) * pageSize}";
        }

        public void HighlightBackground(bool highlight)
        {
            _buttonImage.color = highlight ? Color.green : Color.white;
        }
    }
}