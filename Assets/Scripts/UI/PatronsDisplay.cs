using System;
using TMPro;
using UnityEngine;

namespace Gallerist.UI
{
    public class PatronsDisplay : MonoBehaviour
    {
        SchmoozeController _schmoozeController;
        IObjectManager<Patron> _patronManager;
        Pagination _pagination;

        [SerializeField] PatronCard PatronCard;

        void Awake()
        {
            _schmoozeController = FindObjectOfType<SchmoozeController>();
            _patronManager = FindObjectOfType<PatronManager>();
            _pagination = GetComponentInChildren<Pagination>();

            _schmoozeController.PatronUpdated += UpdatePatronCard;
            _patronManager.ObjectsGenerated += OnPatronsGenerated;
            _pagination.ThumbnailSelected += OnThumbnailSelected;
            _pagination.PageSelected += OnPageSelected;
        }

        void OnPageSelected(object sender, int e)
        {
            _pagination.SetThumbnails(_patronManager.CurrentObjects);
        }

        void OnThumbnailSelected(object sender, string e)
        {
            if (e == string.Empty) return;
            Patron patron = _patronManager.CurrentObjects.Find(x => x.Name == e);
            if (patron is null) return;
            _patronManager.SelectedObject = patron;
            PatronCard.LoadPatronCardData();
        }

        void OnPatronsGenerated(object sender, EventArgs e)
        {   
            _pagination.SetThumbnails(_patronManager.CurrentObjects);
        }

        void UpdatePatronCard(object sender, EventArgs e)
        {
            PatronCard.LoadPatronCardData();
        }
    }
}