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

        int currentPage;

        GameManager _gameManager;
        ArtManager _artManager;

        void Awake()
        {
            _gameManager = FindObjectOfType<GameManager>();
            _artManager = FindObjectOfType<ArtManager>();
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
            Art art = _artManager.ArtPieces.Find(x => x.Name == e);
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
                if (_artManager.ArtPieces.Count <= artIndex) return;
                ArtThumbnails[i].SetImage(
                    sprite: _artManager.ArtPieces[artIndex].Image,
                    name: _artManager.ArtPieces[artIndex].Name);
            }
            ArtThumbnails[0].HighlightBackground();
            paginationText.text = $"{1 + pageSize * currentPage} to {pageSize * (1 + currentPage)} of {_artManager.ArtPieces.Count}";
            ArtCard.LoadArtCardData(art: _artManager.ArtPieces[pageSize * currentPage]);
        }

        public void PageRight()
        {
            currentPage++;
            int lastPage = Mathf.CeilToInt(_artManager.ArtPieces.Count / ArtThumbnails.Count);
            if (currentPage >= lastPage)
            {
                currentPage = lastPage;
                return;
            }
            SetThumbnails();
        }

        public void PageLeft()
        {
            currentPage--;
            if (currentPage < 0)
            {
                currentPage = 0;
                return;
            }
            SetThumbnails();
        }
    }
}