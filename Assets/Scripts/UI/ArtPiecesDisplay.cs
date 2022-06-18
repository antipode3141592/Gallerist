using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Gallerist.UI
{
    public class ArtPiecesDisplay : MonoBehaviour
    {
        [SerializeField] ArtCard ArtCard;
        [SerializeField] List<ClickableImage> ArtThumbnails;
        [SerializeField] Button ScrollRightButton;
        [SerializeField] Button ScrollLeftButton;
        [SerializeField] TextMeshProUGUI paginationText;
        [SerializeField] TextMeshProUGUI searchText;

        int currentPage;

        GameManager _gameManager;

        void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            foreach (var art in ArtThumbnails)
            {
                art.OnImageClicked += OnArtThumbnailClicked;
            }

        }

        void OnEnable()
        {
            currentPage = 0;
        }

        void OnArtThumbnailClicked(object sender, string e)
        {
            foreach (var image in ArtThumbnails) { image.ResetBackground(); }
            if (e == string.Empty) return;
            Art art = _gameManager.ArtPieces.Find(x => x.Name == e);
            if (art is null) return;
            //highlight patronportraitbackground
            var thumbnail = sender as ClickableImage;
            if (thumbnail is not null) thumbnail.HighlightBackground();
            ArtCard.LoadArtCardData(art);
        }

        public void SetThumbnails()
        {
            foreach (var image in ArtThumbnails) { image.ResetBackground(); }
            int pageSize = ArtThumbnails.Count;
            for (int i = 0; i < pageSize; i++)
            {
                int artIndex = i + pageSize * currentPage;
                if (_gameManager.ArtPieces.Count <= artIndex) return;
                ArtThumbnails[i].SetImage(
                    sprite: _gameManager.ArtPieces[artIndex].Image,
                    name: _gameManager.ArtPieces[artIndex].Name);
            }
            ArtThumbnails[0].HighlightBackground();
            paginationText.text = $"{1 + pageSize * currentPage} to {pageSize * (1 + currentPage)} of {_gameManager.ArtPieces.Count}";
            ArtCard.LoadArtCardData(art: _gameManager.ArtPieces[pageSize * currentPage]);
        }

        public void PageRight()
        {
            int _currentPage = currentPage;
            currentPage++;
            int lastPage = Mathf.CeilToInt(_gameManager.PatronPortaits.Count / ArtThumbnails.Count);
            if (currentPage >= lastPage)
            {
                currentPage = lastPage;
                return;
            }
            //if (currentPage != _currentPage)
            SetThumbnails();
        }

        public void PageLeft()
        {
            int _currentPage = currentPage;
            currentPage--;
            if (currentPage <= 0)
            {
                currentPage = 0;
                return;
            }
            //if (currentPage != _currentPage)
            SetThumbnails();
        }
    }
}