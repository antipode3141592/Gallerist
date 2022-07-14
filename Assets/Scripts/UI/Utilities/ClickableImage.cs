using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class ClickableImage : MonoBehaviour
    {
        [SerializeField] Button _button;
        [SerializeField] Image _image;
        [SerializeField] TextMeshProUGUI _text;
        [SerializeField] Image _highlightImage;

        public event EventHandler<string> OnImageClicked;

        string _name;

        private void Awake()
        {
            ResetBackground();
        }

        public void SetImage(Sprite sprite, string name)
        {
            _image.sprite = sprite;
            _name = name;
            _text.text = name;
        }

        public void ImageClicked()
        {
            if (_name == string.Empty) return;

            OnImageClicked?.Invoke(this, _name);
        }
        
        public void HighlightBackground()
        {
            _highlightImage.color = Color.green;
        }

        public void ResetBackground()
        {
            _highlightImage.color = Color.clear;
        }
    }
}