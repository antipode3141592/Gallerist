using System;
using System.Collections.Generic;
using UnityEngine;

namespace Gallerist.UI
{
    public class ArtPiecesDisplay : MonoBehaviour
    {
        [SerializeField] ArtCard ArtCard;
        [SerializeField] List<ClickableImage> ArtThumbnails;

        ArtManager _artManager;
        Pagination _pagination;       
        

        void Awake()
        {
            _artManager = FindObjectOfType<ArtManager>();
            _pagination = GetComponentInChildren<Pagination>();

            _artManager.ObjectsGenerated += OnArtGenerated;
            _artManager.SelectedObjectChanged += OnSelectedArtChanged;
            _pagination.ThumbnailSelected += OnThumbnailSelected;
            _pagination.PageSelected += OnPageSelected;
        }

        void OnSelectedArtChanged(object sender, EventArgs e)
        {
            if (_artManager.CurrentObject is null) 
                return;
            ArtCard.LoadArtCardData();
        }

        void OnPageSelected(object sender, int e)
        {
            _pagination.SetThumbnails(_artManager.CurrentObjects);
        }

        void OnThumbnailSelected(object sender, string e)
        {
            if (e == String.Empty) return;
            Art art = _artManager.CurrentObjects.Find(x => x.Name == e);
            if (art is null) return;
            _artManager.SetCurrentObject(art);
            ArtCard.LoadArtCardData();
        }

        void OnArtGenerated(object sender, EventArgs e)
        {
            _pagination.SetThumbnails(_artManager.CurrentObjects);
        }
    }
}